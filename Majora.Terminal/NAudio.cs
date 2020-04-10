﻿using Bassoon;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace Majora.Terminal
{
    public class NAudio : AudioLibrary, IAudioPlayer
    {
        private AudioFileReader File;

        public NAudio() { }

        public object Load(string path)
        {
            WaveOutEvent output;
            try
            {
                File = new AudioFileReader(path);
                output = new WaveOutEvent();
            }
            catch (Exception e)
            {
                LogError(e);
                return null;
            }
            return output;
        }
        public void Play(object audio, string path)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Init(File);
            output.Play();
            NowPlaying(path);
        }
        public void Dispose(object audio)
        {
            File.Dispose();
            WaveOutEvent output = (WaveOutEvent)audio;
            output.Dispose();
        }
        public void CheckCommandInput(object audio)
        {
            WaveOutEvent output = (WaveOutEvent)audio;
            string input;
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                if(output.PlaybackState == PlaybackState.Playing)
                {
                    if(input.ToLower() == ControlType.pause.ToString())
                        output.Pause();
                    else if (input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                    {
                        Dispose(output);
                        break;
                    }
                    else
                        CommandError();
                }
                else
                {
                    if (input.ToLower() == ControlType.play.ToString())
                        output.Play();
                    else if (input.ToLower() == ControlType.stop.ToString() || input.ToLower() == "")
                    {
                        Dispose(output);
                        break;
                    }
                    else
                        CommandError();
                }
            }
        }
    }
}