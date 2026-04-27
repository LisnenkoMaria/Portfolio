namespace muusicplayyeer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listBoxFavorites = new ListBox();
            buttonOpenPlayer = new Button();
            buttonAddFiles = new Button();
            buttonRemove = new Button();
            SuspendLayout();
            // 
            // listBoxFavorites
            // 
            listBoxFavorites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxFavorites.FormattingEnabled = true;
            listBoxFavorites.Location = new Point(25, 51);
            listBoxFavorites.Name = "listBoxFavorites";
            listBoxFavorites.Size = new Size(335, 264);
            listBoxFavorites.TabIndex = 0;
            listBoxFavorites.DoubleClick += listBoxFavorites_DoubleClick;
            // 
            // buttonOpenPlayer
            // 
            buttonOpenPlayer.Location = new Point(25, 356);
            buttonOpenPlayer.Name = "buttonOpenPlayer";
            buttonOpenPlayer.Size = new Size(335, 30);
            buttonOpenPlayer.TabIndex = 1;
            buttonOpenPlayer.Text = "Открыть плеер";
            buttonOpenPlayer.UseVisualStyleBackColor = true;
            buttonOpenPlayer.Click += buttonOpenPlayer_Click;
            // 
            // buttonAddFiles
            // 
            buttonAddFiles.Location = new Point(25, 20);
            buttonAddFiles.Name = "buttonAddFiles";
            buttonAddFiles.Size = new Size(335, 29);
            buttonAddFiles.TabIndex = 2;
            buttonAddFiles.Text = "Добавить файлы";
            buttonAddFiles.UseVisualStyleBackColor = true;
            buttonAddFiles.Click += buttonAddFiles_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(266, 321);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(94, 29);
            buttonRemove.TabIndex = 4;
            buttonRemove.Text = "Удалить";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(382, 553);
            Controls.Add(buttonRemove);
            Controls.Add(buttonAddFiles);
            Controls.Add(buttonOpenPlayer);
            Controls.Add(listBoxFavorites);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Favorites";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxFavorites;
        private Button buttonOpenPlayer;
        private Button buttonAddFiles;
        private Button buttonRemove;
    }
}
