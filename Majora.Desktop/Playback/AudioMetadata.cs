using ATL;
using ATL.AudioData;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Majora.Playback
{
    class AudioMetadata
    {
        public HashSet<string> imageExtensions = new HashSet<string>()
        {
            ".jpg", ".png"
        };

        public Bitmap Cover { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }

        public AudioMetadata(string path)
        {
            Track track = new Track(path);
            Album = track.Album;
            Artist = track.Artist;
            Title = track.Title;

            string currentDir = Path.GetDirectoryName(path);
            List<string> files;
            List<string> imagefiles;
            if(track.EmbeddedPictures.Count != 0)
                Cover = new Bitmap(new MemoryStream(track.EmbeddedPictures[0].PictureData));
            else
            {
                files = Directory.EnumerateFiles(currentDir).ToList();
                imagefiles = files.Where(x => imageExtensions.Contains(Path.GetExtension(x))).ToList();
                if(imagefiles.Count != 0)
                    Cover = new Bitmap(imagefiles[0]);
                else
                    Cover = null;
            }
        }
    }
}
