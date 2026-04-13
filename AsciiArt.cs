// AsciiArt.cs
// Contains all the visual ASCII art for the chatbot
// ASCII art makes the console application look more professional and engaging
// We use the @ symbol for multi-line strings in C#

// Added ASCII art display

using System;

namespace MizzBox
{
    // Static class - no need to create objects, just call methods directly
    public static class AsciiArt
    {
        // Displays the main "Cyber Bot" logo
        // Generated using https://patorjk.com/software/taag/ (Big font)
        // The green color gives a "matrix/hacker" cybersecurity feel
        public static void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
   ____      _                  _                  _   
  / ___|   _| |__   ___  _ __  | |__   ___  _ __  | |_ 
 | |  | | | | '_ \ / _ \| '__| | '_ \ / _ \| '__| | __|
 | |__| |_| | |_) | (_) | |    | |_) | (_) | |    | |_ 
  \____\__, |_.__/ \___/|_|    |_.__/ \___/|_|     \__|
       |___/                                            
            ");
            Console.ResetColor();
        }

        // Displays the banner with program name and tagline
        // Uses box-drawing characters for a professional border
        // DarkGreen is slightly different from the logo Green for visual hierarchy
        public static void DisplayBanner()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║     CYBERSECURITY AWARENESS ASSISTANT v1.0            ║");
            Console.WriteLine("║     Keeping South Africa Safe Online                  ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // Displays a divider line to separate sections
        // Helps organize the console output visually
        // Cyan color provides good contrast against black background
        public static void DisplayDivider()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n═══════════════════════════════════════════════════════\n");
            Console.ResetColor();
        }
    }
}
