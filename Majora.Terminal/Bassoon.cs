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
