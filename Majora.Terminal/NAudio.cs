using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Terminal
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
        public void Play(object audio, string path)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Init(File);
            output.Play();
            NowPlaying(path);
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
        public void Dispose(object audio)
        {
            File.Dispose();
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Dispose();
        }
        public void CheckCommandInput(object audio)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            string input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                int validCommand = -1;
                if(output.PlaybackState == PlaybackState.Playing)
                {
                    validCommand = CheckCommand(input);
                    if(validCommand == 0)
                        CommandError();
                    else if(validCommand != 2)
                    {
                        if(input.ToLower() == ControlType.pause.ToString())
                            output.Pause();
                        else if(input.Split(' ')[0].ToLower() == ControlType.volume.ToString())
                            ChangeVolume(output, input.Split(' ')[1]);
                        else if(input.ToLower() == ControlType.mute.ToString())
                            ChangeVolume(output, 0.ToString());
                        else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(output);
                            break;
                        }
                    }
                }
                else
                {
                    validCommand = CheckCommand(input);
                    if(validCommand == 0)
                        CommandError();
                    else if(validCommand != 2)
                    {
                        if(input.ToLower() == ControlType.play.ToString())
                            output.Play();
                        else if(input.Split(' ')[0].ToLower() == ControlType.volume.ToString())
                            ChangeVolume(output, input.Split(' ')[1]);
                        else if(input.ToLower() == ControlType.mute.ToString())
                            ChangeVolume(output, 0.ToString());
                        else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(output);
                            break;
                        }
                    }
                }
            }
        }
    }
}
