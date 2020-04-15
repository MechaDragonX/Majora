using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Majora.Playback;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Majora
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private async Task<string> GetPath()
        {
            FileDialogFilter allFilesFilter = new FileDialogFilter()
            {
                Name = "All Supported Music Files",
                Extensions = new List<string>()
                {
                    "wav", "wave", "w64",
                    "flac", "ogg",
                    "mp3", "aac", "m4a",
                    "aiff", "au", "snd"
                }
            };

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(allFilesFilter);
            string[] result = await dialog.ShowAsync(this);
            return string.Join(" ", result);
        }
        private void Play(string path)
        {
            PlaybackController playbackController = new PlaybackController();
            playbackController.Initialize(path);
            playbackController.Play();
        }

        public async void OnClickerClicked(object sender, RoutedEventArgs e)
        {
            string path = await GetPath();
            Play(path);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Button clicker = this.Find<Button>("clicker");
        }
    }
}
