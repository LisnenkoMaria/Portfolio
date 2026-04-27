namespace Курсач_ит
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.btnResizeImages = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.lblOriginalSize = new System.Windows.Forms.Label();
            this.lblNewSize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericQuality = new System.Windows.Forms.NumericUpDown();
            this.comboBoxQuality = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.checkKeepAspectRatio = new System.Windows.Forms.CheckBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.btnAutoEnhance = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnSepia = new System.Windows.Forms.Button();
            this.btnGrayscale = new System.Windows.Forms.Button();
            this.groupBoxWatermark = new System.Windows.Forms.GroupBox();
            this.btnAddWatermark = new System.Windows.Forms.Button();
            this.txtWatermark = new System.Windows.Forms.TextBox();
            this.groupBoxPresets = new System.Windows.Forms.GroupBox();
            this.comboBoxPresets = new System.Windows.Forms.ComboBox();
            this.groupBoxExif = new System.Windows.Forms.GroupBox();
            this.listBoxExif = new System.Windows.Forms.ListBox();
            this.groupBoxRename = new System.Windows.Forms.GroupBox();
            this.btnBatchRename = new System.Windows.Forms.Button();
            this.txtRenamePattern = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveSingle = new System.Windows.Forms.Button();
            this.lblFormatInfo = new System.Windows.Forms.Label();
            this.lblFileInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuality)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            this.groupBoxWatermark.SuspendLayout();
            this.groupBoxPresets.SuspendLayout();
            this.groupBoxExif.SuspendLayout();
            this.groupBoxRename.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(12, 12);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(300, 20);
            this.txtFolderPath.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(318, 10);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Папка";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(60, 26);
            this.numWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(80, 20);
            this.numWidth.TabIndex = 2;
            this.numWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.numWidth.ValueChanged += new System.EventHandler(this.numWidth_ValueChanged);
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(200, 26);
            this.numHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(80, 20);
            this.numHeight.TabIndex = 3;
            this.numHeight.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numHeight.ValueChanged += new System.EventHandler(this.numHeight_ValueChanged);
            // 
            // btnResizeImages
            // 
            this.btnResizeImages.BackColor = System.Drawing.Color.SteelBlue;
            this.btnResizeImages.ForeColor = System.Drawing.Color.White;
            this.btnResizeImages.Location = new System.Drawing.Point(12, 400);
            this.btnResizeImages.Name = "btnResizeImages";
            this.btnResizeImages.Size = new System.Drawing.Size(381, 35);
            this.btnResizeImages.TabIndex = 4;
            this.btnResizeImages.Text = "Пакетное изменение размеров";
            this.btnResizeImages.UseVisualStyleBackColor = false;
            this.btnResizeImages.Click += new System.EventHandler(this.btnResizeImages_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(804, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(573, 318);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(887, 345);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 6;
            this.buttonPrevious.Text = "Назад";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(1219, 344);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 7;
            this.buttonNext.Text = "Вперед";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(980, 347);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(219, 20);
            this.textBox1.TabIndex = 8;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 9;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(133, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(136, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Все форматы файлов";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(300, 24);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 11;
            this.btnPreview.Text = "Превью";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPictureBox.Location = new System.Drawing.Point(804, 390);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(573, 382);
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewPictureBox.TabIndex = 12;
            this.previewPictureBox.TabStop = false;
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(12, 770);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(0, 13);
            this.lblImageCount.TabIndex = 13;
            // 
            // lblOriginalSize
            // 
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.Location = new System.Drawing.Point(410, 746);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(0, 13);
            this.lblOriginalSize.TabIndex = 14;
            // 
            // lblNewSize
            // 
            this.lblNewSize.AutoSize = true;
            this.lblNewSize.Location = new System.Drawing.Point(410, 770);
            this.lblNewSize.Name = "lblNewSize";
            this.lblNewSize.Size = new System.Drawing.Size(0, 13);
            this.lblNewSize.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numWidth);
            this.groupBox1.Controls.Add(this.numHeight);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Location = new System.Drawing.Point(12, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 60);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Размеры";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Высота:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Ширина:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 45);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Форматы файлов (входные)";
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new System.Drawing.Point(399, 10);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(100, 23);
            this.btnSelectFiles.TabIndex = 18;
            this.btnSelectFiles.Text = "Выбрать файлы";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(410, 40);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(378, 95);
            this.listBoxFiles.TabIndex = 19;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(410, 140);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveSelected.TabIndex = 20;
            this.btnRemoveSelected.Text = "Удалить выбранное";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(540, 140);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(100, 23);
            this.btnClearList.TabIndex = 21;
            this.btnClearList.Text = "Очистить список";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericQuality);
            this.groupBox3.Controls.Add(this.comboBoxQuality);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 50);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Качество сжатия (JPEG)";
            // 
            // numericQuality
            // 
            this.numericQuality.Enabled = false;
            this.numericQuality.Location = new System.Drawing.Point(300, 19);
            this.numericQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericQuality.Name = "numericQuality";
            this.numericQuality.Size = new System.Drawing.Size(75, 20);
            this.numericQuality.TabIndex = 2;
            this.numericQuality.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // comboBoxQuality
            // 
            this.comboBoxQuality.FormattingEnabled = true;
            this.comboBoxQuality.Location = new System.Drawing.Point(60, 19);
            this.comboBoxQuality.Name = "comboBoxQuality";
            this.comboBoxQuality.Size = new System.Drawing.Size(150, 21);
            this.comboBoxQuality.TabIndex = 1;
            this.comboBoxQuality.SelectedIndexChanged += new System.EventHandler(this.comboBoxQuality_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Качество:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxFormat);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(12, 290);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(381, 50);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Формат сохранения";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(60, 19);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(150, 21);
            this.comboBoxFormat.TabIndex = 1;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Формат:";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(12, 40);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(300, 20);
            this.txtOutputFolder.TabIndex = 24;
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(318, 38);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutputFolder.TabIndex = 25;
            this.btnSelectOutputFolder.Text = "Папка вывода";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // checkKeepAspectRatio
            // 
            this.checkKeepAspectRatio.AutoSize = true;
            this.checkKeepAspectRatio.Checked = true;
            this.checkKeepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkKeepAspectRatio.Location = new System.Drawing.Point(12, 350);
            this.checkKeepAspectRatio.Name = "checkKeepAspectRatio";
            this.checkKeepAspectRatio.Size = new System.Drawing.Size(136, 17);
            this.checkKeepAspectRatio.TabIndex = 26;
            this.checkKeepAspectRatio.Text = "Сохранять пропорции";
            this.checkKeepAspectRatio.UseVisualStyleBackColor = true;
            this.checkKeepAspectRatio.CheckedChanged += new System.EventHandler(this.checkKeepAspectRatio_CheckedChanged);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 380);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(381, 15);
            this.ProgressBar.TabIndex = 27;
            this.ProgressBar.Visible = false;
            // 
            // groupBoxFilters
            // 
            this.groupBoxFilters.Controls.Add(this.btnResetFilters);
            this.groupBoxFilters.Controls.Add(this.btnAutoEnhance);
            this.groupBoxFilters.Controls.Add(this.btnInvert);
            this.groupBoxFilters.Controls.Add(this.btnSepia);
            this.groupBoxFilters.Controls.Add(this.btnGrayscale);
            this.groupBoxFilters.Location = new System.Drawing.Point(410, 350);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(378, 50);
            this.groupBoxFilters.TabIndex = 28;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Фильтры изображения";
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.Location = new System.Drawing.Point(297, 19);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(75, 23);
            this.btnResetFilters.TabIndex = 4;
            this.btnResetFilters.Text = "Сброс";
            this.btnResetFilters.UseVisualStyleBackColor = true;
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // btnAutoEnhance
            // 
            this.btnAutoEnhance.Location = new System.Drawing.Point(226, 19);
            this.btnAutoEnhance.Name = "btnAutoEnhance";
            this.btnAutoEnhance.Size = new System.Drawing.Size(65, 23);
            this.btnAutoEnhance.TabIndex = 3;
            this.btnAutoEnhance.Text = "Улучшить";
            this.btnAutoEnhance.UseVisualStyleBackColor = true;
            this.btnAutoEnhance.Click += new System.EventHandler(this.btnAutoEnhance_Click);
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(155, 19);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(65, 23);
            this.btnInvert.TabIndex = 2;
            this.btnInvert.Text = "Инверт";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // btnSepia
            // 
            this.btnSepia.Location = new System.Drawing.Point(84, 19);
            this.btnSepia.Name = "btnSepia";
            this.btnSepia.Size = new System.Drawing.Size(65, 23);
            this.btnSepia.TabIndex = 1;
            this.btnSepia.Text = "Сепия";
            this.btnSepia.UseVisualStyleBackColor = true;
            this.btnSepia.Click += new System.EventHandler(this.btnSepia_Click);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.Location = new System.Drawing.Point(6, 19);
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(72, 23);
            this.btnGrayscale.TabIndex = 0;
            this.btnGrayscale.Text = "Ч/Б";
            this.btnGrayscale.UseVisualStyleBackColor = true;
            this.btnGrayscale.Click += new System.EventHandler(this.btnGrayscale_Click);
            // 
            // groupBoxWatermark
            // 
            this.groupBoxWatermark.Controls.Add(this.btnAddWatermark);
            this.groupBoxWatermark.Controls.Add(this.txtWatermark);
            this.groupBoxWatermark.Location = new System.Drawing.Point(410, 290);
            this.groupBoxWatermark.Name = "groupBoxWatermark";
            this.groupBoxWatermark.Size = new System.Drawing.Size(378, 50);
            this.groupBoxWatermark.TabIndex = 29;
            this.groupBoxWatermark.TabStop = false;
            this.groupBoxWatermark.Text = "Водяной знак";
            // 
            // btnAddWatermark
            // 
            this.btnAddWatermark.Location = new System.Drawing.Point(297, 19);
            this.btnAddWatermark.Name = "btnAddWatermark";
            this.btnAddWatermark.Size = new System.Drawing.Size(75, 23);
            this.btnAddWatermark.TabIndex = 1;
            this.btnAddWatermark.Text = "Добавить";
            this.btnAddWatermark.UseVisualStyleBackColor = true;
            this.btnAddWatermark.Click += new System.EventHandler(this.btnAddWatermark_Click);
            // 
            // txtWatermark
            // 
            this.txtWatermark.Location = new System.Drawing.Point(6, 21);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(285, 20);
            this.txtWatermark.TabIndex = 0;
            this.txtWatermark.Text = "Ваш текст";
            // 
            // groupBoxPresets
            // 
            this.groupBoxPresets.Controls.Add(this.comboBoxPresets);
            this.groupBoxPresets.Location = new System.Drawing.Point(12, 350);
            this.groupBoxPresets.Name = "groupBoxPresets";
            this.groupBoxPresets.Size = new System.Drawing.Size(381, 45);
            this.groupBoxPresets.TabIndex = 30;
            this.groupBoxPresets.TabStop = false;
            this.groupBoxPresets.Text = "Предустановки размеров";
            // 
            // comboBoxPresets
            // 
            this.comboBoxPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPresets.FormattingEnabled = true;
            this.comboBoxPresets.Location = new System.Drawing.Point(6, 16);
            this.comboBoxPresets.Name = "comboBoxPresets";
            this.comboBoxPresets.Size = new System.Drawing.Size(369, 21);
            this.comboBoxPresets.TabIndex = 0;
            this.comboBoxPresets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPresets_SelectedIndexChanged);
            // 
            // groupBoxExif
            // 
            this.groupBoxExif.Controls.Add(this.listBoxExif);
            this.groupBoxExif.Location = new System.Drawing.Point(410, 170);
            this.groupBoxExif.Name = "groupBoxExif";
            this.groupBoxExif.Size = new System.Drawing.Size(378, 115);
            this.groupBoxExif.TabIndex = 31;
            this.groupBoxExif.TabStop = false;
            this.groupBoxExif.Text = "Метаданные EXIF";
            // 
            // listBoxExif
            // 
            this.listBoxExif.FormattingEnabled = true;
            this.listBoxExif.Location = new System.Drawing.Point(6, 19);
            this.listBoxExif.Name = "listBoxExif";
            this.listBoxExif.Size = new System.Drawing.Size(366, 82);
            this.listBoxExif.TabIndex = 0;
            // 
            // groupBoxRename
            // 
            this.groupBoxRename.Controls.Add(this.btnBatchRename);
            this.groupBoxRename.Controls.Add(this.txtRenamePattern);
            this.groupBoxRename.Controls.Add(this.label5);
            this.groupBoxRename.Location = new System.Drawing.Point(12, 290);
            this.groupBoxRename.Name = "groupBoxRename";
            this.groupBoxRename.Size = new System.Drawing.Size(381, 50);
            this.groupBoxRename.TabIndex = 33;
            this.groupBoxRename.TabStop = false;
            this.groupBoxRename.Text = "Пакетное переименование";
            // 
            // btnBatchRename
            // 
            this.btnBatchRename.Location = new System.Drawing.Point(300, 17);
            this.btnBatchRename.Name = "btnBatchRename";
            this.btnBatchRename.Size = new System.Drawing.Size(75, 23);
            this.btnBatchRename.TabIndex = 2;
            this.btnBatchRename.Text = "Переименовать";
            this.btnBatchRename.UseVisualStyleBackColor = true;
            this.btnBatchRename.Click += new System.EventHandler(this.btnBatchRename_Click);
            // 
            // txtRenamePattern
            // 
            this.txtRenamePattern.Location = new System.Drawing.Point(60, 19);
            this.txtRenamePattern.Name = "txtRenamePattern";
            this.txtRenamePattern.Size = new System.Drawing.Size(234, 20);
            this.txtRenamePattern.TabIndex = 1;
            this.txtRenamePattern.Text = "image_{0:000}";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Шаблон:";
            // 
            // btnSaveSingle
            // 
            this.btnSaveSingle.BackColor = System.Drawing.Color.LightGreen;
            this.btnSaveSingle.Location = new System.Drawing.Point(410, 400);
            this.btnSaveSingle.Name = "btnSaveSingle";
            this.btnSaveSingle.Size = new System.Drawing.Size(378, 35);
            this.btnSaveSingle.TabIndex = 34;
            this.btnSaveSingle.Text = "Сохранить текущее изображение";
            this.btnSaveSingle.UseVisualStyleBackColor = false;
            this.btnSaveSingle.Click += new System.EventHandler(this.btnSaveSingle_Click);
            // 
            // lblFormatInfo
            // 
            this.lblFormatInfo.AutoSize = true;
            this.lblFormatInfo.Location = new System.Drawing.Point(12, 70);
            this.lblFormatInfo.Name = "lblFormatInfo";
            this.lblFormatInfo.Size = new System.Drawing.Size(0, 13);
            this.lblFormatInfo.TabIndex = 35;
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.AutoSize = true;
            this.lblFileInfo.Location = new System.Drawing.Point(12, 90);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(0, 13);
            this.lblFileInfo.TabIndex = 36;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 805);
            this.Controls.Add(this.lblFileInfo);
            this.Controls.Add(this.lblFormatInfo);
            this.Controls.Add(this.btnSaveSingle);
            this.Controls.Add(this.groupBoxRename);
            this.Controls.Add(this.groupBoxExif);
            this.Controls.Add(this.groupBoxPresets);
            this.Controls.Add(this.groupBoxWatermark);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.checkKeepAspectRatio);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblNewSize);
            this.Controls.Add(this.lblOriginalSize);
            this.Controls.Add(this.lblImageCount);
            this.Controls.Add(this.previewPictureBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnResizeImages);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Name = "Form1";
            this.Text = "Пакетное изменение размеров изображений - Курсовая работа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuality)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxWatermark.ResumeLayout(false);
            this.groupBoxWatermark.PerformLayout();
            this.groupBoxPresets.ResumeLayout(false);
            this.groupBoxExif.ResumeLayout(false);
            this.groupBoxRename.ResumeLayout(false);
            this.groupBoxRename.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Button btnResizeImages;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Label lblImageCount;
        private System.Windows.Forms.Label lblOriginalSize;
        private System.Windows.Forms.Label lblNewSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericQuality;
        private System.Windows.Forms.ComboBox comboBoxQuality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.CheckBox checkKeepAspectRatio;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Button btnResetFilters;
        private System.Windows.Forms.Button btnAutoEnhance;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnSepia;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.GroupBox groupBoxWatermark;
        private System.Windows.Forms.Button btnAddWatermark;
        private System.Windows.Forms.TextBox txtWatermark;
        private System.Windows.Forms.GroupBox groupBoxPresets;
        private System.Windows.Forms.ComboBox comboBoxPresets;
        private System.Windows.Forms.GroupBox groupBoxExif;
        private System.Windows.Forms.ListBox listBoxExif;
        private System.Windows.Forms.GroupBox groupBoxRename;
        private System.Windows.Forms.Button btnBatchRename;
        private System.Windows.Forms.TextBox txtRenamePattern;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveSingle;
        private System.Windows.Forms.Label lblFormatInfo;
        private System.Windows.Forms.Label lblFileInfo;
    }
}