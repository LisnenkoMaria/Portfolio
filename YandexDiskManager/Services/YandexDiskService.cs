using YandexDiskManager.DataClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;


namespace YandexDiskManager.Services
{
    public class YandexDiskService
    {
        private const string TokenDirectory = "yandex_tokens";
        private const string ClientId = "a88f9b1590374608832233431172d381"; // Паспорт приложения от Яндекс.OAuth
        private const string RedirectUri = "http://localhost:8080/";

        private class PassportInfo
        {
            public string login { get; set; }
            public string display_name { get; set; }
        }

        public async Task<YandexAccount> AuthenticateUserAsync(string userId)
        {
            string tokenPath = Path.Combine(TokenDirectory, userId);
            string token = null;

            // 1. Чтение токена
            if (Directory.Exists(tokenPath))
            {
                string tokenFile = Path.Combine(tokenPath, "token.txt");
                if (File.Exists(tokenFile))
                {
                    token = File.ReadAllText(tokenFile);
                }
                if (string.IsNullOrEmpty(token)) return null;
            }
            else // Токен не существует, нужно получить новый (только при нажатии BtnAddAccount_Click)
            {
                // 2. Получаем новый токен
                token = await GetTokenFromBrowserAsync();
                if (string.IsNullOrEmpty(token)) return null;

                // Сохраняем токен в новую папку/файл
                Directory.CreateDirectory(tokenPath);
                File.WriteAllText(Path.Combine(tokenPath, "token.txt"), token);
            }

            // 3. Создаем клиент API
            var api = new DiskHttpApi(token);

            try
            {
                // Получаем логин через Yandex Passport API
                var client = new HttpClient();
                var passportUrl = $"https://login.yandex.ru/info?oauth_token={token}&format=json";
                var response = await client.GetStringAsync(passportUrl);

                var passportInfo = JsonConvert.DeserializeObject<PassportInfo>(response);

                if (string.IsNullOrEmpty(passportInfo?.login))
                {
                    // Токен не вернул логин -> невалиден
                    return null;
                }

                // Проверка, что сам Диск доступен
                await api.MetaInfo.GetDiskInfoAsync();

                return new YandexAccount
                {
                    UserId = userId,
                    Login = passportInfo.login,
                    Token = token,
                    Api = api
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<string> GetTokenFromBrowserAsync()
        {
            // Формируем URL авторизации (token response type)
            string url = $"https://oauth.yandex.ru/authorize?response_type=token&client_id={ClientId}&redirect_uri={RedirectUri}&display=popup&force_confirm=yes";

            // Запускаем локальный слушатель
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add(RedirectUri);
                listener.Start();

                // Открываем браузер
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                // Ждем входящего запроса (браузер редиректнет на localhost)
                // Внимание: Яндекс при implicit flow возвращает токен в #hash, который сервер не видит.
                // Хитрость: Сервер должен отдать HTML c JS, который вытащит токен из URL и отправит его обратно на сервер.

                // ШАГ 1: Ловим редирект от Яндекса
                var context = await listener.GetContextAsync();
                var response = context.Response;

                // Отдаем JS код, который считает токен из адресной строки и сделает POST запрос к нам же
                string responseString = @"
                    <html><body>
                    <script>
                        var hash = window.location.hash;
                        if(hash) {
                            var token = hash.match(/access_token=([^&]*)/)[1];
                            fetch('/', { method: 'POST', body: token }).then(() => window.close());
                            document.write('Авторизация успешна. Можете закрыть окно.');
                        } else {
                            document.write('Ошибка авторизации.');
                        }
                    </script>
                    </body></html>";

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();

                // ШАГ 2: Ловим POST запрос с токеном от нашего скрипта
                var tokenContext = await listener.GetContextAsync();
                using (var reader = new StreamReader(tokenContext.Request.InputStream))
                {
                    string token = await reader.ReadToEndAsync();
                    tokenContext.Response.Close();
                    return token;
                }
            }
        }
        
        public async Task<List<YandexDiskItem>> ListFilesAsync(IDiskApi api, string path)
        {
            // В Яндексе корень "/"
            if (string.IsNullOrEmpty(path)) path = "/";

            var resource = await api.MetaInfo.GetInfoAsync(new ResourceRequest { Path = path, Limit = 200 });
            var list = new List<YandexDiskItem>();

            // _embedded содержит список файлов внутри папки
            if (resource.Embedded != null)
            {
                foreach (var item in resource.Embedded.Items)
                {
                    list.Add(new YandexDiskItem
                    {
                        Path = item.Path, // Ключевое поле: "disk:/Foo/Bar"
                        Name = item.Name,
                        IsFolder = item.Type == ResourceType.Dir,
                        Size = item.Size
                    });
                }
            }
            return list;
        }

        public async Task DeleteFileAsync(IDiskApi api, string path)
        {
            await api.Commands.DeleteAsync(new DeleteFileRequest { Path = path });
        }

        public async Task RenameFileAsync(IDiskApi api, string path, string newName)
        {
            string folder = path.Substring(0, path.LastIndexOf('/'));
            string newPath = folder + "/" + newName;

            await api.Commands.MoveAsync(new MoveFileRequest { From = path, Path = newPath });
        }

        public async Task CopyFileAsync(IDiskApi api, string path, string targetFolder)
        {
            // Вычисляем имя файла
            string fileName = path.Substring(path.LastIndexOf('/') + 1);
            string newPath = (targetFolder == "/" ? "" : targetFolder) + "/" + fileName;

            await api.Commands.CopyAsync(new CopyFileRequest { From = path, Path = newPath });
        }
    }
}