using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Majora.Playback
{
    class PlaybackController : IDisposable
    {
        private LibVLC VLCLib;
        private MediaPlayer VLCPlayer;

        protected string ResourcePath { get; set;  }
        public AudioMetadata CurrentAudioMetadata { get; set; }

        // Event Handlers
        //private EventHandler<MediaPlayerTimeChangedEventArgs> TimeChanged;
        //private EventHandler<MediaPlayerPositionChangedEventArgs> PositionChanged;
        //private EventHandler<MediaPlayerLengthChangedEventArgs> LengthChanged;
        //private EventHandler<EventArgs> EndReached;
        //private EventHandler<EventArgs> Playing;
        //private EventHandler<EventArgs> Paused;

        public PlaybackController()
        {
            Core.Initialize();

            VLCLib = new LibVLC();
            VLCPlayer = new MediaPlayer(VLCLib);

            ResourcePath = "";
            CurrentAudioMetadata = null;
        }

        /// <summary>
        /// Initialize with a path to the resource
        /// </summary>
        /// <param name="path">Path to a audio resource. Can be from filesystem and web.</param>
        public void Initialize(string path)
        {
            ResourcePath = path;
            CurrentAudioMetadata = new AudioMetadata(ResourcePath);

            FromType fromType;
            if (File.Exists(ResourcePath))
                fromType = FromType.FromPath;
            else
                fromType = FromType.FromLocation;

            VLCPlayer.Media = new Media(VLCLib, ResourcePath, fromType);
            VLCPlayer.Media.AddOption(":no-video");

            //VLCPlayer.TimeChanged += TimeChanged;
            //VLCPlayer.PositionChanged += PositionChanged;
            //VLCPlayer.LengthChanged += LengthChanged;
            //VLCPlayer.EndReached += EndReached;
            //VLCPlayer.Playing += Playing;
            //VLCPlayer.Paused += Paused;
        }
        public void Play()
        {
            VLCPlayer.Play();
        }
        public void Pause()
        {
            VLCPlayer.Pause();
        }
        public void Stop()
        {
            VLCPlayer.Stop();
        }
        public void Dispose()
        {
            VLCLib.Dispose();
            VLCPlayer.Dispose();
        }
    }
}