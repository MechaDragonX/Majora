using ATL;
using Bassoon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Terminal
{
    public class Bassoon : AudioLibrary, IAudioPlayer
    {
        public BassoonEngine Engine { get; }

        public Bassoon()
        {
            Engine = new BassoonEngine();
        }

        public Sound Load(string path)
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
        public void Play(Sound sound, string path)
        {
            sound.Play();
            NowPlaying(path);
        }
        public void Dispose(Sound sound) { sound.Dispose(); }
        public void CheckCommandInput(Sound sound)
        {
            string input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if(sound.IsPlaying)
                {
                    if(input.ToLower() == ControlType.pause.ToString())
                        sound.Pause();
                    else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                    {
                        Dispose(sound);
                        break;
                    }
                    else
                        CommandError();
                }
                else
                {
                    if(input.ToLower() == ControlType.play.ToString())
                        sound.Play();
                    else if(input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                    {
                        Dispose(sound);
                        break;
                    }
                    else
                        CommandError();
                }
            }
        }
    }
}
