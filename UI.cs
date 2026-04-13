// UI.cs
// Handles all console formatting and visual effects
// This separates the "look" from the "logic" - good programming practice
// Contains color printing and typing effects for better user experience

using System;
using System.Threading;

namespace MizzBox
{
    // Static class - utility methods that don't need object instances
    public static class UI
    {
        // Prints text in a specific color without moving to next line
        // Useful for prompts where user types on same line
        // Example: UI.Print("Enter name: ", ConsoleColor.Yellow);
        public static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        // Prints text in a specific color and moves to next line
        // Useful for displaying messages
        // Example: UI.PrintLine("Hello!", ConsoleColor.Green);
        public static void PrintLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // Creates a "typing" effect where text appears character by character
        // This simulates a real conversation and makes the bot feel more alive
        // delay = 15ms between characters (adjustable)
        public static void TypeWrite(string text, ConsoleColor color, int delay = 15)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);  // Pause briefly between each character
            }
            Console.ResetColor();
        }

        // Displays error messages in red with a warning symbol
        // Consistent error formatting helps users recognize problems quickly
        public static void ShowError(string message)
        {
            PrintLine("⚠ " + message, ConsoleColor.Red);
        }
    }
}