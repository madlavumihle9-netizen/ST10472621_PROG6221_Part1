// Program.cs
// This is the entry point of our Cybersecurity Awareness Bot
// It simply creates a Chatbot object and starts the program
// PROG6221 Part 1 - Cybersecurity Chatbot

using System;

namespace MizzBox
{
    class Program
    {
        // Main method - this is where the program starts running
        // Every C# application needs a Main method as the entry point
        static void Main(string[] args)
        {
            // Create a new Chatbot object
            // This object contains all the chatbot logic and features
            Chatbot bot = new Chatbot();

            // Start the chatbot - this launches the greeting, ASCII art, and chat loop
            bot.Start();
        }
    }
}