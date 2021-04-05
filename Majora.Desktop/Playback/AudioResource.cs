using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Playback
{
    class AudioResource
    {
        public string Path { get; set; }
        public AudioMetadata Metadata { get; set; }

        public AudioResource(string path)
        {
            Path = path;
            Metadata = new AudioMetadata(path);
        }
    }
}
