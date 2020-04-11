namespace Majora.Terminal
{
    interface IAudioLibrary
    {
        public object Load(string path);
        public void Play(object audio, string path);
        public void ChangeVolume(object audio, string input);
        public void Dispose(object audio);
        public void CheckCommandInput(object audio);
    }
}
