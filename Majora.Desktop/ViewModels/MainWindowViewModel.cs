using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Majora.Playback;
using Majora.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Majora.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static PlaybackController playbackController = null;
        private static readonly FileDialogFilter allFilesFilter = new FileDialogFilter()
        {
            Name = "All Supported Music Files",
            Extensions = new List<string>()
            {
                "3gp", "a52", "a52b", "aac", "alac", "asf", "atrc", "atrc", "au",
                "cook", "dnet", "dts", "flac", "m4a", "mp3", "mpc", "mpga", "ogg",
                "opus", "raac", "racp", "ralf", "sipr", "spex", "tac", "tta", "vorb",
                "wav", "wma", "wma1", "wma2", "xa"
            }
        };

        public string PlayPause { get; set; }
        public string Mute { get; set; }
        public Bitmap Cover { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }

        public MainWindowViewModel()
        {
            PlayPause = "Play";
            Mute = "Mute";
            Cover = null;
            Album = "";
            Artist = "";
            Title = "";

            OpenFile = ReactiveCommand.Create(OpenFileCommand);
        }

        public ReactiveCommand<Unit, Unit> OpenFile { get; }
        private async Task<string> GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(allFilesFilter);
            string[] result = await dialog.ShowAsync((MainWindow)Application.Current.ApplicationLifetime);
            return result[0];
        }
        private void Start(string path)
        {
            if (playbackController != null)
                playbackController.Dispose();

            playbackController = new PlaybackController();
            playbackController.Initialize(path);
            playbackController.Play();
            SetNowPlayingData();
            PlayPause = "Pause";

        }
        private void SetNowPlayingData()
        {
            Cover = playbackController.CurrentAudioMetadata.Cover;
            Album = playbackController.CurrentAudioMetadata.Album;
            Artist = playbackController.CurrentAudioMetadata.Artist;
            Title = playbackController.CurrentAudioMetadata.Title;
        }
        async void OpenFileCommand()
        {
            string path = await GetPath();
            if (path != "")
                Start(path);
        }
    }
}
