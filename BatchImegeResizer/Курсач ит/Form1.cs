using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач_ит
{
    public partial class Form1 : Form
    {
        private List<string> imageFiles;
        private int currentIndex;
        private string outputFolder;
        private Image originalImage;
        private Bitmap processedImage;

        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Инициализация комбобокса с входными форматами
            comboBox1.Items.AddRange(new string[] { "*.jpg", "*.png", "*.bmp", "*.tiff", "*.gif" });
            comboBox1.SelectedIndex = 0;

            // Инициализация комбобокса с качеством сжатия
            comboBoxQuality.Items.AddRange(new string[] { "Высокое (90%)", "Среднее (75%)", "Низкое (50%)", "Пользовательское" });
            comboBoxQuality.SelectedIndex = 1;
            numericQuality.Value = 75;
            numericQuality.Enabled = false;

            // Инициализация комбобокса с выходными форматами
            comboBoxFormat.Items.AddRange(new string[] { "JPG (*.jpg)", "PNG (*.png)", "BMP (*.bmp)", "TIFF (*.tiff)", "GIF (*.gif)" });
            comboBoxFormat.SelectedIndex = 0;

            // Инициализация предустановок размеров
            comboBoxPresets.Items.AddRange(new string[] {
                "Выберите предустановку...",
                "HD (1280x720)",
                "Full HD (1920x1080)",
                "4K (3840x2160)",
                "Instagram (1080x1080)",
                "Facebook (1200x630)",
                "Twitter (1024x512)",
                "VK (1200x800)"
            });
            comboBoxPresets.SelectedIndex = 0;

            // Установка начальных значений
            numWidth.Value = 800;
            numHeight.Value = 600;
            checkKeepAspectRatio.Checked = true;

            // Обновляем информацию о формате
            UpdateFormatInfo();
        }

        private void UpdateFormatInfo()
        {
            string format = GetOutputFormat();
            string qualityInfo = comboBoxQuality.Text;
            lblFormatInfo.Text = $"Формат: {comboBoxFormat.Text} | Качество: {qualityInfo}";
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tiff;*.gif";
                openFileDialog.Title = "Выберите изображения";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageFiles = new List<string>(openFileDialog.FileNames);
                    currentIndex = 0;

                    UpdateFilesList();
                    DisplayImage();
                }
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
                {
                    txtFolderPath.Text = folderBrowser.SelectedPath;

                    // Определяем фильтр для файлов
                    string filter = checkBox1.Checked ? "*.*" : comboBox1.Text;

                    string[] files = Directory.GetFiles(folderBrowser.SelectedPath, filter);

                    imageFiles = new List<string>();
                    foreach (string file in files)
                    {
                        string ext = Path.GetExtension(file).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" ||
                            ext == ".bmp" || ext == ".tiff" || ext == ".gif")
                        {
                            imageFiles.Add(file);
                        }
                    }

                    currentIndex = 0;
                    UpdateFilesList();

                    if (imageFiles.Count > 0)
                    {
                        DisplayImage();
                    }
                    else
                    {
                        pictureBox.Image = null;
                        textBox1.Text = "Изображения не найдены";
                        lblImageCount.Text = "0 изображений";
                    }
                }
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = folderBrowser.SelectedPath;
                    outputFolder = folderBrowser.SelectedPath;
                }
            }
        }

        private void UpdateFilesList()
        {
            listBoxFiles.Items.Clear();
            if (imageFiles != null)
            {
                foreach (string file in imageFiles)
                {
                    listBoxFiles.Items.Add(Path.GetFileName(file));
                }
                lblImageCount.Text = $"Найдено: {imageFiles.Count} изображений";
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex >= 0 && listBoxFiles.SelectedIndex < imageFiles.Count)
            {
                currentIndex = listBoxFiles.SelectedIndex;
                DisplayImage();
            }
        }

        private void DisplayImage()
        {
            if (imageFiles != null && imageFiles.Count > 0 && currentIndex < imageFiles.Count)
            {
                try
                {
                    string currentImage = imageFiles[currentIndex];

                    // Освобождаем предыдущее изображение
                    pictureBox.Image?.Dispose();
                    originalImage?.Dispose();
                    processedImage?.Dispose();

                    // Загружаем новое изображение
                    originalImage = Image.FromFile(currentImage);
                    pictureBox.Image = (Image)originalImage.Clone();
                    processedImage = null;

                    textBox1.Text = Path.GetFileName(currentImage);

                    // Обновляем информацию о размере оригинального изображения
                    lblOriginalSize.Text = $"Оригинал: {originalImage.Width}×{originalImage.Height}";

                    // Обновляем информацию о файле
                    lblFileInfo.Text = $"Файл: {currentImage}";

                    // Обновляем EXIF данные
                    DisplayExifData();

                    if (listBoxFiles.SelectedIndex != currentIndex)
                        listBoxFiles.SelectedIndex = currentIndex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplayExifData()
        {
            listBoxExif.Items.Clear();
            if (imageFiles == null || currentIndex >= imageFiles.Count) return;

            try
            {
                using (Image image = Image.FromFile(imageFiles[currentIndex]))
                {
                    // Получаем базовую информацию
                    listBoxExif.Items.Add($"Размер: {image.Width}×{image.Height}");
                    listBoxExif.Items.Add($"Формат: {image.RawFormat}");
                    listBoxExif.Items.Add($"Горизонтальное разрешение: {image.HorizontalResolution} dpi");
                    listBoxExif.Items.Add($"Вертикальное разрешение: {image.VerticalResolution} dpi");

                    // Получаем EXIF данные если есть
                    try
                    {
                        foreach (var prop in image.PropertyItems)
                        {
                            string propName = GetExifTagName(prop.Id);
                            string propValue = GetExifValue(prop);
                            if (!string.IsNullOrEmpty(propValue))
                            {
                                listBoxExif.Items.Add($"{propName}: {propValue}");
                            }
                        }
                    }
                    catch
                    {
                        // Игнорируем ошибки EXIF
                    }
                }
            }
            catch { }
        }

        private string GetExifTagName(int tagId)
        {
            switch (tagId)
            {
                case 0x010F:
                    return "Производитель";
                case 0x0110:
                    return "Модель камеры";
                case 0x0132:
                    return "Дата съемки";
                case 0x9003:
                    return "Дата оригинальной съемки";
                case 0x829A:
                    return "Выдержка";
                case 0x829D:
                    return "Диафрагма";
                case 0x8827:
                    return "ISO";
                case 0x920A:
                    return "Фокусное расстояние";
                default:
                    return $"Тег 0x{tagId:X4}";
            }
        }

        private string GetExifValue(System.Drawing.Imaging.PropertyItem prop)
        {
            try
            {
                switch (prop.Type)
                {
                    case 2: // ASCII
                        return Encoding.ASCII.GetString(prop.Value).Trim('\0');
                    case 3: // Short
                        return BitConverter.ToUInt16(prop.Value, 0).ToString();
                    case 4: // Long
                        return BitConverter.ToUInt32(prop.Value, 0).ToString();
                    case 5: // Rational
                        uint numerator = BitConverter.ToUInt32(prop.Value, 0);
                        uint denominator = BitConverter.ToUInt32(prop.Value, 4);
                        return denominator != 0 ? ((double)numerator / denominator).ToString("F2") : "0";
                    default:
                        return BitConverter.ToString(prop.Value);
                }
            }
            catch
            {
                return "Невозможно прочитать";
            }
        }

        private void btnResizeImages_Click(object sender, EventArgs e)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                MessageBox.Show("Сначала выберите изображения!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outputFolder))
            {
                MessageBox.Show("Выберите папку для сохранения!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                try
                {
                    Directory.CreateDirectory(outputFolder);
                }
                catch
                {
                    MessageBox.Show("Невозможно создать папку для сохранения!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                int processedCount = 0;
                int errorCount = 0;
                ProgressBar.Visible = true;
                ProgressBar.Value = 0;
                ProgressBar.Maximum = imageFiles.Count;

                foreach (string file in imageFiles)
                {
                    if (ProcessImageFile(file, (int)numWidth.Value, (int)numHeight.Value))
                        processedCount++;
                    else
                        errorCount++;

                    ProgressBar.Value++;
                    Application.DoEvents();
                }

                ProgressBar.Visible = false;

                string message = $"Обработка завершена!\nУспешно: {processedCount}\nОшибок: {errorCount}";
                MessageBox.Show(message, "Результат", MessageBoxButtons.OK,
                              errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ProgressBar.Visible = false;
                MessageBox.Show($"Ошибка при обработке: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveSingle_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Нет изображения для сохранения!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outputFolder))
            {
                MessageBox.Show("Выберите папку для сохранения!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string currentImagePath = imageFiles[currentIndex];

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    string outputFormat = GetOutputFormat();
                    string filter = GetFileFilter();

                    saveDialog.Filter = filter;
                    saveDialog.FileName = Path.GetFileNameWithoutExtension(currentImagePath) + "_processed" + outputFormat;
                    saveDialog.InitialDirectory = outputFolder;

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveProcessedImage(pictureBox.Image, saveDialog.FileName);

                        MessageBox.Show($"Изображение успешно сохранено!\n\nИсходный файл: {currentImagePath}\nСохранен как: {saveDialog.FileName}",
                                      "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Добавляем в лог
                        AddToFileLog($"Исходный: {currentImagePath}");
                        AddToFileLog($"Сохранен: {saveDialog.FileName}");
                        AddToFileLog($"Формат: {comboBoxFormat.Text}, Качество: {comboBoxQuality.Text}");
                        AddToFileLog("---");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ProcessImageFile(string filePath, int width, int height)
        {
            try
            {
                Image imageToProcess;

                if (pictureBox.Image != null && pictureBox.Image != originalImage && filePath == imageFiles[currentIndex])
                {
                    imageToProcess = pictureBox.Image;
                }
                else
                {
                    imageToProcess = Image.FromFile(filePath);
                }

                using (imageToProcess)
                {
                    Size newSize = CalculateNewSize(imageToProcess, width, height);

                    using (Bitmap resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb))
                    {
                        using (Graphics graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.DrawImage(imageToProcess, 0, 0, newSize.Width, newSize.Height);
                        }

                        string outputFormat = GetOutputFormat();
                        string fileName = Path.GetFileNameWithoutExtension(filePath) + "_processed" + outputFormat;
                        string newFilePath = Path.Combine(outputFolder, fileName);

                        AddToFileLog($"Исходный: {filePath}");
                        AddToFileLog($"Сохранен: {newFilePath}");
                        AddToFileLog($"Формат: {comboBoxFormat.Text}, Качество: {comboBoxQuality.Text}");
                        AddToFileLog("---");

                        ImageFormat format = GetImageFormat(outputFormat);
                        int quality = GetQualityValue();

                        if (outputFormat == ".jpg" || outputFormat == ".jpeg")
                        {
                            SaveJpegWithQuality(resizedImage, newFilePath, quality);
                        }
                        else
                        {
                            resizedImage.Save(newFilePath, format);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки файла {filePath}: {ex.Message}");
                return false;
            }
        }

        private void AddToFileLog(string message)
        {
            if (string.IsNullOrEmpty(outputFolder)) return;

            try
            {
                string logPath = Path.Combine(outputFolder, "conversion_log.txt");
                using (StreamWriter sw = new StreamWriter(logPath, true, Encoding.UTF8))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                }
            }
            catch
            {
                // Игнорируем ошибки записи лога
            }
        }

        private void SaveProcessedImage(Image image, string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            ImageFormat format = GetImageFormat(extension);
            int quality = GetQualityValue();

            if (format == ImageFormat.Jpeg)
            {
                SaveJpegWithQuality(new Bitmap(image), filePath, quality);
            }
            else
            {
                image.Save(filePath, format);
            }
        }

        private string GetFileFilter()
        {
            switch (comboBoxFormat.SelectedIndex)
            {
                case 0: return "JPEG Image|*.jpg";
                case 1: return "PNG Image|*.png";
                case 2: return "BMP Image|*.bmp";
                case 3: return "TIFF Image|*.tiff";
                case 4: return "GIF Image|*.gif";
                default: return "All Files|*.*";
            }
        }

        private string GetOutputFormat()
        {
            string selected = comboBoxFormat.Text;
            if (selected.Contains("JPG")) return ".jpg";
            if (selected.Contains("PNG")) return ".png";
            if (selected.Contains("BMP")) return ".bmp";
            if (selected.Contains("TIFF")) return ".tiff";
            if (selected.Contains("GIF")) return ".gif";
            return ".jpg";
        }

        private Size CalculateNewSize(Image originalImage, int targetWidth, int targetHeight)
        {
            if (!checkKeepAspectRatio.Checked)
                return new Size(targetWidth, targetHeight);

            double ratioX = (double)targetWidth / originalImage.Width;
            double ratioY = (double)targetHeight / originalImage.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalImage.Width * ratio);
            int newHeight = (int)(originalImage.Height * ratio);

            return new Size(newWidth, newHeight);
        }

        private int GetQualityValue()
        {
            switch (comboBoxQuality.SelectedIndex)
            {
                case 0: return 90;
                case 1: return 75;
                case 2: return 50;
                case 3: return (int)numericQuality.Value;
                default: return 75;
            }
        }

        private ImageFormat GetImageFormat(string format)
        {
            string formatLower = format.ToLower();
            if (formatLower == ".jpg" || formatLower == ".jpeg")
                return ImageFormat.Jpeg;
            else if (formatLower == ".png")
                return ImageFormat.Png;
            else if (formatLower == ".bmp")
                return ImageFormat.Bmp;
            else if (formatLower == ".tiff")
                return ImageFormat.Tiff;
            else if (formatLower == ".gif")
                return ImageFormat.Gif;
            else
                return ImageFormat.Jpeg;
        }

        private void SaveJpegWithQuality(Image image, string filePath, int quality)
        {
            var encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            var qualityParam = new System.Drawing.Imaging.EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                (long)quality);

            encoderParameters.Param[0] = qualityParam;

            var jpegCodec = GetEncoder(ImageFormat.Jpeg);
            if (jpegCodec != null)
            {
                image.Save(filePath, jpegCodec, encoderParameters);
            }
            else
            {
                image.Save(filePath, ImageFormat.Jpeg);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        // ФИЛЬТРЫ И РЕДАКТИРОВАНИЕ
        private void ApplyFilter(string filterType)
        {
            if (pictureBox.Image == null) return;

            Bitmap original = new Bitmap(pictureBox.Image);
            Bitmap filtered = new Bitmap(original.Width, original.Height);

            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    Color pixel = original.GetPixel(x, y);
                    Color newPixel = pixel;

                    switch (filterType)
                    {
                        case "Grayscale":
                            newPixel = ToGrayscale(pixel);
                            break;
                        case "Sepia":
                            newPixel = ToSepia(pixel);
                            break;
                        case "Invert":
                            newPixel = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                            break;
                        default:
                            newPixel = pixel;
                            break;
                    }

                    filtered.SetPixel(x, y, newPixel);
                }
            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = filtered;
            processedImage = filtered;
        }

        private Color ToGrayscale(Color color)
        {
            int gray = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
            return Color.FromArgb(gray, gray, gray);
        }

        private Color ToSepia(Color color)
        {
            int tr = (int)(color.R * 0.393 + color.G * 0.769 + color.B * 0.189);
            int tg = (int)(color.R * 0.349 + color.G * 0.686 + color.B * 0.168);
            int tb = (int)(color.R * 0.272 + color.G * 0.534 + color.B * 0.131);

            return Color.FromArgb(
                Math.Min(255, tr),
                Math.Min(255, tg),
                Math.Min(255, tb)
            );
        }

        private void AutoEnhance()
        {
            if (pictureBox.Image == null) return;

            Bitmap image = new Bitmap(pictureBox.Image);

            float brightness = 1.1f;
            float contrast = 1.2f;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = image.GetPixel(x, y);

                    int r = Math.Max(0, Math.Min(255, (int)(((pixel.R / 255.0f - 0.5f) * contrast + 0.5f) * 255 * brightness)));
                    int g = Math.Max(0, Math.Min(255, (int)(((pixel.G / 255.0f - 0.5f) * contrast + 0.5f) * 255 * brightness)));
                    int b = Math.Max(0, Math.Min(255, (int)(((pixel.B / 255.0f - 0.5f) * contrast + 0.5f) * 255 * brightness)));

                    image.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = image;
            processedImage = image;
        }

        private void AddWatermark()
        {
            if (pictureBox.Image == null) return;

            Bitmap image = new Bitmap(pictureBox.Image);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                string watermark = txtWatermark.Text;
                if (string.IsNullOrEmpty(watermark)) return;

                Font font = new Font("Arial", 36, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));

                SizeF textSize = graphics.MeasureString(watermark, font);
                PointF position = new PointF(
                    image.Width - textSize.Width - 10,
                    image.Height - textSize.Height - 10
                );

                graphics.DrawString(watermark, font, brush, position);
            }

            pictureBox.Image?.Dispose();
            pictureBox.Image = image;
            processedImage = image;
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = (Image)originalImage.Clone();
                processedImage = null;
            }
        }

        private void btnBatchRename_Click(object sender, EventArgs e)
        {
            if (imageFiles == null || imageFiles.Count == 0) return;

            string pattern = "image_{0:000}";
            if (!string.IsNullOrEmpty(txtRenamePattern.Text))
                pattern = txtRenamePattern.Text;

            try
            {
                for (int i = 0; i < imageFiles.Count; i++)
                {
                    string newName = string.Format(pattern, i + 1);
                    string extension = Path.GetExtension(imageFiles[i]);
                    string directory = Path.GetDirectoryName(imageFiles[i]);
                    string newPath = Path.Combine(directory, newName + extension);

                    int counter = 1;
                    while (File.Exists(newPath))
                    {
                        newPath = Path.Combine(directory, $"{newName}_{counter++}{extension}");
                    }

                    File.Move(imageFiles[i], newPath);
                    imageFiles[i] = newPath;
                }

                UpdateFilesList();
                DisplayImage();
                MessageBox.Show("Переименование завершено успешно!", "Информация",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка переименования: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ОБРАБОТЧИКИ СОБЫТИЙ ДЛЯ КНОПОК ФИЛЬТРОВ
        private void btnGrayscale_Click(object sender, EventArgs e) => ApplyFilter("Grayscale");
        private void btnSepia_Click(object sender, EventArgs e) => ApplyFilter("Sepia");
        private void btnInvert_Click(object sender, EventArgs e) => ApplyFilter("Invert");
        private void btnAutoEnhance_Click(object sender, EventArgs e) => AutoEnhance();
        private void btnAddWatermark_Click(object sender, EventArgs e) => AddWatermark();

        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxPresets.SelectedIndex)
            {
                case 1: // HD
                    numWidth.Value = 1280;
                    numHeight.Value = 720;
                    break;
                case 2: // Full HD
                    numWidth.Value = 1920;
                    numHeight.Value = 1080;
                    break;
                case 3: // 4K
                    numWidth.Value = 3840;
                    numHeight.Value = 2160;
                    break;
                case 4: // Instagram
                    numWidth.Value = 1080;
                    numHeight.Value = 1080;
                    break;
                case 5: // Facebook
                    numWidth.Value = 1200;
                    numHeight.Value = 630;
                    break;
                case 6: // Twitter
                    numWidth.Value = 1024;
                    numHeight.Value = 512;
                    break;
                case 7: // VK
                    numWidth.Value = 1200;
                    numHeight.Value = 800;
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    buttonPrevious_Click(null, null);
                    return true;
                case Keys.Right:
                    buttonNext_Click(null, null);
                    return true;
                case Keys.F5:
                    btnPreview_Click(null, null);
                    return true;
                case Keys.F9:
                    btnResizeImages_Click(null, null);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void comboBoxQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericQuality.Enabled = (comboBoxQuality.SelectedIndex == 3);
            UpdateFormatInfo();
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormatInfo();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                if (currentIndex > 0)
                {
                    currentIndex--;
                    DisplayImage();
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                if (currentIndex < imageFiles.Count - 1)
                {
                    currentIndex++;
                    DisplayImage();
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0 && currentIndex < imageFiles.Count)
            {
                try
                {
                    string currentImagePath = imageFiles[currentIndex];
                    using (Image originalImage = Image.FromFile(currentImagePath))
                    {
                        Size newSize = CalculateNewSize(originalImage, (int)numWidth.Value, (int)numHeight.Value);
                        using (Bitmap resizedImage = new Bitmap(newSize.Width, newSize.Height))
                        {
                            using (Graphics graphics = Graphics.FromImage(resizedImage))
                            {
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.DrawImage(originalImage, 0, 0, newSize.Width, newSize.Height);
                            }

                            previewPictureBox.Image?.Dispose();
                            previewPictureBox.Image = (Image)resizedImage.Clone();

                            lblNewSize.Text = $"Новый: {newSize.Width}×{newSize.Height}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при предварительном просмотре: {ex.Message}",
                                  "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox.Image?.Dispose();
            previewPictureBox.Image?.Dispose();
            originalImage?.Dispose();
            processedImage?.Dispose();
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex >= 0)
            {
                int selectedIndex = listBoxFiles.SelectedIndex;
                imageFiles.RemoveAt(selectedIndex);
                UpdateFilesList();

                if (imageFiles.Count > 0)
                {
                    currentIndex = Math.Min(selectedIndex, imageFiles.Count - 1);
                    DisplayImage();
                }
                else
                {
                    pictureBox.Image = null;
                    textBox1.Text = "Нет изображений";
                    lblOriginalSize.Text = "Оригинал: ";
                    lblNewSize.Text = "Новый: ";
                    lblFileInfo.Text = "Файл: ";
                }
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            imageFiles?.Clear();
            UpdateFilesList();
            pictureBox.Image = null;
            textBox1.Text = "Нет изображений";
            lblOriginalSize.Text = "Оригинал: ";
            lblNewSize.Text = "Новый: ";
            lblFileInfo.Text = "Файл: ";
        }

        private void checkKeepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0)
            {
                btnPreview_Click(sender, e);
            }
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0 && checkKeepAspectRatio.Checked)
            {
                btnPreview_Click(sender, e);
            }
        }

        private void numHeight_ValueChanged(object sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Count > 0 && checkKeepAspectRatio.Checked)
            {
                btnPreview_Click(sender, e);
            }
        }

        // Дополнительные методы для совместимости
        private void btnResizeImages_Click_1(object sender, EventArgs e)
        {
            btnResizeImages_Click(sender, e);
        }

        private void buttonPrevious_Click_1(object sender, EventArgs e)
        {
            buttonPrevious_Click(sender, e);
        }

        private void buttonNext_Click_1(object sender, EventArgs e)
        {
            buttonNext_Click(sender, e);
        }

        private void btnSelectFolder_Click_1(object sender, EventArgs e)
        {
            btnSelectFolder_Click(sender, e);
        }
    }
}