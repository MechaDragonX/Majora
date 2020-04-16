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

        private static Image albumImage;
        private static TextBlock albumBlock;
        private static TextBlock artistBlock;
        private static TextBlock titleBlock;
        private static TextBlock timeBlock;

        private static Button playPauseButton;

        private void SetNowPlayingData()
        {
            albumImage.Source = playbackController.CurrentAudioMetadata.AlbumCover;
            albumBlock.Text = playbackController.CurrentAudioMetadata.Album;
            artistBlock.Text = playbackController.CurrentAudioMetadata.Artist;
            titleBlock.Text = playbackController.CurrentAudioMetadata.Title;
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
            string[] result = await dialog.ShowAsync((Window)this.Parent);
            return string.Join(" ", result);
        }
        private void Start(string path)
        {
            if(playbackController != null)
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
            Start(path);
        }
        public void OnPlayPauseButtonClicked(object sender, RoutedEventArgs e)
        {
            if(playbackController == null)
                return;

            if(playbackController.IsPlaying())
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
            if(playbackController == null)
                return;

            playbackController.Stop();
        }

        public NowPlayingView()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            albumImage = this.Find<Image>("albumImage");
            albumBlock = this.Find<TextBlock>("albumBlock");
            artistBlock = this.Find<TextBlock>("artistBlock");
            titleBlock = this.Find<TextBlock>("titleBlock");
            timeBlock = this.Find<TextBlock>("timeBlock");

            playPauseButton = this.Find<Button>("playPauseButton");
        }
    }
}
