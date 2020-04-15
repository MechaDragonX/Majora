namespace MajoraLib
{
    public interface IAudioLibrary
    {
        public object Load(string path);
        public void Start(object audio, string path);
        public void Dispose(object audio);

        public bool IsPlaying(object audio);
        public void Play(object audio);
        public void Pause(object audio);
        public void ChangeVolume(object audio, string input);
    }
}
