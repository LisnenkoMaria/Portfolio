namespace muusicplayyeer
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            pictureBoxAlbum = new PictureBox();
            labelSongName = new Label();
            buttonPlayPause = new Button();
            buttonPrevious = new Button();
            buttonNext = new Button();
            buttonBack = new Button();
            progressBarMusic = new ProgressBar();
            timerProgress = new System.Windows.Forms.Timer(components);
            labelArtist = new Label();
            labelCurrentTime = new Label();
            labelTotalTime = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlbum).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxAlbum
            // 
            pictureBoxAlbum.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAlbum.Location = new Point(80, 58);
            pictureBoxAlbum.Name = "pictureBoxAlbum";
            pictureBoxAlbum.Size = new Size(200, 200);
            pictureBoxAlbum.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAlbum.TabIndex = 0;
            pictureBoxAlbum.TabStop = false;
            // 
            // labelSongName
            // 
            labelSongName.AutoSize = true;
            labelSongName.Location = new Point(135, 261);
            labelSongName.Name = "labelSongName";
            labelSongName.Size = new Size(116, 20);
            labelSongName.TabIndex = 1;
            labelSongName.Text = "labelSongName";
            // 
            // buttonPlayPause
            // 
            buttonPlayPause.BackColor = Color.Transparent;
            buttonPlayPause.BackgroundImage = (Image)resources.GetObject("buttonPlayPause.BackgroundImage");
            buttonPlayPause.BackgroundImageLayout = ImageLayout.Stretch;
            buttonPlayPause.FlatAppearance.BorderSize = 0;
            buttonPlayPause.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonPlayPause.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonPlayPause.FlatStyle = FlatStyle.Flat;
            buttonPlayPause.ForeColor = Color.WhiteSmoke;
            buttonPlayPause.Location = new Point(125, 292);
            buttonPlayPause.Name = "buttonPlayPause";
            buttonPlayPause.Size = new Size(126, 101);
            buttonPlayPause.TabIndex = 2;
            buttonPlayPause.UseVisualStyleBackColor = false;
            buttonPlayPause.Click += buttonPlayPause_Click;
            // 
            // buttonPrevious
            // 
            buttonPrevious.BackColor = Color.Transparent;
            buttonPrevious.BackgroundImage = (Image)resources.GetObject("buttonPrevious.BackgroundImage");
            buttonPrevious.BackgroundImageLayout = ImageLayout.Stretch;
            buttonPrevious.FlatAppearance.BorderSize = 0;
            buttonPrevious.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonPrevious.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonPrevious.FlatStyle = FlatStyle.Flat;
            buttonPrevious.Location = new Point(3, 281);
            buttonPrevious.Name = "buttonPrevious";
            buttonPrevious.Size = new Size(126, 117);
            buttonPrevious.TabIndex = 3;
            buttonPrevious.UseVisualStyleBackColor = false;
            buttonPrevious.Click += buttonPrevious_Click;
            // 
            // buttonNext
            // 
            buttonNext.BackColor = Color.Transparent;
            buttonNext.BackgroundImage = (Image)resources.GetObject("buttonNext.BackgroundImage");
            buttonNext.BackgroundImageLayout = ImageLayout.Stretch;
            buttonNext.FlatAppearance.BorderSize = 0;
            buttonNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonNext.FlatStyle = FlatStyle.Flat;
            buttonNext.Location = new Point(242, 284);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(135, 111);
            buttonNext.TabIndex = 4;
            buttonNext.UseVisualStyleBackColor = false;
            buttonNext.Click += buttonNext_Click;
            // 
            // buttonBack
            // 
            buttonBack.Location = new Point(283, 12);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(94, 29);
            buttonBack.TabIndex = 5;
            buttonBack.Text = "← Назад";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // progressBarMusic
            // 
            progressBarMusic.ForeColor = Color.LightCoral;
            progressBarMusic.Location = new Point(38, 399);
            progressBarMusic.Name = "progressBarMusic";
            progressBarMusic.Size = new Size(300, 20);
            progressBarMusic.Style = ProgressBarStyle.Continuous;
            progressBarMusic.TabIndex = 6;
            progressBarMusic.Click += progressBarMusic_Click;
            // 
            // timerProgress
            // 
            timerProgress.Interval = 1000;
            timerProgress.Tick += timerProgress_Tick;
            // 
            // labelArtist
            // 
            labelArtist.AutoSize = true;
            labelArtist.Location = new Point(276, 261);
            labelArtist.Name = "labelArtist";
            labelArtist.Size = new Size(101, 20);
            labelArtist.TabIndex = 7;
            labelArtist.Text = "Исполнитель";
            // 
            // labelCurrentTime
            // 
            labelCurrentTime.AutoSize = true;
            labelCurrentTime.Location = new Point(50, 422);
            labelCurrentTime.Name = "labelCurrentTime";
            labelCurrentTime.Size = new Size(44, 20);
            labelCurrentTime.TabIndex = 8;
            labelCurrentTime.Text = "00:00";
            // 
            // labelTotalTime
            // 
            labelTotalTime.AutoSize = true;
            labelTotalTime.Location = new Point(306, 422);
            labelTotalTime.Name = "labelTotalTime";
            labelTotalTime.Size = new Size(44, 20);
            labelTotalTime.TabIndex = 9;
            labelTotalTime.Text = "00:00";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(382, 553);
            Controls.Add(labelTotalTime);
            Controls.Add(labelCurrentTime);
            Controls.Add(labelArtist);
            Controls.Add(progressBarMusic);
            Controls.Add(buttonBack);
            Controls.Add(buttonNext);
            Controls.Add(buttonPrevious);
            Controls.Add(buttonPlayPause);
            Controls.Add(labelSongName);
            Controls.Add(pictureBoxAlbum);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form2";
            Text = "Playing now";
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlbum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxAlbum;
        private Label labelSongName;
        private Button buttonPlayPause;
        private Button buttonPrevious;
        private Button buttonNext;
        private Button buttonBack;
        private ProgressBar progressBarMusic;
        private System.Windows.Forms.Timer timerProgress;
        private Label labelArtist;
        private Label labelCurrentTime;
        private Label labelTotalTime;
    }
}