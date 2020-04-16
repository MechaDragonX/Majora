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
        public long Time { get; set; }

        // Event Handlers
        public EventHandler<MediaPlayerTimeChangedEventArgs> OnTimeChanged;
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
            if(path == "")
                return;

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

        /// <summary>
        /// Check if any media is playing currently
        /// </summary>
        /// <returns>True if any is playing, false if not</returns>
        public bool IsPlaying()
        {
            return VLCPlayer.IsPlaying;
        }
        /// <summary>
        /// Play the currently loaded resource
        /// </summary>
        public void Play()
            => VLCPlayer.Play();
        /// <summary>
        /// Pause the currently loaded resource
        /// </summary>
        public void Pause()
            => VLCPlayer.Pause();
        /// <summary>
        /// Stop the currently loaded resource
        /// </summary>
        public void Stop()
            => VLCPlayer.Stop();
        /// <summary>
        /// Dispose the currently used VLC Library and Media PLayer. Should be called when ever the file is changed.
        /// </summary>
        public void Dispose()
        {
            VLCLib.Dispose();
            VLCPlayer.Dispose();
        }

        private long TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Time = e.Time;
            return Time;
        }
    }
}