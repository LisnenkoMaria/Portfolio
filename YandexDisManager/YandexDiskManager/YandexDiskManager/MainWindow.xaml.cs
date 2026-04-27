using YandexDiskManager.DataClasses;
using YandexDiskManager.Services;
using YandexDiskManager.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace YandexDiskManager
{
    public partial class MainWindow : Window
    {
        private readonly YandexDiskService _yandexService = new YandexDiskService();

        // Коллекция аккаунтов
        private ObservableCollection<YandexAccount> _yandexAccounts = new ObservableCollection<YandexAccount>();
        private YandexAccount _currentYandexAccount;

        // Буфер обмена:
        private List<YandexDiskItem> _diskClipboard = new List<YandexDiskItem>();

        // История навигации 
        private Stack<string> _diskHistory = new Stack<string>();

        // === СЕРВИСЫ ===
        private readonly YandexDiskService _googleService = new YandexDiskService();
        private readonly LocalFileService _localService = new LocalFileService();
        private readonly DownloadService _downloadService = new DownloadService();

        // === ДАННЫЕ ===
        private const string TokenDirectory = "yandex_tokens";

        public ObservableCollection<DownloadItem> ActiveDownloads => _downloadService.ActiveDownloads;

        // === ПЕРЕМЕННЫЕ ИНТЕРФЕЙСА ===
        private Point _dragStartPoint;
        private Point _selectionStartPoint;
        private bool _isDragging = false;
        private bool _isSelecting = false;
        private bool _clickedSelected = false;
        private RubberBandAdorner _rubberBandAdorner;
        private AdornerLayer _adornerLayer;

        // Буфер обмена
        private bool _isDriveCut = false;
        private bool _isWindowsCut = false;

        // Переименование
        private object _itemToRename;
        private string _renameSourceList;

        public MainWindow()
        {
            InitializeComponent();

            // Привязываем список загрузок к коллекции в сервисе
            // теперь, когда сервис что-то качает, UI обновляется сам
            lstDownloads.ItemsSource = _downloadService.ActiveDownloads;
            _downloadService.ActiveDownloads.CollectionChanged += ActiveDownloads_CollectionChanged;

            LoadWindowsDrives();
            LoadPersistedAccounts();
        }


        #region =================== WINDOWS ЛОГИКА ===================

        private void LoadWindowsDrives()
        {
            lstWindowsFiles.Items.Clear();
            txtWindowsCurrentPath.Text = "This PC";
            btnWindowsUp.IsEnabled = false;

            var drives = _localService.GetDrives();
            foreach (var drive in drives) lstWindowsFiles.Items.Add(drive);
        }

        private void LoadWindowsDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Папка не найдена.");
                    return;
                }

                lstWindowsFiles.Items.Clear();
                txtWindowsCurrentPath.Text = path;
                btnWindowsUp.IsEnabled = true;

                var items = _localService.GetDirectoryContents(path);
                foreach (var item in items) lstWindowsFiles.Items.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка доступа: {ex.Message}");
                LoadWindowsDrives();
            }
        }

        private void BtnWindowsUp_Click(object sender, RoutedEventArgs e)
        {
            string currentPath = txtWindowsCurrentPath.Text;
            if (currentPath == "This PC") return;

            DirectoryInfo parentInfo = Directory.GetParent(currentPath);
            if (parentInfo != null) LoadWindowsDirectory(parentInfo.FullName);
            else LoadWindowsDrives();
        }

        #endregion

        #region =================== YANDEX DISK ЛОГИКА ===================

        private async void LoadPersistedAccounts()
        {
            if (!Directory.Exists(TokenDirectory)) Directory.CreateDirectory(TokenDirectory);

            // Получаем список папок, каждая из которых - потенциальный userId
            var userDirectories = Directory.GetDirectories(TokenDirectory);

            // Используем ToList(), чтобы не сбить итератор при удалении папок
            foreach (var userDir in userDirectories.ToList())
            {
                string userId = new DirectoryInfo(userDir).Name;

                var account = await _yandexService.AuthenticateUserAsync(userId);

                if (account != null)
                {
                    // Проверка на дубликат по логину
                    if (!_yandexAccounts.Any(a => a.Login == account.Login))
                    {
                        _yandexAccounts.Add(account);
                    }
                    else
                    {
                        // Если найден дубликат, удаляем лишнюю папку
                        try { Directory.Delete(userDir, true); } catch { }
                    }
                }
                else
                {
                    // Если аутентификация не удалась, удаляем папку с невалидным токеном
                    try { Directory.Delete(userDir, true); } catch { }
                }
            }

            // Привязываем UI к коллекции 
            lstAccounts.ItemsSource = _yandexAccounts;

            if (_yandexAccounts.Count > 0)
            {
                lstAccounts.SelectedIndex = 0;
                var selectedAccount = _yandexAccounts[0];

                _currentYandexAccount = selectedAccount;
                _diskHistory.Clear();
                LoadYandexDiskFolder("/");
            }
            else
            {
                // Если аккаунты не загрузились
                lstYandexDiskFiles.Items.Clear();
                txtYandexDiskCurrentPath.Text = "Нет аккаунтов";
            }
        }

        private async void BtnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            // Генерируем новый уникальный ID для сохранения токена нового пользователя
            string userId = "user-" + Guid.NewGuid().ToString("N");

            // Запускаем процесс аутентификации для нового пользователя
            // Токен будет сохранен в папку TokenDirectory/userId
            var newAccount = await _yandexService.AuthenticateUserAsync(userId);

            if (newAccount != null)
            {
                // 3. Проверяем, не был ли этот аккаунт (по логину) уже добавлен
                if (!_yandexAccounts.Any(a => a.Login == newAccount.Login))
                {
                    // Аккаунт новый - добавляем в список и делаем выбранным
                    _yandexAccounts.Add(newAccount);
                    lstAccounts.SelectedItem = newAccount;
                }
                else
                {
                    // Аккаунт уже есть. Сообщаем об этом и удаляем только что созданный 
                    // файл токена, чтобы не оставлять мусор.
                    MessageBox.Show($"Аккаунт {newAccount.Login} уже добавлен.");

                    // Выбираем существующий аккаунт в списке
                    lstAccounts.SelectedItem = _yandexAccounts.First(a => a.Login == newAccount.Login);

                    // Удаляем токен, который только что был создан (т.к. он дубликат)
                    try { Directory.Delete(Path.Combine(TokenDirectory, userId), true); } catch { }
                }
            }
        }


        private void LstAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAccounts.SelectedItem is YandexAccount selectedAccount)
            {
                _currentYandexAccount = selectedAccount;
                _diskHistory.Clear();
                LoadYandexDiskFolder("/");
            }
            else
            {
                _currentYandexAccount = null;
                lstYandexDiskFiles.Items.Clear();
                txtYandexDiskCurrentPath.Text = "Нет аккаунтов";
            }
        }

        private void BtnRemoveAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var accountToRemove = button.DataContext as YandexAccount;

            if (accountToRemove != null)
            {
                var result = MessageBox.Show($"Выйти из {accountToRemove.Login}?\nЛокальные файлы останутся, но токен доступа будет удален.",
                                             "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string tokenPath = Path.Combine(TokenDirectory, accountToRemove.Login);
                        if (Directory.Exists(tokenPath)) Directory.Delete(tokenPath, true);
                    }
                    catch { }

                    _yandexAccounts.Remove(accountToRemove);
                    if (_yandexAccounts.Count > 0) lstAccounts.SelectedIndex = 0;
                }
            }
            e.Handled = true;
        }


        private async void LoadYandexDiskFolder(string path)
        {
            if (_currentYandexAccount == null) return;

            txtYandexDiskCurrentPath.Text = path == "/" ? "Корень" : Path.GetFileName(path);
            txtYandexDiskCurrentPath.Tag = path;

            btnYandexDiskUp.IsEnabled = path != "/" && path != "disk:/";

            try
            {
                var files = await _yandexService.ListFilesAsync(_currentYandexAccount.Api, path);

                // Сортируем элементы (папки сначала, затем файлы по имени)
                var sortedFiles = files
                    .OrderByDescending(i => i.IsFolder)
                    .ThenBy(i => i.Name)
                    .ToList();

                lstYandexDiskFiles.ItemsSource = sortedFiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка Яндекс.Диск: {ex.Message}");
                lstYandexDiskFiles.ItemsSource = null;
            }
        }

        private void BtnYandexDiskUp_Click(object sender, RoutedEventArgs e)
        {
            if (_diskHistory.Count > 0)
            {
                var previousPath = _diskHistory.Pop();
                LoadYandexDiskFolder(previousPath);
            }
        }

        #endregion

        #region =================== УНИВЕРСАЛЬНОЕ ВЫДЕЛЕНИЕ ===================

        private static T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T) return (T)obj;
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void Universal_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            if (e.ClickCount == 2)
            {
                Universal_MouseDoubleClick(sender, e);
                e.Handled = true;
                return;
            }

            var source = e.OriginalSource as DependencyObject;
            var clickedItem = FindVisualParent<ListBoxItem>(source);

            _selectionStartPoint = e.GetPosition(listBox);
            _dragStartPoint = e.GetPosition(null);
            _clickedSelected = false;

            if (clickedItem != null)
            {
                if (clickedItem.IsSelected)
                {
                    _clickedSelected = true;
                    e.Handled = true;
                }
            }
            else
            {
                if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    listBox.SelectedItems.Clear();
                }
            }
        }

        private void Universal_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            if (_isSelecting && _rubberBandAdorner != null)
            {
                _rubberBandAdorner.UpdateSelection(e.GetPosition(listBox));

                // Компактная логика Live-выделения
                Rect rect = _rubberBandAdorner.GetSelectionRect();
                bool isCtrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

                foreach (var item in listBox.Items)
                {
                    if (listBox.ItemContainerGenerator.ContainerFromItem(item) is ListBoxItem container)
                    {
                        var bounds = container.TransformToVisual(listBox)
                                      .TransformBounds(new Rect(0, 0, container.ActualWidth, container.ActualHeight));
                        if (rect.IntersectsWith(bounds)) container.IsSelected = true;
                        else if (!isCtrl) container.IsSelected = false;
                    }
                }

                e.Handled = true;
                return;
            }

            if (e.LeftButton == MouseButtonState.Released)
            {
                _clickedSelected = false;
                return;
            }

            Point currentPos = e.GetPosition(null);
            Vector diff = _dragStartPoint - currentPos;

            if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                if (_clickedSelected && listBox.SelectedItems.Count > 0)
                {
                    DragDrop.DoDragDrop(listBox, listBox.SelectedItems.Cast<object>().ToList(), DragDropEffects.Copy);
                    _clickedSelected = false;
                    e.Handled = true;
                    return;
                }

                _isSelecting = true;
                if (!Keyboard.IsKeyDown(Key.LeftCtrl)) listBox.SelectedItems.Clear();

                _adornerLayer = AdornerLayer.GetAdornerLayer(listBox);
                if (_adornerLayer != null)
                {
                    _rubberBandAdorner = new RubberBandAdorner(listBox, _selectionStartPoint);
                    _adornerLayer.Add(_rubberBandAdorner);
                    listBox.CaptureMouse();
                }
                e.Handled = true;
            }
        }

        private void Universal_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            if (_isSelecting && _rubberBandAdorner != null)
            {
                _adornerLayer.Remove(_rubberBandAdorner);
                _rubberBandAdorner = null;
            }

            _isDragging = false;
            _isSelecting = false;
            _clickedSelected = false;
            if (listBox.IsMouseCaptured) listBox.ReleaseMouseCapture();
        }

        private void Universal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null || listBox.SelectedItem == null) return;

            if (listBox == lstWindowsFiles)
            {
                var item = listBox.SelectedItem as WindowsItem;
                if (item != null && item.IsFolder) LoadWindowsDirectory(item.FullPath);
            }
            else if (listBox == lstYandexDiskFiles)
            {
                var item = listBox.SelectedItem as YandexDiskItem;
                if (item != null && item.IsFolder)
                {
                    // Сохраняем текущий путь в историю
                    _diskHistory.Push(txtYandexDiskCurrentPath.Tag.ToString());
                    LoadYandexDiskFolder(item.Path);
                }
            }
        }

        #endregion

        #region =================== DRAG-N-DROP & ЗАГРУЗКА (Через Service) ===================

        private void LstWindowsFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<object>))) e.Effects = DragDropEffects.Copy;
            else e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void LstWindowsFiles_Drop(object sender, DragEventArgs e)
        {
            // 1. Куда бросили
            string targetPath = txtWindowsCurrentPath.Text;
            var element = e.OriginalSource as FrameworkElement;
            var dropItem = element?.DataContext as WindowsItem;
            if (dropItem != null && dropItem.IsFolder) targetPath = dropItem.FullPath;

            if (targetPath == "This PC")
            {
                MessageBox.Show("Выберите диск или папку для скачивания.");
                return;
            }

            // 2. Что бросили
            var draggedData = e.Data.GetData(typeof(List<object>)) as List<object>;
            if (draggedData == null) return;

            // Выбираем файлы Google Drive
            var filesToDownload = draggedData.OfType<YandexDiskItem>().Where(i => !i.IsFolder).ToList();

            if (filesToDownload.Count > 0)
            {
                ShowDownloadManager();
                foreach (var file in filesToDownload)
                {
                    StartDownload(file, targetPath);
                }
            }
        }

        private async void StartDownload(YandexDiskItem file, string targetPath)
        {
            ShowDownloadManager();

            await _downloadService.StartDownloadAsync(_currentYandexAccount, file, targetPath);

            if (txtWindowsCurrentPath.Text.Equals(targetPath, StringComparison.OrdinalIgnoreCase))
            {
                LoadWindowsDirectory(targetPath);
            }
        }

        private void ShowDownloadManager()
        {
            // Раскрываем список, если был свернут
            lstDownloads.Visibility = Visibility.Visible;
            btnTogglePopup.Content = "▼";

            // Открываем Popup
            if (!DownloadPopup.IsOpen) DownloadPopup.IsOpen = true;
            PositionDownloadPopup();
        }

        #endregion

        #region =================== POPUP LOGIC ===================

        private void PositionDownloadPopup()
        {
            if (DownloadPopup.IsOpen)
            {
                DownloadPopup.HorizontalOffset += 1;
                DownloadPopup.HorizontalOffset -= 1;
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e) => PositionDownloadPopup();
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => PositionDownloadPopup();

        private void BtnTogglePopup_Click(object sender, RoutedEventArgs e)
        {
            if (lstDownloads.Visibility == Visibility.Visible)
            {
                lstDownloads.Visibility = Visibility.Collapsed;
                btnTogglePopup.Content = "▲";
            }
            else
            {
                lstDownloads.Visibility = Visibility.Visible;
                btnTogglePopup.Content = "▼";
                PositionDownloadPopup();
            }
        }

        // Закрывает Popup, когда список пуст.
        private void ActiveDownloads_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_downloadService.ActiveDownloads.Count == 0 && DownloadPopup.IsOpen)
            {
                DownloadPopup.IsOpen = false;

            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region =================== КОНТЕКСТНОЕ МЕНЮ И ОПЕРАЦИИ (Через Services) ===================

        private ListBox GetActiveListBox(object sender)
        {
            if (sender is MenuItem menuItem)
            {
                var menu = menuItem.Parent as ContextMenu;
                if (menu != null)
                {
                    if (menu.PlacementTarget is ListBox lb) return lb;
                    if (menu.PlacementTarget is ListBoxItem lbi) return FindVisualParent<ListBox>(lbi);
                }
            }
            return null;
        }

        private void Universal_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null) return;

            var menu = this.Resources["FileSystemContextMenu"] as ContextMenu;
            if (menu == null) return;

            MenuItem itemCopy = null, itemCut = null, itemPaste = null, itemRename = null, itemDelete = null, itemOpen = null;

            foreach (var item in menu.Items)
            {
                if (item is MenuItem mi)
                {
                    if (mi.Name == "miCopy") itemCopy = mi;
                    else if (mi.Name == "miCut") itemCut = mi;
                    else if (mi.Name == "miPaste") itemPaste = mi;
                    else if (mi.Name == "miRename") itemRename = mi;
                    else if (mi.Name == "miDelete") itemDelete = mi;
                    else if (mi.Name == "miOpen") itemOpen = mi;
                }
            }

            int count = listBox.SelectedItems.Count;

            if (itemOpen != null) itemOpen.IsEnabled = count == 1;
            if (itemCopy != null) itemCopy.IsEnabled = count > 0;
            if (itemCut != null) itemCut.IsEnabled = count > 0;
            if (itemDelete != null) itemDelete.IsEnabled = count > 0;
            if (itemRename != null) itemRename.IsEnabled = count == 1;

            if (itemPaste != null)
            {
                bool canPaste = false;
                if (listBox == lstWindowsFiles)
                {
                    canPaste = Clipboard.ContainsFileDropList() || _diskClipboard.Count > 0;
                }
                else
                {
                    canPaste = _diskClipboard.Count > 0;
                }
                itemPaste.IsEnabled = canPaste;
            }
        }

        private void Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            Universal_MouseDoubleClick(GetActiveListBox(sender), null);
        }

        private async void Menu_Delete_Click(object sender, RoutedEventArgs e)
        {
            var listBox = GetActiveListBox(sender);
            if (listBox == null || listBox.SelectedItems.Count == 0) return;

            if (MessageBox.Show("Удалить выбранное?", "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            if (listBox == lstWindowsFiles)
            {
                foreach (WindowsItem item in listBox.SelectedItems.Cast<WindowsItem>().ToList())
                {
                    try { _localService.DeleteItem(item); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                LoadWindowsDirectory(txtWindowsCurrentPath.Text);
            }
            else // Удаление с Яндекс.Диска
            {
                foreach (YandexDiskItem item in listBox.SelectedItems.Cast<YandexDiskItem>().ToList())
                {
                    try
                    {
                        // Используем _yandexService.DeleteFileAsync с API и Путем (Path)
                        await _yandexService.DeleteFileAsync(_currentYandexAccount.Api, item.Path);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                LoadYandexDiskFolder(txtYandexDiskCurrentPath.Tag.ToString());
            }
            LoadYandexDiskFolder(txtYandexDiskCurrentPath.Tag.ToString());
        }

        private void Menu_Copy_Click(object sender, RoutedEventArgs e) => DoCopyOrCut(sender, false);
        private void Menu_Cut_Click(object sender, RoutedEventArgs e) => DoCopyOrCut(sender, true);

        private void DoCopyOrCut(object sender, bool isCut)
        {
            var listBox = GetActiveListBox(sender);
            if (listBox == null) return;

            if (listBox == lstWindowsFiles)
            {
                var paths = new System.Collections.Specialized.StringCollection();
                foreach (WindowsItem item in listBox.SelectedItems) paths.Add(item.FullPath);
                Clipboard.SetFileDropList(paths);
                _isWindowsCut = isCut;
            }
            else
            {
                _diskClipboard.Clear();

                foreach (YandexDiskItem item in listBox.SelectedItems) _diskClipboard.Add(item);
                _isDriveCut = isCut;
            }
        }

        private async void Menu_Paste_Click(object sender, RoutedEventArgs e)
        {
            var listBox = GetActiveListBox(sender);
            if (listBox == null) return;

            if (listBox == lstWindowsFiles)
            {
                string targetDir = txtWindowsCurrentPath.Text;
                if (targetDir == "This PC") return;

                // 1. Из Yandex (Скачивание)
                if (_diskClipboard.Count > 0)
                {
                    ShowDownloadManager();
                    foreach (var item in _diskClipboard) StartDownload(item, targetDir);
                    if (_isDriveCut) _diskClipboard.Clear();
                    return;
                }

                // 2. Из Windows (Копирование/Перемещение)
                if (Clipboard.ContainsFileDropList())
                {
                    var files = Clipboard.GetFileDropList();
                    foreach (string src in files)
                    {
                        string dest = Path.Combine(targetDir, Path.GetFileName(src));
                        try
                        {
                            if (Directory.Exists(src))
                            {
                                if (_isWindowsCut) Directory.Move(src, dest);
                                else MessageBox.Show("Копирование папок пока не реализовано.");
                            }
                            else if (File.Exists(src))
                            {
                                if (_isWindowsCut) File.Move(src, dest);
                                else File.Copy(src, dest, true);
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                    if (_isWindowsCut) { Clipboard.Clear(); _isWindowsCut = false; }
                    LoadWindowsDirectory(targetDir);
                }
            }
            else //  из Yandex 
            {
                string targetPath = txtYandexDiskCurrentPath.Tag.ToString();

                // 1. внутри ЯндексДиска (Копирование/Перемещение)
                if (_diskClipboard.Count > 0)
                {
                    foreach (YandexDiskItem item in _diskClipboard.Cast<YandexDiskItem>())
                    {
                        try
                        {
                            if (_isDriveCut)
                            {
                                // Cut: Move (Copy + Delete) 
                                await _yandexService.CopyFileAsync(_currentYandexAccount.Api, item.Path, targetPath);
                                await _yandexService.DeleteFileAsync(_currentYandexAccount.Api, item.Path);
                            }
                            else
                            {
                                // Copy
                                await _yandexService.CopyFileAsync(_currentYandexAccount.Api, item.Path, targetPath);
                            }
                        }
                        catch (Exception ex) { MessageBox.Show($"Ошибка API: {ex.Message}"); }
                    }
                    LoadYandexDiskFolder(targetPath);

                    if (_isDriveCut) _diskClipboard.Clear();  // Очищаем буфер после Cut
                }
            }

        }

        private void Menu_Rename_Click(object sender, RoutedEventArgs e)
        {
            var listBox = GetActiveListBox(sender);
            if (listBox == null || listBox.SelectedItems.Count != 1) return;

            if (listBox == lstWindowsFiles)
            {
                _renameSourceList = "Windows";
                _itemToRename = listBox.SelectedItem as WindowsItem;
                txtRenameInput.Text = ((WindowsItem)_itemToRename).Name;
            }
            else // Яндекс.Диск
            {
                _renameSourceList = "Yandex";
                _itemToRename = listBox.SelectedItem as YandexDiskItem;
                txtRenameInput.Text = ((YandexDiskItem)_itemToRename).Name;
            }
            RenamePopup.IsOpen = true;
            txtRenameInput.Focus();
            txtRenameInput.SelectAll();
        }

        private async void BtnRenameOk_Click(object sender, RoutedEventArgs e)
        {
            string newName = txtRenameInput.Text.Trim();
            if (string.IsNullOrEmpty(newName)) return;
            RenamePopup.IsOpen = false;

            try
            {
                if (_renameSourceList == "Windows")
                {
                    _localService.RenameItem((WindowsItem)_itemToRename, newName);
                    LoadWindowsDirectory(txtWindowsCurrentPath.Text);
                }
                // Проверка на "Yandex"
                else if (_renameSourceList == "Yandex")
                {
                    var item = (YandexDiskItem)_itemToRename;
                    await _yandexService.RenameFileAsync(_currentYandexAccount.Api, item.Path, newName);
                    LoadYandexDiskFolder(txtYandexDiskCurrentPath.Tag.ToString());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);
            LoadYandexDiskFolder(txtYandexDiskCurrentPath.Tag.ToString());
            }
        }

        private void BtnRenameCancel_Click(object sender, RoutedEventArgs e) => RenamePopup.IsOpen = false;

        #endregion
    }
}