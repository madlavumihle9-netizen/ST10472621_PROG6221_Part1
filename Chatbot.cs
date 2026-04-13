// Chatbot.cs
// The main brain of the application - contains all conversation logic
// This is the largest file because it handles:
//   - User greetings and setup
//   - The main chat loop
//   - Smart responses to 8 different cybersecurity topics
//   - Follow-up question handling
//   - Randomized answers so conversations feel natural

using System;
using System.Collections.Generic;

namespace MizzBox
{
    public class Chatbot
    {
        // Stores the current user - we create this after asking for their name
        // Using a class field so all methods can access it
        private User currentUser;

        // Remembers the last topic discussed - enables follow-up questions
        // Example: User asks "tell me more" and we know they mean "more about passwords"
        private string lastTopic = "";

        // Random number generator for picking different answers
        // Static so we don't create a new one every time
        private Random random = new Random();

        // Main entry point - called from Program.cs
        // Orchestrates the entire flow: greeting → art → name → chat
        public void Start()
        {
            // STEP 1: Play AI voice greeting in background
            // The voice plays while we show the visual elements
            VoiceEngine.PlayGreeting();

            // STEP 2: Display visual elements
            // ASCII art makes the program look professional
            AsciiArt.DisplayLogo();
            AsciiArt.DisplayBanner();
            AsciiArt.DisplayDivider();

            // STEP 3: Get user's name for personalization
            // We store this so we can say "Hello [Name]" instead of generic greetings
            GetUserName();

            // STEP 4: Show welcome message with their name
            // Also displays available topics so users know what to ask
            ShowWelcome();

            // STEP 5: Main conversation loop
            // This runs until user types "exit" or "bye"
            RunChatLoop();
        }

        // Asks for and validates the user's name
        // Uses a while loop to keep asking until valid input is given
        // Input validation prevents empty names which would break personalization
        private void GetUserName()
        {
            UI.Print(">> Enter your name: ", ConsoleColor.Yellow);

            string name = Console.ReadLine();

            // Validation loop - keeps asking until we get a real name
            while (string.IsNullOrWhiteSpace(name))
            {
                UI.ShowError("Please enter a valid name.");
                UI.Print(">> Enter your name: ", ConsoleColor.Yellow);
                name = Console.ReadLine();
            }

            // Create User object with validated name
            currentUser = new User(name);
        }

        // Displays welcome message and available topics
        // Lists all 8 topics so users know what they can ask about
        private void ShowWelcome()
        {
            // Personalized greeting using their name
            UI.TypeWrite("\nHello, " + currentUser.Name + "! Welcome to the Cybersecurity Awareness Bot.\n",
                        ConsoleColor.Cyan);
            UI.TypeWrite("I'm here to help you stay safe online. Ask me about:\n", ConsoleColor.Cyan);

            // List all available topics
            Console.WriteLine("   • Password safety");
            Console.WriteLine("   • Phishing scams");
            Console.WriteLine("   • Safe browsing");
            Console.WriteLine("   • Malware & viruses");
            Console.WriteLine("   • Social engineering");
            Console.WriteLine("   • Two-factor authentication");
            Console.WriteLine("   • Public Wi-Fi safety");
            Console.WriteLine("   • Data backup");
            Console.WriteLine("   • Or type 'exit' to quit\n");
        }

        // Main conversation loop - the heart of the chatbot
        // Runs continuously until user types exit
        // Handles input, gets responses, and displays them
        private void RunChatLoop()
        {
            bool running = true;

            while (running)
            {
                // Show prompt with user's name for personalization
                UI.Print(currentUser.Name + " >> ", ConsoleColor.Yellow);

                // Get user input
                string input = Console.ReadLine();

                // Validate input - can't be empty
                if (string.IsNullOrWhiteSpace(input))
                {
                    UI.ShowError("Please enter something!");
                    continue;  // Skip to next iteration, don't process empty input
                }

                // Get smart response based on what user typed
                // Convert to lowercase for easier matching (case-insensitive)
                string response = GetSmartResponse(input.ToLower());

                // Display bot's response with typing effect
                UI.Print("Bot >> ", ConsoleColor.Green);
                UI.TypeWrite(response + "\n", ConsoleColor.Green);

                // Check if user wants to exit
                if (input.Contains("exit") || input.Contains("bye"))
                {
                    // Play goodbye voice message
                    VoiceEngine.PlayGoodbye();
                    running = false;  // This will exit the while loop
                }
            }

            // Final goodbye message (text only, voice already played)
            UI.TypeWrite("\nGoodbye, " + currentUser.Name + "! Stay safe online!\n",
                        ConsoleColor.Magenta);
        }

