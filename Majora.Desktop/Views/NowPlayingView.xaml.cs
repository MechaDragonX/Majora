using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Majora.Playback;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Majora.Views
{
    public class NowPlayingView : UserControl
    {
		private static PlaybackController playbackController;

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
		private void Play(string path)
		{
			playbackController = new PlaybackController();
			playbackController.Initialize(path);
			playbackController.Play();
		}

		public async void OnFileImportButtonClicked(object sender, RoutedEventArgs e)
		{
			string path = await GetPath();
			Play(path);
		}
		public void OnPlayButtonClicked(object sender, RoutedEventArgs e)
		{
			playbackController.Play();
		}
		public void OnPauseButtonClicked(object sender, RoutedEventArgs e)
		{
			playbackController.Pause();
		}

		public NowPlayingView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
			Button fileImportButton = this.Find<Button>("clicker");
		}
    }
}
