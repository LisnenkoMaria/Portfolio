using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YandexDiskManager.DataClasses;

namespace YandexDiskManager.Services
{
    public class DownloadService
    {
        public ObservableCollection<DownloadItem> ActiveDownloads { get; } = new ObservableCollection<DownloadItem>();
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task StartDownloadAsync(YandexAccount account, YandexDiskItem file, string targetFolder)
        {
            var cts = new CancellationTokenSource();
            DownloadItem item = null;
            string savePath = Path.Combine(targetFolder, file.Name);

            try
            {
                // 1. Создаем элемент UI
                item = new DownloadItem(cts, file.Size)
                {
                    FileName = file.Name,
                    Status = "Получение ссылки..."
                };
                System.Windows.Application.Current.Dispatcher.Invoke(() => ActiveDownloads.Add(item));

                // 2. Получаем ссылку на скачивание
                var link = await account.Api.Files.GetDownloadLinkAsync(file.Path);

                // 3. Скачиваем вручную для прогресса
                using (var response = await _httpClient.GetAsync(link.Href, HttpCompletionOption.ResponseHeadersRead, cts.Token))
                {
                    response.EnsureSuccessStatusCode();
                    long totalBytes = file.Size;
                    long receivedBytes = 0;

                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        item.Status = "Скачивание...";
                        var buffer = new byte[8192];
                        int bytesRead;

                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cts.Token)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead, cts.Token);
                            receivedBytes += bytesRead;

                            if (totalBytes > 0)
                            {
                                item.Progress = (double)receivedBytes * 100 / totalBytes;
                            }
                        }
                    }
                }

                item.Status = "Завершено";
                item.Progress = 100;
            }
            catch (OperationCanceledException)
            {
                if (item != null) item.Status = "Отменено";
                try { if (File.Exists(savePath)) File.Delete(savePath); } catch { }
            }
            catch (Exception ex)
            {
                if (item != null) item.Status = $"Ошибка: {ex.Message}";
            }
            finally
            {
                if (item != null)
                {
                    await Task.Delay(3000);
                    System.Windows.Application.Current.Dispatcher.Invoke(() => ActiveDownloads.Remove(item));
                }
            }
        }
    }
}