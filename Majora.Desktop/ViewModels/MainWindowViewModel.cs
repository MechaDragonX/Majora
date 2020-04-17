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
    public class MainWindowViewModel : ReactiveObject
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

        private string playPauseText;
        public string PlayPauseText
        {
            get => playPauseText;
            set => this.RaiseAndSetIfChanged(ref playPauseText, value);
        }
        private string muteText;
        public string MuteText
        {
            get => muteText;
            set => this.RaiseAndSetIfChanged(ref muteText, value);
        }
        private Bitmap cover;
        public Bitmap Cover
        {
            get => cover;
            set => this.RaiseAndSetIfChanged(ref cover, value);
        }
        private string album;
        public string Album
        {
            get => album;
            set => this.RaiseAndSetIfChanged(ref album, value);
        }
        private string artist;
        public string Artist
        {
            get => artist;
            set => this.RaiseAndSetIfChanged(ref artist, value);
        }
        private string title;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        public MainWindowViewModel()
        {
            PlayPauseText = "Play";
            MuteText = "Mute";
            Cover = null;
            Album = "";
            Artist = "";
            Title = "";

            OpenFile = ReactiveCommand.Create(OpenFileCommand);
            PlayPause = ReactiveCommand.Create(PlayPauseCommand);
        }

        public ReactiveCommand<Unit, Unit> OpenFile { get; }
        private async Task<string> GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(allFilesFilter);

            string[] result = null;
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                result = await dialog.ShowAsync(desktop.MainWindow);
            else if (Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime singleView)
                result = await dialog.ShowAsync((Window)singleView.MainView.Parent);

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
            PlayPauseText = "Pause";

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

        public ReactiveCommand<Unit, Unit> PlayPause { get; }
        void PlayPauseCommand()
        {
            if (playbackController == null)
                return;

            if (playbackController.IsPlaying())
            {
                playbackController.Pause();
                PlayPauseText = "Play";
            }
            else
            {
                playbackController.Play();
                PlayPauseText = "Pause";
            }
        }
    }
}
