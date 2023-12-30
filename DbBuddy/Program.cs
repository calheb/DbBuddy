using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace DbBuddy
{
    class Program
    {
        static ConnectionStrings connectionStrings = new ConnectionStrings();

        static void Main(string[] args)
        {
            DisplayMainMenu();
        }

        static void DisplayMainMenu()
        {
            connectionStrings = ReadConnectionStrings();
            if ((String.IsNullOrEmpty(connectionStrings.CurrentDb)))
            {
                connectionStrings.CurrentDb = "Not set.";
            }            
            ShowHeader();
            Console.WriteLine("Main Menu");
            Console.WriteLine("---------");
            Console.WriteLine("Select an option:");
            Console.WriteLine("[1] Settings");
            Console.WriteLine("[2] Remote Db");
            Console.WriteLine("[3] Local Db");
            Console.WriteLine("[4] Settings");

            int userInput = int.Parse(Console.ReadLine() ?? null);

            if (userInput == 1)
            {
                Console.Clear();
                DisplaySettingsMenu();
            }
            else if (userInput == 2)
            {
                Console.Clear();
                ToggleRemoteDbActive();
            }
            else if (userInput == 3)
            {
                Console.Clear();
                ToggleLocalDbActive();

            }
            else
            {
                DisplayMainMenu();
            }
        }

        static void DisplaySettingsMenu()
        {
            ShowHeader();
            Console.WriteLine("Settings");
            Console.WriteLine("---------");
            Console.WriteLine("[1] Set Local Db");
            Console.WriteLine("[2] Set Remote Db");
            Console.WriteLine("[3] Main Menu");

            int userInput = int.Parse(Console.ReadLine() ?? null);

            if (userInput == 1)
            {
                Console.Clear();
                DisplaySetLocalDbMenu();
            }
            else if (userInput == 2)
            {
                Console.Clear();
                DisplaySetRemoteDbMenu();
            }
            else if (userInput == 3)
            {
                Console.Clear();
                DisplayMainMenu();
            }
            else
            {
                DisplaySettingsMenu();
            }
        }

        static void DisplaySetLocalDbMenu()
        {
            ShowHeader();
            Console.WriteLine("Enter your local Db connection string:");
            connectionStrings.LocalDbPath = Console.ReadLine() ?? string.Empty;
            connectionStrings.CurrentDb = "Local Db";
            SaveConnectionStrings(connectionStrings);
        }

        static void DisplaySetRemoteDbMenu()
        {
            ShowHeader();
            Console.WriteLine("Enter your remote Db connection string:");
            connectionStrings.RemoteDbPath = Console.ReadLine() ?? string.Empty;
            connectionStrings.CurrentDb = "remote Db";
            SaveConnectionStrings(connectionStrings);
        }

        static void DisplaySetProjectPath()
        {
            ShowHeader();
            Console.WriteLine("Enter the complete path to the web.config file in your project:");
            string userInput = Console.ReadLine() ?? string.Empty;
            if (userInput == null)
            {
                Console.Clear();
                DisplaySetProjectPath();
            }
            else
            {
                Console.Clear();
                SaveProjPath(userInput);
                Console.WriteLine("Project path saved.");
                DisplayMainMenu();
            }
        }

        static void SaveProjPath(string projPath)
        {
            connectionStrings.ConfigPath = projPath;
            SaveConnectionStrings(connectionStrings);
        }

        static void ToggleRemoteDbActive()
        {
            connectionStrings.CurrentDb = "Remote Db";
            SaveConnectionStrings(connectionStrings);
        }

        static void ToggleLocalDbActive()
        {
            connectionStrings.CurrentDb = "Local Db";
            SaveConnectionStrings(connectionStrings);
        }
        public static void SaveConnectionStrings(ConnectionStrings connectionStrings)
        {
            string jsonString = JsonConvert.SerializeObject(connectionStrings);
            File.WriteAllText("connectionStrings.json", jsonString);
            DisplayMainMenu();
        }

        public static ConnectionStrings ReadConnectionStrings()
        {
            string jsonString = File.ReadAllText("connectionStrings.json");
            if (string.IsNullOrEmpty(jsonString))
            {
                return new ConnectionStrings();
            }
            else
            {
                return JsonConvert.DeserializeObject<ConnectionStrings>(jsonString);
            }
        }

        static void ShowHeader()
        {
            Console.WriteLine("  ____  _     ____            _     _       ");
            Console.WriteLine(" |  _ \\| |__ | __ ) _   _  __| | __| |_   _ ");
            Console.WriteLine(" | | | | '_ \\|  _ \\| | | |/ _` |/ _` | | | |");
            Console.WriteLine(" | |_| | |_) | |_) | |_| | (_| | (_| | |_| |");
            Console.WriteLine(" |____/|_.__/|____/ \\__,_|\\__,_|\\__,_|\\__, |");
            Console.WriteLine("                                      |___/ ");
            Console.WriteLine();
            Console.WriteLine($"Current Db: {connectionStrings.CurrentDb}");
            Console.WriteLine();
        }
    }
}