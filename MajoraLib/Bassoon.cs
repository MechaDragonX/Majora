using Bassoon;
using System;

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
        public void Start(object audio, string path)
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

        public bool IsPlaying(object audio)
        {
            Sound sound = (Sound)audio;
            return sound.IsPlaying;
        }
        public void Play(object audio)
        {
            Sound sound = (Sound)audio;
            sound.Play();
        }
        public void Pause(object audio)
        {
            Sound sound = (Sound)audio;
            sound.Pause();
        }
        public void ChangeVolume(object audio, string input)
        {
            Sound sound = (Sound)audio;
            double percent;
            try
            {
                percent = double.Parse(input.Trim('%'));
            }
            catch(Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: You need to have a number after \"volume\"!");
                Console.ResetColor();
                return;
            }

            if(percent >= 0 && percent <= 100)
                sound.Volume = (float)(percent / 100);
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
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                int validCommand;
                if(IsPlaying(audio))
                {
                    validCommand = CheckCommand(input);
                    if(validCommand == 0)
                        CommandError();
                    else if(validCommand != 2)
                    {
                        if(input.ToLower() == Terminal.Commands.pause.ToString())
                            Pause(audio);
                        else if(input.Split(' ')[0].ToLower() == Terminal.Commands.volume.ToString())
                            ChangeVolume(audio, input.Split(' ')[1]);
                        else if(input.ToLower() == Terminal.Commands.mute.ToString())
                            ChangeVolume(audio, 0.ToString());
                        else if(input.ToLower() == Terminal.Commands.stop.ToString() || input.ToLower() == "")
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
                        if (input.ToLower() == Terminal.Commands.play.ToString())
                            Play(audio);
                        else if (input.Split(' ')[0].ToLower() == Terminal.Commands.volume.ToString())
                            ChangeVolume(audio, input.Split(' ')[1]);
                        else if (input.ToLower() == Terminal.Commands.mute.ToString())
                            ChangeVolume(audio, 0.ToString());
                        else if (input.ToLower() == Terminal.Commands.stop.ToString() || input.ToLower() == "")
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
