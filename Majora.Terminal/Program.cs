using ATL;
using ATL.AudioData;
using Bassoon;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Majora.Terminal
{
    class Program
    {
        private static readonly Dictionary<string, string> testFiles = new Dictionary<string, string>()
        {
            { "wav", "hydro_city_2-mania.wav" },
            { "mp3", "mm2_wily1-fc.mp3" },
            { "m4a", "gerudo_valley.m4a" },
            { "flac", "tank.flac" },
            { "ogg", "cruel_angel's_thesis.ogg" }
        };
        private static readonly Dictionary<string, string> extensions = new Dictionary<string, string>()
        {
            { "wav", "bassoon" },
            { "flac", "bassoon" },
            { "ogg", "bassoon" },
            { "mp3", "naudio" },
            { "aac", "naudio" },
            { "m4a", "naudio" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("--- Majora Terminal Test Program ---\n\nWrite the name of the file extension you want to test!");

            using(new BassoonEngine())
            {
                while(true)
                {
                    Tuple<string, string> file = GetFile();

                    if(extensions[file.Item1] == "bassoon")
                        PlayWithBassoon(file.Item2);
                    else if(extensions[file.Item1] == "naudio")
                    {
                        // playWithNAudio();
                        AudioFileReader audioFile = new AudioFileReader(file.Item2);
                        WaveOutEvent outputDevice = new WaveOutEvent();
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        if (outputDevice.PlaybackState == PlaybackState.Playing)
                            NowPlaying(file.Item2);
                        Console.WriteLine("Press any key to stop");
                        Console.ReadKey();
                        audioFile.Dispose();
                        outputDevice.Dispose();
                    }
                    Console.ResetColor();

                    string input;
                    Console.WriteLine("Do you wanna play another file? (y/n)");
                    while(true)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        input = Console.ReadLine();
                        if(input.ToLower() == "y" || input.ToLower() == "yes")
                        {
                            Console.ResetColor();
                            break;
                        }
                        else if (input.ToLower() == "n" || input.ToLower() == "no")
                        {
                            Console.ResetColor();
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Please type yes or no\n");
                        }
                    }
                    Console.WriteLine("Write the name of the file extension you want to test!");
                }
            }
        }

        private static Tuple<string, string> GetFile()
        {
            string extension;
            while(true)
            {

                Console.ForegroundColor = ConsoleColor.Cyan;
                extension = Console.ReadLine();
                if(testFiles.ContainsKey(extension))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There's not test file with that extension.");
            }
            Console.ResetColor();
            return new Tuple<string, string>
            (
                extension,
                Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, "Test", testFiles[extension])
            );
        }
        private static void NowPlaying(string path)
        {
            Track track = new Track(path);
            Console.WriteLine($"Now Playing \"{ track.Artist } - { track.Title }\" from \"{ track.Album }\"");
        }
        private static void PlayWithBassoon(string path)
        {
            Sound sound;
            try { sound = new Sound(path); }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: { e.Message }");
                Console.ResetColor();
                return;
            }

            sound.Play();
            NowPlaying(path);

            string input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if(sound.IsPlaying)
                {
                    if(input.ToLower() == "pause")
                        sound.Pause();
                    else if(input.ToLower() == "stop" || input.ToLower() == "")
                    {
                        sound.Dispose();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("That command is not supported or is misspelled!\n");
                    }
                }
                else
                {
                    if(input.ToLower() == "play")
                        sound.Play();
                    else if(input.ToLower() == "stop" || input.ToLower() == "")
                    {
                        sound.Dispose();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("That command is not supported or is misspelled!\n");
                    }
                }
            }
        }
    }
}
