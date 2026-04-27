using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YandexDiskManager.DataClasses;



namespace YandexDiskManager.Services
{
    public class LocalFileService
    {
        /// <summary>
        /// Получает список дисков
        /// </summary>
        public List<WindowsItem> GetDrives()
        {
            var items = new List<WindowsItem>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    items.Add(new WindowsItem
                    {
                        Name = drive.Name,
                        FullPath = drive.Name,
                        IsFolder = true
                    });
                }
            }
            return items;
        }

        /// <summary>
        /// Получает содержимое папки (файлы и подпапки), скрывая системные
        /// </summary>
        public List<WindowsItem> GetDirectoryContents(string path)
        {
            var items = new List<WindowsItem>();
            var dirInfo = new DirectoryInfo(path);

            // 1. Папки
            foreach (var dir in dirInfo.GetDirectories())
            {
                bool isHidden = dir.Attributes.HasFlag(FileAttributes.Hidden);
                bool isSystem = dir.Attributes.HasFlag(FileAttributes.System);

                if (!isHidden && !isSystem)
                {
                    items.Add(new WindowsItem
                    {
                        Name = dir.Name,
                        FullPath = dir.FullName,
                        IsFolder = true
                        
                    });
                }
            }

            // 2. Файлы
            foreach (var file in dirInfo.GetFiles())
            {
                bool isHidden = file.Attributes.HasFlag(FileAttributes.Hidden);
                bool isSystem = file.Attributes.HasFlag(FileAttributes.System);

                if (!isHidden && !isSystem)
                {
                    items.Add(new WindowsItem
                    {
                        Name = file.Name,
                        FullPath = file.FullName,
                        IsFolder = false,
                        Size = file.Length
                    });
                }
            }

            return items;
        }

        /// <summary>
        /// Удаляет файл или папку
        /// </summary>
        public void DeleteItem(WindowsItem item)
        {
            if (item.IsFolder)
                Directory.Delete(item.FullPath, true);
            else
                File.Delete(item.FullPath);
        }

        /// <summary>
        /// Переименовывает файл или папку
        /// </summary>
        public void RenameItem(WindowsItem item, string newName)
        {
            string dir = Path.GetDirectoryName(item.FullPath);
            string newPath = Path.Combine(dir, newName);

            if (item.IsFolder)
                Directory.Move(item.FullPath, newPath);
            else
                File.Move(item.FullPath, newPath);
        }
    }
}
