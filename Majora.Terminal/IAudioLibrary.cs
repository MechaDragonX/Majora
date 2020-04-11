using Bassoon;
using System;

namespace Majora.Terminal
{
    interface IAudioLibrary
    {
        public object Load(string path);
        public void Play(object audio, string path);
        public void Dispose(object audio);
        public void CheckCommandInput(object audio);
    }
}
