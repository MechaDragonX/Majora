using ATL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Majora.Terminal
{

    public abstract class AudioLibrary
    {
        public static readonly Dictionary<string, string> Commands = new Dictionary<string, string>()
        {
            { "help", "List the features of all the commands." },
            { "play", "Play the file. Only works when paused." },
            { "pause", "Pause the file. Only works when the file is playing." },
            { "stop", "Stop using the current file. This also you to select a new file." },
        };

        public static void LogError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: { e.Message }");
            Console.ResetColor();
        }
        public static void NowPlaying(string path)
        {
            Track track = new Track(path);
            Console.WriteLine($"Now Playing \"{ track.Artist } - { track.Title }\" from \"{ track.Album }\"");
        }
        public static int CheckCommand(string input)
        {
            string[] args = input.Split(' ');
            if(args.Where(x => x == "help").ToList().Count > 0)
            {
                if(args.Length == 1)
                {
                    HelpCommand();
                    return 2;
                }
                else
                {
                    HelpCommand(args[1]);
                    return 2;
                }
            }
            else
            {
                if(Commands.ContainsKey(args[0]))
                    return 1;
            }
            return 0;
        }
        public static void HelpCommand()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All Commands:");
            foreach(KeyValuePair<string, string> item in Commands)
                Console.WriteLine($"{ item.Key }: { item.Value }");
            Console.ResetColor();
        }
        public static void HelpCommand(string command)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Help info for \"{ command }\":");
            Console.WriteLine($"{ command }: { Commands[command] }");
            Console.ResetColor();
        }
        public static void CommandError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("That command is not supported or is misspelled!\n");
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
    }
}
