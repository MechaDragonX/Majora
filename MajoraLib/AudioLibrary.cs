using ATL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Majora.Terminal
{

    public abstract class AudioLibrary
    {
        /// <summary>
        /// Defines the supported file types. Key: Extension with '.', Value: "true" = Supported by Basson, "false" = Only supported by NAudio
        /// </summary>
        public static readonly Dictionary<string, bool> supported = new Dictionary<string, bool>()
        {
            { "wav", true},
            { "w64", true },
            { "ogg", true },
            { "flac", true },
            { "au", true },
            { "aiff", true },
            { "mp3", false },
            { "aac", false },
            { "m4a", false }
        };
        /// <summary>
        /// Defines the supported commands with help text. Key: Command Name, Value: Help Text
        /// </summary>
        public static readonly Dictionary<string, string> Commands = new Dictionary<string, string>()
        {
            { "help", "List the features of all the commands." },
            { "play", "Play the file. Only works when paused." },
            { "pause", "Pause the file. Only works when the file is playing." },
            { "volume", "Change the volume. Type \"volume <percent>\" to change the volume to that percentage. Percent symbol is not necessary." },
            { "mute", "Mute the volume." },
            { "stop", "Stop using the current file. This allows you to select a new file." },
        };

        /// <summary>
        /// Check if the provided file type is supported and return the appropriate an instance audio library class.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Audio Library Object</returns>
        public static AudioLibrary CheckFile(string path)
        {
            if(!File.Exists(path))
                throw new FileNotFoundException("The file was not found!");
            if(!supported.ContainsKey(Path.GetExtension(path)[1..]))
                throw new NotSupportedException("The provided file type is not supported!");

            if(supported[Path.GetExtension(path)[1..]])
                return new Bassoon();
            return new NAudio();
        }
        /// <summary>
        /// A Method for changing the console color to red, logging the error message of an exception, and then resetting the color. Use in try/catch statements.
        /// </summary>
        /// <param name="e">Generic Exception</param>
        public static void LogError(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: { e.Message }");
            Console.ResetColor();
        }
        /// <summary>
        /// Writes the artist, track title, and album title of the current.
        /// </summary>
        /// <param name="path">Path to the file</param>
        public static void NowPlaying(string path)
        {
            Track track = new Track(path);
            Console.WriteLine($"Now Playing \"{ track.Artist } - { track.Title }\" from \"{ track.Album }\"");
        }
        /// <summary>
        /// Checks if the command is legal and if it is a help command.
        /// </summary>
        /// <param name="input">Text passed into the console</param>
        /// <returns>0: Bad Input, 1: Good Command, 2: Help Command</returns>
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
        /// <summary>
        /// Displays help messages for all commands.
        /// </summary>
        public static void HelpCommand()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All Commands:");
            foreach(KeyValuePair<string, string> item in Commands)
                Console.WriteLine($"{ item.Key }: { item.Value }");
            Console.ResetColor();
        }
        /// <summary>
        /// Displays help messages for the specified command.
        /// </summary>
        /// <param name="command"></param>
        public static void HelpCommand(string command)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Help info for \"{ command }\":");
            Console.WriteLine($"{ command }: { Commands[command] }");
            Console.ResetColor();
        }
        /// <summary>
        /// Logs a command error message.
        /// </summary>
        public static void CommandError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("That command is not supported or is misspelled!\n");
        }
    }
}