        // SMART RESPONSE SYSTEM
        // Analyzes user input and returns appropriate cybersecurity information
        // Features:
        //   - 8 different topics covered
        //   - Multiple randomized answers per topic (feels more natural)
        //   - Follow-up question support ("tell me more")
        //   - Context awareness (remembers last topic)
        private string GetSmartResponse(string input)
        {
            // FIRST: Check if this is a follow-up question
            // Example: User asks "password" then "tell me more"
            if (IsFollowUpQuestion(input))
            {
                return GetFollowUpResponse();
            }

            // TOPIC 1: PASSWORD SAFETY
            // Keywords: password, pass
            // 5 different randomized tips so repeat questions get fresh answers
            if (input.Contains("password") || input.Contains("pass"))
            {
                lastTopic = "password";  // Remember for follow-ups
                return GetRandomResponse(new string[]
                {
                    "Password Safety Tip #1: Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols. Avoid dictionary words!",
                    "Password Safety Tip #2: Never reuse passwords across different sites. If one gets hacked, all your accounts are at risk!",
                    "Password Safety Tip #3: Use a password manager like LastPass or Bitwarden to generate and store strong passwords securely.",
                    "Password Safety Tip #4: Change your passwords every 3-6 months, especially for banking and email accounts.",
                    "Password Safety Tip #5: Don't share passwords via email or text. Use secure password sharing features in password managers."
                });
            }

            // TOPIC 2: PHISHING SCAMS
            // Keywords: phishing, email, scam
            // Phishing is the #1 attack method in South Africa
            else if (input.Contains("phishing") || input.Contains("email") || input.Contains("scam"))
            {
                lastTopic = "phishing";
                return GetRandomResponse(new string[]
                {
                    "Phishing Alert #1: Check the sender's email address carefully. Scammers use addresses that look similar to real companies!",
                    "Phishing Alert #2: Hover over links before clicking to see the real URL. Don't click if it looks suspicious!",
                    "Phishing Alert #3: Legitimate companies will never ask for your password or banking details via email.",
                    "Phishing Alert #4: Look for spelling errors and urgent language like 'Act now!' or 'Your account will be closed!'",
                    "Phishing Alert #5: When in doubt, contact the company directly using their official website, not the email contact info."
                });
            }

            // TOPIC 3: SAFE BROWSING
            // Keywords: browse, internet, safe
            else if (input.Contains("browse") || input.Contains("internet") || input.Contains("safe"))
            {
                lastTopic = "browsing";
                return GetRandomResponse(new string[]
                {
                    "Safe Browsing Tip #1: Always check for 'https://' and the padlock icon before entering sensitive information.",
                    "Safe Browsing Tip #2: Keep your browser updated. Updates often include security patches for known vulnerabilities.",
                    "Safe Browsing Tip #3: Use an ad-blocker and anti-malware extension to block malicious websites and ads.",
                    "Safe Browsing Tip #4: Don't download software from untrusted websites. Stick to official app stores and vendor sites.",
                    "Safe Browsing Tip #5: Clear your cookies and cache regularly, and use private browsing for sensitive searches."
                });
            }

            // TOPIC 4: MALWARE & VIRUSES
            // Keywords: malware, virus, antivirus
            else if (input.Contains("malware") || input.Contains("virus") || input.Contains("antivirus"))
            {
                lastTopic = "malware";
                return GetRandomResponse(new string[]
                {
                    "Malware Protection #1: Install reputable antivirus software like Windows Defender, Malwarebytes, or Norton.",
                    "Malware Protection #2: Keep your operating system and all software updated. Updates patch security holes!",
                    "Malware Protection #3: Never open email attachments from unknown senders. Even PDFs can contain malware.",
                    "Malware Protection #4: Backup your important data regularly to an external drive or cloud service.",
                    "Malware Protection #5: If your computer is slow, crashes often, or shows pop-ups, scan for malware immediately!"
                });
            }

            // TOPIC 5: SOCIAL ENGINEERING
            // Keywords: social engineering, manipulate, trick
            // Social engineering targets human psychology, not technology
            else if (input.Contains("social engineering") || input.Contains("manipulate") || input.Contains("trick"))
            {
                lastTopic = "social";
                return GetRandomResponse(new string[]
                {
                    "Social Engineering Defense #1: Be skeptical of urgent requests. Scammers create false urgency to pressure you!",
                    "Social Engineering Defense #2: Verify identities before sharing information. Call back using official numbers, not ones provided.",
                    "Social Engineering Defense #3: Don't let strangers into secure areas, even if they look official. Verify with management.",
                    "Social Engineering Defense #4: Be careful what you share on social media. Scammers use personal info to craft convincing attacks.",
                    "Social Engineering Defense #5: When in doubt, double-check! Take time to verify before acting on requests."
                });
            }

            // TOPIC 6: TWO-FACTOR AUTHENTICATION (2FA)
            // Keywords: 2fa, two factor, authentication
            // 2FA is one of the best security measures available
            else if (input.Contains("2fa") || input.Contains("two factor") || input.Contains("two-factor") || input.Contains("authentication"))
            {
                lastTopic = "2fa";
                return GetRandomResponse(new string[]
                {
                    "2FA Tip #1: Always enable Two-Factor Authentication on your important accounts (email, banking, social media).",
                    "2FA Tip #2: Use an authenticator app like Google Authenticator or Microsoft Authenticator instead of SMS when possible.",
                    "2FA Tip #3: Save your backup codes in a secure location. You'll need them if you lose your phone!",
                    "2FA Tip #4: Never share your 2FA codes with anyone, even if they claim to be from tech support.",
                    "2FA Tip #5: 2FA adds an extra layer of security. Even if hackers get your password, they can't access your account without the second factor!"
                });
            }

            // TOPIC 7: PUBLIC WI-FI SAFETY
            // Keywords: wifi, wi-fi, public, hotspot
            // Public Wi-Fi is convenient but dangerous
            else if (input.Contains("wifi") || input.Contains("wi-fi") || input.Contains("public") || input.Contains("hotspot"))
            {
                lastTopic = "wifi";
                return GetRandomResponse(new string[]
                {
                    "Public Wi-Fi Safety #1: Avoid accessing banking or shopping sites on public Wi-Fi. Use your mobile data instead!",
                    "Public Wi-Fi Safety #2: Use a VPN (Virtual Private Network) to encrypt your traffic on public networks.",
                    "Public Wi-Fi Safety #3: Turn off auto-connect to Wi-Fi on your devices. You might connect to a fake hotspot!",
                    "Public Wi-Fi Safety #4: Verify the network name with staff before connecting. Scammers create fake networks with similar names.",
                    "Public Wi-Fi Safety #5: Forget the network after use so your device doesn't auto-connect to it later."
                });
            }

            // TOPIC 8: DATA BACKUP
            // Keywords: backup, save data, cloud, restore
            // The 3-2-1 rule is industry standard
            else if (input.Contains("backup") || input.Contains("save data") || input.Contains("cloud") || input.Contains("restore"))
            {
                lastTopic = "backup";
                return GetRandomResponse(new string[]
                {
                    "Data Backup Tip #1: Follow the 3-2-1 rule: 3 copies of data, 2 different media types, 1 offsite/cloud copy.",
                    "Data Backup Tip #2: Use cloud services like Google Drive, OneDrive, or Dropbox for automatic backups.",
                    "Data Backup Tip #3: Test your backups regularly! A backup you can't restore is useless.",
                    "Data Backup Tip #4: Keep an offline backup (external hard drive) disconnected from your computer for ransomware protection.",
                    "Data Backup Tip #5: Backup your phone too! Photos and contacts are easily lost if your phone is stolen or damaged."
                });
            }

            // CASUAL CONVERSATION: How are you
            // Multiple responses for variety
            else if (input.Contains("how are you") || input.Contains("how r u"))
            {
                return GetRandomResponse(new string[]
                {
                    "I'm just code, but I'm functioning perfectly and ready to help you stay secure!",
                    "I'm doing great! Ready to teach you about cybersecurity today.",
                    "All systems operational! What cybersecurity topic would you like to learn about?",
                    "I'm excellent! Excited to help you protect yourself online."
                });
            }

            // CASUAL CONVERSATION: Purpose/What do you do
            else if (input.Contains("purpose") || input.Contains("what do you do") || input.Contains("who are you"))
            {
                return GetRandomResponse(new string[]
                {
                    "I am a Cybersecurity Awareness Bot. I educate South African citizens about online threats like phishing, malware, and social engineering.",
                    "My purpose is to help you stay safe online by teaching you about password safety, phishing scams, and safe browsing habits.",
                    "I'm your virtual cybersecurity assistant! I provide tips and guidance on protecting yourself from cyber threats."
                });
            }

            // HELP: Show all available topics
            else if (input.Contains("help") || input.Contains("what can i ask"))
            {
                return "You can ask me about: password safety, phishing scams, safe browsing, malware protection, social engineering, two-factor authentication, public Wi-Fi safety, or data backup. What interests you?";
            }

            // EXIT: Say goodbye
            else if (input.Contains("exit") || input.Contains("bye") || input.Contains("quit"))
            {
                return "Thank you for using the Cybersecurity Awareness Bot. Remember to stay vigilant and stay safe online!";
            }

            // UNKNOWN INPUT: Provide helpful guidance
            else
            {
                return "I didn't understand that. Try asking about: passwords, phishing, safe browsing, malware, social engineering, 2FA, Wi-Fi safety, or data backup. Type 'help' for all options.";
            }
        }

