using Bassoon;
using System;

namespace Majora.Terminal
{
    interface IAudioPlayer
    {
        public Sound Load(string path);
        public void Play(Sound sound, string path);
        public void Dispose(Sound sound);
        public void CheckCommandInput(Sound sound);
    }
}
