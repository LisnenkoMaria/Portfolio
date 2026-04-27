using YandexDiskManager.DataClasses;

namespace YandexDiskManager.DataClasses
{
    public class YandexDiskItem
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public long Size { get; set; }

        public string FormattedSize
        {
            get
            {
                if (IsFolder) return "";
                return FileSizeFormatter.FormatSize(this.Size);
            }
        }
    }
}