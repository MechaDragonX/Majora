using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Majora.Models;
using Majora.Playback;
using Majora.Views;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Majora.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private static PlaybackController PlaybackController = null;
        private static readonly FileDialogFilter AllFilesFilter = new FileDialogFilter()
        {
            Name = "All Supported Music Files",
            Extensions = new List<string>()
            {
                "3gp", "a52", "a52b", "aac", "alac", "asf", "atrc", "atrc", "au",
                "cook", "dnet", "dts", "flac", "m4a", "mp3", "mpc", "mpga", "ogg",
                "opus", "raac", "racp", "ralf", "sipr", "spex", "tac", "tta", "vorb",
                "wav", "wma", "wma1", "wma2", "xa"
            }
        };
        private static Stack<string> PlayedAudio = new Stack<string>();

        private ObservableCollection<string> recentlyPlayedMenu;
        public ObservableCollection<string> RecentlyPlayedMenu
        {
            get => recentlyPlayedMenu;
            set => this.RaiseAndSetIfChanged(ref recentlyPlayedMenu, value);
        }
        private string playPauseText;
        public string PlayPauseText
        {
            get => playPauseText;
            set => this.RaiseAndSetIfChanged(ref playPauseText, value);
        }
        private string muteText;
        public string MuteText
        {
            get => muteText;
            set => this.RaiseAndSetIfChanged(ref muteText, value);
        }
        private Bitmap cover;
        public Bitmap Cover
        {
            get => cover;
            set => this.RaiseAndSetIfChanged(ref cover, value);
        }
        private string album;
        public string Album
        {
            get => album;
            set => this.RaiseAndSetIfChanged(ref album, value);
        }
        private string artist;
        public string Artist
        {
            get => artist;
            set => this.RaiseAndSetIfChanged(ref artist, value);
        }
        private string title;
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        //private string time;
        //public string Time
        //{
        //    get => time;
        //    set => this.RaiseAndSetIfChanged(ref time, value);
        //}

        public MainWindowViewModel()
        {
            PlayPauseText = "Play";
            MuteText = "Mute";
            Cover = null;
            Album = "";
            Artist = "";
            Title = "";

            OpenFile = ReactiveCommand.Create(OpenFileCommand);
            PlayPause = ReactiveCommand.Create(PlayPauseCommand);
            Stop = ReactiveCommand.Create(StopCommand);
            Mute = ReactiveCommand.Create(MuteCommand);
            PlayNewFile = ReactiveCommand.Create<string>(PlayNewFileCommand);

            if (File.Exists(Path.Join(Environment.CurrentDirectory, "Data", "recent.json")))
            {
                var data = DeserializePlayedAudio();
                if (data != null)
                    PlayedAudio = data;
            }
            RecentlyPlayedMenu = GetRecentlyPlayed();
            
        }
        private static void SerializePlayedAudio()
        {
            JsonSerializer serializer = new JsonSerializer();
            string dir = Path.Join(Environment.CurrentDirectory, "Data");
            string file = Path.Join(dir, "recent.json");

            if(!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using(StreamWriter sWriter = new StreamWriter(file))
            using(JsonWriter jWriter = new JsonTextWriter(sWriter))
                serializer.Serialize(jWriter, PlayedAudio);
        }
        private static Stack<string> DeserializePlayedAudio()
        {
            return JsonConvert.DeserializeObject<Stack<string>>(File.ReadAllText(Path.Join(Environment.CurrentDirectory, "Data", "recent.json")));
        }
        private ObservableCollection<string> GetRecentlyPlayed()
        {
            List<string> list = PlayedAudio.ToList();
            list.Reverse();

            int count = 0;
            ObservableCollection<string> menuItems = new ObservableCollection<string>();
            foreach(string value in list)
            {
                if(count == 10)
                    break;

                count++;
                menuItems.Add(value);
            }

            return menuItems;
        }

        public ReactiveCommand<Unit, Unit> OpenFile { get; }
        private async Task<string> GetPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(AllFilesFilter);

            string[] result = null;
            if(Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                result = await dialog.ShowAsync(desktop.MainWindow);
            else if(Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime singleView)
                result = await dialog.ShowAsync((Window)singleView.MainView.Parent);

            return result[0];
        }
        private void Start(string path)
        {
            if (PlaybackController != null)
                PlaybackController.Dispose();

            PlaybackController = new PlaybackController();
            PlaybackController.Initialize(path);
            PlayedAudio.Push(PlaybackController.Resource.Path);
            SerializePlayedAudio();

            PlaybackController.Play();
            SetNowPlayingData();
            PlayPauseText = "Pause";
        }
        private void SetNowPlayingData()
        {
            Cover = PlaybackController.Resource.Metadata.Cover;
            Album = PlaybackController.Resource.Metadata.Album;
            Artist = PlaybackController.Resource.Metadata.Artist;
            Title = PlaybackController.Resource.Metadata.Title;
        }
        async void OpenFileCommand()
        {
            string path = await GetPath();
            if(path != "")
                Start(path);
        }

        public ReactiveCommand<Unit, Unit> PlayPause { get; }
        void PlayPauseCommand()
        {
            if(PlaybackController == null)
                return;

            if(PlaybackController.IsPlaying())
            {
                PlaybackController.Pause();
                PlayPauseText = "Play";
            }
            else
            {
                PlaybackController.Play();
                PlayPauseText = "Pause";
            }
        }

        public ReactiveCommand<Unit, Unit> Stop { get; set; }
        void StopCommand()
        {
            if(PlaybackController == null)
                return;

            PlaybackController.Stop();
        }

        public ReactiveCommand<Unit, Unit> Mute { get; set; }
        void MuteCommand()
        {
            if(PlaybackController == null)
                return;

            if(!PlaybackController.Muted)
            {
                PlaybackController.ChangeVolume(0, true);
                MuteText = "Unmute";
            }
            else if(PlaybackController.Muted)
            {
                PlaybackController.ChangeVolume(PlaybackController.Volume, true);
                MuteText = "Mute";
            }
        }

        public ReactiveCommand<string, Unit> PlayNewFile { get; }
        void PlayNewFileCommand(string parameter)
        {
            Start(parameter);
        }
    }
}
