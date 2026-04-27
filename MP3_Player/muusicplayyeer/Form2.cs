using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;

namespace muusicplayyeer
{
    public partial class Form2 : Form
    {
        public List<string> Playlist { get; set; }
        public int CurrentTrackIndex { get; set; }

        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;
        private bool isPlaying = false;

        public Form2()
        {
            InitializeComponent();
            InitializeAudio();
        }

        private void InitializeAudio()
        {
            waveOut = new WaveOutEvent();
            timerProgress.Interval = 100;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Playlist != null && Playlist.Count > 0 && CurrentTrackIndex >= 0)
            {
                LoadCurrentTrack();
            }
        }

        private void LoadCurrentTrack()
        {
            if (CurrentTrackIndex < 0 || CurrentTrackIndex >= Playlist.Count) return;

            string filePath = Playlist[CurrentTrackIndex];
            StopPlayback();

            try
            {
                audioFile = new AudioFileReader(filePath);
                waveOut.Init(audioFile);

                labelSongName.Text = Path.GetFileNameWithoutExtension(filePath);
                progressBarMusic.Maximum = (int)audioFile.TotalTime.TotalSeconds;
                progressBarMusic.Value = 0;

                pictureBoxAlbum.Image = LoadAlbumCover(filePath);
                PlayCurrentTrack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}");
            }
        }

        private Image LoadAlbumCover(string songFilePath)
        {
            try
            {
                Image mp3Cover = GetAlbumArtFromFile(songFilePath);
                if (mp3Cover != null)
                    return mp3Cover;

                string songName = Path.GetFileNameWithoutExtension(songFilePath);
                string coversFolder = Path.Combine(Application.StartupPath, "AlbumCovers");

                if (Directory.Exists(coversFolder))
                {
                    string[] supportedImageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.bmp" };

                    foreach (string extension in supportedImageExtensions)
                    {
                        string[] imageFiles = Directory.GetFiles(coversFolder, extension);
                        foreach (string imageFile in imageFiles)
                        {
                            string imageName = Path.GetFileNameWithoutExtension(imageFile);
                            if (imageName.Equals(songName, StringComparison.OrdinalIgnoreCase))
                            {
                                return Image.FromFile(imageFile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки обложки: {ex.Message}");
            }

            return CreateDefaultAlbumArt();
        }

        private Image GetAlbumArtFromFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    if (Path.GetExtension(filePath).ToLower() == ".mp3")
                    {
                        var file = TagLib.File.Create(filePath);
                        var pictures = file.Tag.Pictures;
                        if (pictures.Length > 0)
                        {
                            var picture = pictures[0];
                            using (MemoryStream ms = new MemoryStream(picture.Data.Data))
                            {
                                return Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        private Image CreateDefaultAlbumArt()
        {
            Bitmap bmp = new Bitmap(200, 200);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightBlue);
                using (Font font = new Font("Arial", 12))
                {
                    g.DrawString("No Album Art", font, Brushes.Black, 45, 90);
                }
            }
            return bmp;
        }

        private void PlayCurrentTrack()
        {
            if (audioFile != null)
            {
                waveOut.Play();
                isPlaying = true;
                buttonPlayPause.Text = "⏸";
                timerProgress.Start();
            }
        }

        private void StopPlayback()
        {
            timerProgress.Stop();
            waveOut.Stop();
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            isPlaying = false;
            buttonPlayPause.Text = "▶";
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            if (audioFile != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                progressBarMusic.Value = (int)audioFile.CurrentTime.TotalSeconds;
                labelCurrentTime.Text = audioFile.CurrentTime.ToString(@"mm\:ss");
                labelTotalTime.Text = audioFile.TotalTime.ToString(@"mm\:ss");

                if (audioFile.CurrentTime >= audioFile.TotalTime)
                {
                    NextTrack();
                }
            }
        }

        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            if (audioFile == null && Playlist != null && Playlist.Count > 0)
            {
                LoadCurrentTrack();
                return;
            }

            if (isPlaying)
            {
                waveOut.Pause();
                isPlaying = false;
                buttonPlayPause.Text = "▶";
                timerProgress.Stop();
            }
            else
            {
                waveOut.Play();
                isPlaying = true;
                buttonPlayPause.Text = "⏸";
                timerProgress.Start();
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (Playlist == null || Playlist.Count == 0) return;

            CurrentTrackIndex--;
            if (CurrentTrackIndex < 0) CurrentTrackIndex = Playlist.Count - 1;
            LoadCurrentTrack();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            NextTrack();
        }

        private void NextTrack()
        {
            if (Playlist == null || Playlist.Count == 0) return;

            CurrentTrackIndex++;
            if (CurrentTrackIndex >= Playlist.Count) CurrentTrackIndex = 0;
            LoadCurrentTrack();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            StopPlayback();

            if (Playlist != null && Playlist.Count > 0)
            {
                System.IO.File.WriteAllLines("playlist.txt", Playlist);
            }

            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    form.Show();
                    break;
                }
            }
            this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPlayback();

            if (Playlist != null && Playlist.Count > 0)
            {
                System.IO.File.WriteAllLines("playlist.txt", Playlist);
            }

            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    form.Show();
                    break;
                }
            }

            waveOut?.Dispose();
        }

        private void progressBarMusic_Click(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                var mousePos = progressBarMusic.PointToClient(Cursor.Position);
                float percent = (float)mousePos.X / progressBarMusic.Width;
                audioFile.CurrentTime = TimeSpan.FromSeconds(percent * audioFile.TotalTime.TotalSeconds);
            }
        }
    }
}