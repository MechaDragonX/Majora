using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Majora.Playback;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Majora.Views
{
    public class NowPlayingView : UserControl
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

        private static MenuItem openButton;
        private static MenuItem recentFileButton;
        private static Image cover;
        private static TextBlock albumBlock;
        private static TextBlock artistBlock;
        private static TextBlock titleBlock;
        private static Button playPauseButton;
        private static Button muteButton;

        public NowPlayingView()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            openButton = this.Find<MenuItem>("openButton");
            recentFileButton = this.Find<MenuItem>("recentFileButton");

            cover = this.Find<Image>("cover");
            albumBlock = this.Find<TextBlock>("albumBlock");
            artistBlock = this.Find<TextBlock>("artistBlock");
            titleBlock = this.Find<TextBlock>("titleBlock");

            playPauseButton = this.Find<Button>("playPauseButton");
            muteButton = this.Find<Button>("muteButton");
        }

        private void SetNowPlayingData()
        {
            cover.Source = playbackController.CurrentAudioMetadata.Cover;
            albumBlock.Text = playbackController.CurrentAudioMetadata.Album;
            artistBlock.Text = playbackController.CurrentAudioMetadata.Artist;
            titleBlock.Text = playbackController.CurrentAudioMetadata.Title;
        }
        private async Task<string> GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(allFilesFilter);
            string[] result = await dialog.ShowAsync((Window)this.Parent);
            return string.Join(" ", result);
        }
        private void Start(string path)
        {
            if (playbackController != null)
                playbackController.Dispose();

            playbackController = new PlaybackController();
            playbackController.Initialize(path);
            playbackController.Play();
            SetNowPlayingData();
            playPauseButton.Content = "Pause";
        }

        public async void OnFileImportButtonClicked(object sender, RoutedEventArgs e)
        {
            string path = await GetPath();
            if (path != "")
                Start(path);
        }
        public async void OnOpenButtonClicked(object sender, RoutedEventArgs e)
        {
            string path = await GetPath();
            if (path != "")
                Start(path);
        }
        public void OnPlayPauseButtonClicked(object sender, RoutedEventArgs e)
        {
            if (playbackController == null)
                return;

            if (playbackController.IsPlaying())
            {
                playbackController.Pause();
                playPauseButton.Content = "Play";
            }
            else
            {
                playbackController.Play();
                playPauseButton.Content = "Pause";
            }
        }
        public void OnStopButtonClicked(object sender, RoutedEventArgs e)
        {
            if (playbackController == null)
                return;

            playbackController.Stop();
        }
        public void OnMuteButtonClicked(object sender, RoutedEventArgs e)
        {
            if (playbackController == null)
                return;

            if (!playbackController.Muted)
            {
                playbackController.ChangeVolume(0, true);
                muteButton.Content = "Unmute";
            }
            else if (playbackController.Muted)
            {
                playbackController.ChangeVolume(playbackController.Volume, true);
                muteButton.Content = "Mute";
            }
        }
    }
}
