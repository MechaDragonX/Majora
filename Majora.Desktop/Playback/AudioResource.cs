using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Playback
{
    class AudioResource
    {
        public string ResourcePath { get; set; }
        public AudioMetadata Metadata { get; set; }

        public AudioResource(string resourcePath)
        {
            ResourcePath = resourcePath;
            Metadata = new AudioMetadata(resourcePath);
        }
    }
}
