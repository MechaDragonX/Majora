using NAudio.Wave;
using System;

namespace MajoraLib
{
    public class NAudio : AudioLibrary, IAudioLibrary
    {
        private AudioFileReader File;

        public NAudio() { }

        public object Load(string path)
        {
            WaveOutEvent output;
            try
            {
                File = new AudioFileReader(path);
                output = new WaveOutEvent();
            }
            catch(Exception e)
            {
                LogError(e);
                return null;
            }
            return output;
        }
        public void Start(object audio, string path)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Init(File);
            output.Play();
            NowPlaying(path);
        }
        public void Dispose(object audio)
        {
            File.Dispose();
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Dispose();
        }

        public bool IsPlaying(object audio)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            return output.PlaybackState == PlaybackState.Playing;
        }
        public void Play(object audio)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Play();
        }
        public void Pause(object audio)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Pause();
        }
        public void ChangeVolume(object audio, string input)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            double percent = double.Parse(input.Trim('%'));

            if(percent >= 0 && percent <= 100)
                output.Volume = (float)(percent / 100);
            else if(percent < 0 || percent > 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: The volume can't be negative or over 100%!");
                Console.ResetColor();
            }
        }

        public void Execute(object audio)
        {
            string input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                int validCommand;
                if (IsPlaying(audio))
                {
                    validCommand = CheckCommand(input);
                    if (validCommand == 0)
                        CommandError();
                    else if (validCommand != 2)
                    {
                        if (input.ToLower() == Commands.pause.ToString())
                            Pause(audio);
                        else if (input.Split(' ')[0].ToLower() == Commands.volume.ToString())
                            ChangeVolume(audio, input.Split(' ')[1]);
                        else if (input.ToLower() == Commands.mute.ToString())
                            ChangeVolume(audio, 0.ToString());
                        else if (input.ToLower() == Commands.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(audio);
                            break;
                        }
                    }
                }
                else
                {
                    validCommand = CheckCommand(input);
                    if (validCommand == 0)
                        CommandError();
                    else if (validCommand != 2)
                    {
                        if (input.ToLower() == Commands.play.ToString())
                            Play(audio);
                        else if (input.Split(' ')[0].ToLower() == Commands.volume.ToString())
                            ChangeVolume(audio, input.Split(' ')[1]);
                        else if (input.ToLower() == Commands.mute.ToString())
                            ChangeVolume(audio, 0.ToString());
                        else if (input.ToLower() == Commands.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(audio);
                            break;
                        }
                    }
                }
            }
        }
    }
}
