using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Majora.Terminal
{
    class Program
    {
        private static readonly List<string> nAudioExtensions = new List<string>()
        {
            "mp3",
            "aac",
            "m4a"
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string path = "";
            bool isDir = false;
            while(true)
            {
                Console.WriteLine("--- Majora Terminal Test Program ---\n\nWrite the name of the file extension you want to test!");
                Console.WriteLine("Please write the path to your file!");

                while(!isDir)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    path = Console.ReadLine();
                    if(File.Exists(path))
                    {
                        isDir = true;
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ERROR: The file doesn't exist! Did you misspell the path?");
                        Console.ResetColor();
                    }
                }

                if(nAudioExtensions.Contains(Path.GetExtension(path)))
                    PlayWithNAudio(new NAudio(), path);
                else
                {
                    Bassoon bassoon = new Bassoon();
                    using (bassoon.Engine)
                        PlayWithBassoon(bassoon, path);
                }
                Console.ResetColor();
                if(!YesNo())
                {
                    Console.WriteLine("Thanks for using Majora Terminal! Press any key to exit.");
                    Console.ReadKey();
                    return;
                }
            }
        }

        public static bool YesNo()
        {
            string input;
            Console.WriteLine("Do you wanna play another file? (y/n)");
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if(input.ToLower() == "y" || input.ToLower() == "yes")
                {
                    Console.ResetColor();
                    return true;
                }
                else if(input.ToLower() == "n" || input.ToLower() == "no")
                {
                    Console.ResetColor();
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Please type yes or no\n");
                }
            }
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
