using Bassoon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Terminal
{
    public class Bassoon : AudioLibrary, IAudioLibrary
    {
        public BassoonEngine Engine { get; }

        public Bassoon()
        {
            Engine = new BassoonEngine();
        }

        public object Load(string path)
        {
            Sound sound;
            try { sound = new Sound(path); }
            catch(Exception e)
            {
                LogError(e);
                return null;
            }
            return sound;
        }
        public void Play(object audio, string path)
        {
            Sound sound = (Sound)audio;
            sound.Play();
            NowPlaying(path);
        }
        public void ChangeVolume(object audio, string input)
        {
            Sound sound = (Sound)audio;
            double percent = double.Parse(input.Trim('%'));

            if(percent >= 0 && percent <= 100)
                sound.Volume = (float)(percent / 100);
            else if(percent < 0 || percent > 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: The volume can't be negative or over 100%!");
                Console.ResetColor();
            }
        }
        public void Dispose(object audio)
        {
            Sound sound = (Sound)audio;
            sound.Dispose();
        }
        public void CheckCommandInput(object audio)
        {
            Sound sound = (Sound)audio;
            string input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                int validCommand = -1;
                if(sound.IsPlaying)
                {
                    validCommand = CheckCommand(input);
                    if(validCommand == 0)
                        CommandError();
                    else if(validCommand != 2)
                    {
                        if(input.ToLower() == ControlType.pause.ToString())
                            sound.Pause();
                        else if(input.Split(' ')[0].ToLower() == ControlType.volume.ToString())
                            ChangeVolume(sound, input.Split(' ')[1]);
                        else if(input.ToLower() == ControlType.mute.ToString())
                            ChangeVolume(sound, 0.ToString());
                        else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(sound);
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
                            sound.Play();
                        else if(input.Split(' ')[0].ToLower() == ControlType.volume.ToString())
                            ChangeVolume(sound, input.Split(' ')[1]);
                        else if(input.ToLower() == ControlType.mute.ToString())
                            ChangeVolume(sound, 0.ToString());
                        else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                        {
                            Dispose(sound);
                            break;
                        }
                    }
                }
            }
        }
    }
}
