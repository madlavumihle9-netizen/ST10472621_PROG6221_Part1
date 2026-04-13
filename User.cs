// User.cs
// This class stores information about the user
// We use auto-properties which is a C# feature that makes code cleaner
// The user's name is stored here so we can personalize responses

namespace MizzBox
{
    // User class - represents a person using our chatbot
    public class User
    {
        // Auto-property: automatically creates a private field and getter/setter
        // This stores the user's name so we can say "Hello [Name]!" instead of generic greetings
        public string Name { get; set; }

        // Constructor - runs when we create a new User object
        // We pass the name here and store it in our property
        public User(string name)
        {
            Name = name;
        }
    }
}