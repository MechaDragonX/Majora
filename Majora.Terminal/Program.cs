using ATL;
using Bassoon;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            { "ogg", "cruel_angel's_thesis.ogg" },
            { "aiff", "yume_cinderella.aiff" },
            { "opus", "sou.opus" }, // Not supported
            { "w64", "fukashigi_no_carte.w64" },
            { "wv", "gypsy_bard-ex.wv" }, // Not supported
            { "mpc", "zenzenzense.mpc" }, // Not supported
            { "au", "shinzou_wo_sasageyo.au" }
        };
        private static readonly List<string> nAudioExtensions = new List<string>()
        {
            "mp3",
            "aac",
            "m4a"
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            

            Console.WriteLine("Internal or Global Test?");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if(input.ToLower() == "internal" || input.ToLower() == "i")
                {
                    InternalTest();
                    break;
                }
                else if(input.ToLower() == "global" || input.ToLower() == "g")
                {
                    while(true)
                    {
                        Console.WriteLine("Please write the path to your file!");
                        string path = Console.ReadLine();
                        Tuple<string, string> file = GetFile(path);

                        if(nAudioExtensions.ContainsKey(file.Item1))
                            PlayWithNAudio(new NAudio(), file.Item2);
                        else
                        {
                            Bassoon bassoon = new Bassoon();
                            using (bassoon.Engine)
                                PlayWithBassoon(bassoon, file.Item2);
                        }
                        Console.ResetColor();
                        if(!AudioLibrary.YesNo())
                            return;
                    }
                }
            }
        }

        private static void InternalTest()
        {
            Console.WriteLine("--- Majora Terminal Test Program ---\n\nWrite the name of the file extension you want to test!");

            while(true)
            {
                Tuple<string, string> file = GetFile();

                if(nAudioExtensions.ContainsKey(file.Item1))
                    PlayWithNAudio(new NAudio(), file.Item2);
                else
                {
                    Bassoon bassoon = new Bassoon();
                    using (bassoon.Engine)
                        PlayWithBassoon(bassoon, file.Item2);
                }
                Console.ResetColor();
                if(!AudioLibrary.YesNo())
                    return;
                Console.WriteLine("Write the name of the file extension you want to test!");
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
        private static Tuple<string, string> GetFile(string path)
        {
            if(!File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: File doesn't exist!");
                Console.ResetColor();
                return null;
            }

            return new Tuple<string, string>
            (
                Path.GetExtension(path)[1..],
                path
            );
        }
        private static void LogError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: { e.Message }");
            Console.ResetColor();
        }
        private static void NowPlaying(string path)
        {
            Track track = new Track(path);
            Console.WriteLine($"Now Playing \"{ track.Artist } - { track.Title }\" from \"{ track.Album }\"");
        }
        private static void PlayWithBassoon(Bassoon bassoon, string path)
        {
            var sound = bassoon.Load(path);
            bassoon.Play(sound, path);
            bassoon.CheckCommandInput(sound);
        }
        private static void PlayWithNAudio(NAudio nAudio, string path)
        {
            var output = nAudio.Load(path);
            nAudio.Play(output, path);
            nAudio.CheckCommandInput(output);
        }
    }
}
