using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace muusicplayyeer
{
    public partial class Form1 : Form
    {
        private List<string> playlist = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPlaylistFromFile();
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.flac;*.m4a;*.wma|All files (*.*)|*.*";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        if (!playlist.Contains(filePath))
                        {
                            playlist.Add(filePath);
                            listBoxFavorites.Items.Add(Path.GetFileName(filePath));
                        }
                    }

                    SavePlaylistToFile();
                }
            }
        }

        private void buttonAddFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] supportedExtensions = { "*.mp3", "*.wav", "*.flac", "*.m4a", "*.wma" };

                    foreach (string extension in supportedExtensions)
                    {
                        string[] files = Directory.GetFiles(folderDialog.SelectedPath, extension, SearchOption.AllDirectories);

                        foreach (string filePath in files)
                        {
                            if (!playlist.Contains(filePath))
                            {
                                playlist.Add(filePath);
                                listBoxFavorites.Items.Add(Path.GetFileName(filePath));
                            }
                        }
                    }

                    SavePlaylistToFile();
                }
            }
        }

        private void buttonOpenPlayer_Click(object sender, EventArgs e)
        {
            if (listBoxFavorites.SelectedIndex >= 0 && listBoxFavorites.SelectedIndex < playlist.Count)
            {
                Form2 playerForm = new Form2();
                playerForm.Playlist = playlist;
                playerForm.CurrentTrackIndex = listBoxFavorites.SelectedIndex;
                playerForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Выберите песню из списка!");
            }
        }

        private void listBoxFavorites_DoubleClick(object sender, EventArgs e)
        {
            buttonOpenPlayer_Click(sender, e);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFavorites.SelectedIndex >= 0)
            {
                int selectedIndex = listBoxFavorites.SelectedIndex;
                playlist.RemoveAt(selectedIndex);
                listBoxFavorites.Items.RemoveAt(selectedIndex);
                SavePlaylistToFile();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePlaylistToFile();
        }

        private void SavePlaylistToFile()
        {
            try
            {
                System.IO.File.WriteAllLines("playlist.txt", playlist);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения плейлиста: {ex.Message}");
            }
        }

        private void LoadPlaylistFromFile()
        {
            try
            {
                if (System.IO.File.Exists("playlist.txt"))
                {
                    var savedPlaylist = System.IO.File.ReadAllLines("playlist.txt").Where(System.IO.File.Exists).ToList();
                    if (savedPlaylist.Any())
                    {
                        playlist.Clear();
                        listBoxFavorites.Items.Clear();

                        playlist.AddRange(savedPlaylist);
                        foreach (string filePath in playlist)
                        {
                            listBoxFavorites.Items.Add(Path.GetFileName(filePath));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки плейлиста: {ex.Message}");
            }
        }
    }
}