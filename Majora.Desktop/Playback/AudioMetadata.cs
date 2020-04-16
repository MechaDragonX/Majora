using ATL;
using ATL.AudioData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Playback
{
    class AudioMetadata
    {
        
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }

        public AudioMetadata(string path)
        {
            Track track = new Track(path);
            Album = track.Album;
            Artist = track.Artist;
            Title = track.Title;
        }
    }
}