        // Checks if user is asking a follow-up question
        // Follow-ups work because we remember the lastTopic variable
        // Example: User asks "password" → lastTopic = "password"
        //          User asks "tell me more" → we know they want more about passwords!
        private bool IsFollowUpQuestion(string input)
        {
            // Can't have a follow-up if we haven't discussed anything yet
            if (string.IsNullOrEmpty(lastTopic))
                return false;

            // Common follow-up phrases
            string[] followUpWords = { "tell me more", "more info", "elaborate", "explain more", "what else", "anything else", "more" };

            // Check if input contains any follow-up phrase
            foreach (string word in followUpWords)
            {
                if (input.Contains(word))
                    return true;
            }

            return false;
        }

        // Returns additional information about the last discussed topic
        // This makes the conversation feel more natural and intelligent
        private string GetFollowUpResponse()
        {
            switch (lastTopic)
            {
                case "password":
                    return "Here's more on passwords: Consider using passphrases - long phrases with spaces like 'My dog has 7 spots!' are easier to remember and harder to crack!";
                case "phishing":
                    return "More on phishing: Check for poor grammar and generic greetings like 'Dear Customer' instead of your name. These are red flags!";
                case "browsing":
                    return "More on browsing: Enable 'Do Not Track' in your browser settings and review site permissions regularly.";
                case "malware":
                    return "More on malware: Be careful of USB drives! Never plug in unknown USBs - they can auto-run malware.";
                case "social":
                    return "More on social engineering: Beware of 'CEO fraud' where scammers pretend to be your boss asking for urgent transfers.";
                case "2fa":
                    return "More on 2FA: Hardware security keys (like YubiKey) are the most secure 2FA method for high-value accounts.";
                case "wifi":
                    return "More on Wi-Fi: Disable file sharing and AirDrop when on public networks to prevent unauthorized access.";
                case "backup":
                    return "More on backups: Encrypt sensitive backups with a password in case the drive is lost or stolen.";
                default:
                    return "What specific topic would you like to know more about?";
            }
        }

        // Helper method: picks a random response from an array
        // This prevents repetitive conversations - same question gets different answer
        // Uses Random class initialized at the top of the class
        private string GetRandomResponse(string[] responses)
        {
            int index = random.Next(responses.Length);
            return responses[index];
        }
    }
}