// VoiceEngine.cs
// Handles all audio playback for the chatbot
// We use PowerShell to play WAV files because it's reliable on school computers
// This gives the bot a "voice" without needing complex AI libraries

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MizzBox
{
    // Static class - we don't need to create objects, just call the methods directly
    // Example: VoiceEngine.PlayGreeting() instead of new VoiceEngine().PlayGreeting()
    public static class VoiceEngine
    {
        // Plays the greeting sound when the program starts
        // We use Task.Run to play in background so the display shows immediately
        // without waiting for the audio to finish
        public static void PlayGreeting()
        {
            Task.Run(() => PlayVoiceAsync("greeting.wav"));
        }

        // Plays the goodbye sound when user exits
        // Separate method so we can have different sounds for different events
        public static void PlayGoodbye()
        {
            Task.Run(() => PlayVoiceAsync("goodbye.wav"));
        }

        // Generic speak method - currently plays greeting sound for any text
        // In future versions, this could be expanded to play different sounds
        // for different types of responses
        public static void Speak(string text)
        {
            Task.Run(() => PlayVoiceAsync("greeting.wav"));
        }

        // Private helper method that actually plays the audio file
        // Uses PowerShell with hidden window so no popup appears
        // The SoundPlayer class plays the WAV file synchronously (waits for it to finish)
        private static void PlayVoiceAsync(string fileName)
        {
            try
            {
                // Safety check: make sure the file exists before trying to play it
                if (!File.Exists(fileName))
                    return;

                // Create a ProcessStartInfo to run PowerShell
                // We use PowerShell because it has SoundPlayer built-in
                // WindowStyle.Hidden ensures no black window pops up
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-windowstyle hidden -c \"(New-Object Media.SoundPlayer '{Path.GetFullPath(fileName)}').PlaySync()\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                // Start the process and wait for audio to finish
                Process.Start(psi)?.WaitForExit();
            }
            catch
            {
                // Silent fail - if audio doesn't work, the program continues normally
                // This prevents crashes if the WAV file is missing or corrupted
            }
        }
    }
}