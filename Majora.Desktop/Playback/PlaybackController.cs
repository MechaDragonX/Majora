using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Majora.Playback
{
    class PlaybackController
    {
        // 
        private static LibVLC VLCLib;
        private static MediaPlayer VLCPlayer;

        // Event Handlers
        private EventHandler<MediaPlayerTimeChangedEventArgs> TimeChanged;
        private EventHandler<MediaPlayerPositionChangedEventArgs> PositionChanged;
        private EventHandler<MediaPlayerLengthChangedEventArgs> LengthChanged;
        private EventHandler<EventArgs> EndReached;
        private EventHandler<EventArgs> Playing;
        private EventHandler<EventArgs> Paused;

        public PlaybackController()
        {
            Core.Initialize();
            VLCLib = new LibVLC();
            VLCPlayer = new MediaPlayer(VLCLib);
        }

        /// <summary>
        /// Initialize with a path to the resource
        /// </summary>
        /// <param name="path">Path to a audio resource. Can be from filesystem and web.</param>
        public void Initialize(string path)
        {
            FromType fromType;
            if(File.Exists(path))
                fromType = FromType.FromPath;
            else
                fromType = FromType.FromLocation;

            VLCPlayer.Media = new Media(VLCLib, path, fromType);
            VLCPlayer.Media.AddOption(":no-video");

            VLCPlayer.TimeChanged += TimeChanged;
            VLCPlayer.PositionChanged += PositionChanged;
            VLCPlayer.LengthChanged += LengthChanged;
            VLCPlayer.EndReached += EndReached;
            VLCPlayer.Playing += Playing;
            VLCPlayer.Paused += Paused;
        }
        public void Play()
        {
            VLCPlayer.Play();
        }
    }
}
