using ATL;
using ATL.AudioData;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Majora.Playback
{
    class AudioMetadata
    {
        public Bitmap AlbumCover { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }

        public AudioMetadata(string path)
        {
            Track track = new Track(path);
            Album = track.Album;
            Artist = track.Artist;
            Title = track.Title;
            AlbumCover = new Bitmap(new MemoryStream(track.EmbeddedPictures[0].PictureData));
        }
    }
}
