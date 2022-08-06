using System;
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;

namespace RoadToDB
{
    public class SoundOperations : IDisposable
    {
        // Depends on: NAudio is an open source .NET audio library written by Mark Heath (https://github.com/naudio/NAudio)
        public static WaveOutEvent WaveSound { get; set; } = new WaveOutEvent();
        public static WaveFileReader WavReader { get; set; } = new WaveFileReader(@"SoundEffects/Sound.wav");

        static SoundOperations()
        {
            WaveSound.Init(WavReader);
        }

        public static void PlaySound()
        {
            WaveSound.Play();
        }

        public static void STFU()
        {
            WaveSound.Stop();
        }

        // Dispose: https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        //      Dispose is primarily for releasing unmanaged resources
        public void Dispose()
        {
            WavReader.Dispose();
            WaveSound.Dispose();
        }
    }
}
