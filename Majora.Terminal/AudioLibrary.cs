using ATL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Terminal
{
    public abstract class AudioLibrary
    {
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
        public static void CommandError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("That command is not supported or is misspelled!\n");
        }
    }
}
