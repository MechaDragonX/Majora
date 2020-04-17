﻿using LibVLCSharp.Shared;
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

        public AudioResource Resource { get; set; }
        public int Volume { get; set; }
        public bool Muted { get; set; }
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
        }

        /// <summary>
        /// Initialize with a path to the resource
        /// </summary>
        /// <param name="path">Path to a audio resource. Can be from filesystem and web.</param>
        public void Initialize(string path)
        {
            if(path == "")
                return;

            Resource = new AudioResource(path);

            FromType fromType;
            if (File.Exists(path))
                fromType = FromType.FromPath;
            else
                fromType = FromType.FromLocation;

            VLCPlayer.Media = new Media(VLCLib, path, fromType);
            VLCPlayer.Media.AddOption(":no-video");

            Volume = 100;
            Muted = false;

            VLCPlayer.TimeChanged += TimeChanged; 
            //VLCPlayer.PositionChanged += PositionChanged;
            //VLCPlayer.LengthChanged += LengthChanged;
            //VLCPlayer.EndReached += EndReached;
            //VLCPlayer.Playing += Playing;
            //VLCPlayer.Paused += Paused;
        }

        public void TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            Time = e.Time;
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
        /// Change the volume to the given percentage
        /// </summary>
        /// <param name="percentage">A integer between 0 and 100 (inclusive on both ends)</param>
        /// <param name="muteButton">Was the mute button used to change the volume?</param>
        public void ChangeVolume(int percentage, bool muteButton)
        {
            if(!(percentage < 0 || percentage > 100))
            {
                VLCPlayer.Volume = percentage;
                if(muteButton)
                    Muted = true;
                else
                    Volume = percentage;
            }
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

        //private long TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        //{
        //    Time = e.Time;
        //    return Time;
        //}
    }
}