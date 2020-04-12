using System;
using System.IO;
using System.Text;

namespace Majora.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("--- Majora Terminal ---\n\n");
            string path = "";
            while(true)
            {
                Console.WriteLine("Please write the path to your file!");
                
                AudioLibrary library;
                while (true)
                {
                    path = ValidateFile();
                    try
                    {
                        library = AudioLibrary.CheckFile(path);
                        break;

                    }
                    catch (Exception e)
                    {
                        AudioLibrary.LogError(e);
                    }
                }

                if(!AudioLibrary.supported[Path.GetExtension(path)[1..]])
                    NAudioStart(library, path);
                else
                {
                    Bassoon bassoon = (Bassoon)library;
                    using (bassoon.Engine)
                        BassoonStart(bassoon, path);
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

        private static string ValidateFile()
        {
            string path;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                path = Console.ReadLine();
                if(File.Exists(path))
                {
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

            return path;
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
                    Console.WriteLine();
                    return true;
                }
                else if(input.ToLower() == "n" || input.ToLower() == "no")
                {
                    Console.ResetColor();
                    Console.WriteLine();
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Please type yes or no\n");
                }
            }
        }
        
        private static void BassoonStart(Bassoon bassoon, string path)
        {
            var sound = bassoon.Load(path);
            bassoon.Start(sound, path);
            bassoon.Execute(sound);
        }
        private static void NAudioStart(AudioLibrary lib, string path)
        {
            NAudio nAudio = (NAudio)lib;
            var output = nAudio.Load(path);
            nAudio.Start(output, path);
            nAudio.Execute(output);
        }
    }
}
