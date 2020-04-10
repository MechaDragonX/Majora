using System;
using System.Collections.Generic;
using System.IO;
using Bassoon;

namespace Majora.Terminal
{
    class Program
    {
        private static Dictionary<string, string> testFiles = new Dictionary<string, string>()
        {
            {  "wav", "hydro_city_2-mania.wav" },
            {  "mp3", "mm2_wily1-fc.mp3" },
            {  "m4a", "gerudo_valley.m4a" },
            {  "flac", "tank.flac" },
        };

        static void Main(string[] args)
        {
            Console.WriteLine("--- Majora Terminal Test Program ---\n\nWrite the name of the file extension you want to test!");

            using(new BassoonEngine())
            {
                while(true)
                {
                    string path = getFile();
                    Sound sound;

                    try { sound = new Sound(path); }
                    catch(Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ERROR: { e.Message }");
                        Console.ResetColor();
                        return;
                    }

                    sound.Volume = 0.2f;
                    sound.Play();
                    Console.WriteLine($"Playing \"{ sound.Artist } - { sound.Title }\" from \"{ sound.Album }\" at 20% volume.");

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
                    Console.ResetColor();

                    Console.WriteLine("Do you wanna play another file? (y/n)");
                    input = Console.ReadLine();
                    if (input.ToLower() == "n" || input.ToLower() != "y")
                        return;
                    Console.WriteLine("Write the name of the file extension you want to test!");
                }
            }
        }

        private static string getFile()
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
            return Path.Join(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, "Test", testFiles[extension]);
        }
    }
}
