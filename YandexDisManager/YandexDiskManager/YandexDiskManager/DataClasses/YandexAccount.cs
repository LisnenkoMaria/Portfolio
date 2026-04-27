using YandexDisk.Client.Http; 

namespace YandexDiskManager.DataClasses
{
    public class YandexAccount
    {
        public string UserId { get; set; }

        public string Login { get; set; }
        public string Token { get; set; }
        public DiskHttpApi Api { get; set; } // Клиент API Яндекс.Диска

        public override string ToString()
        {
            return Login ?? UserId;
        }
    }
}