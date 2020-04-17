﻿using Avalonia;
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
        private static TextBlock timeBlock;
        private static Button playPauseButton;
        private static Button muteButton;

        public NowPlayingView()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            recentFileButton = this.Find<MenuItem>("recentFileButton");

            timeBlock = this.Find<TextBlock>("timeBlock");

            muteButton = this.Find<Button>("muteButton");
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
