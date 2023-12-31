using System.ComponentModel.DataAnnotations;
using System.Xml;
using Newtonsoft.Json;

namespace DbBuddy
{
    class Program
    {
        static ConnectionStrings connectionStrings = new ConnectionStrings();
        static bool isShowingMenu = false;
        
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "start":
                        isShowingMenu = true;
                        DisplayMainMenu();
                        break;
                    case "set":
                        if (args.Length > 1)
                        {
                            if (args[1].ToLower() == "local")
                            {
                                ToggleLocalDbActive();
                            }
                            else if (args[1].ToLower() == "remote")
                            {
                                ToggleRemoteDbActive();
                            }
                        }
                        break;
                    // You can add more cases here for other commands.
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No arguments provided.");
                // Optionally, start the main menu by default if no arguments are provided.
                // DisplayMainMenu();
            }
        }

        static void DisplayMainMenu()
        {
            bool validInput = false;
            while (!validInput)
            {
                connectionStrings = ReadConnectionStrings();
                if (String.IsNullOrEmpty(connectionStrings.CurrentDb))
                {
                    connectionStrings.CurrentDb = "Not set.";
                }

                ShowHeader();
                Console.WriteLine("Main Menu");
                Console.WriteLine("---------");
                Console.WriteLine("Select an option:");
                Console.WriteLine("[1] Local Db");
                Console.WriteLine("[2] Remote Db");
                Console.WriteLine("[3] Settings");
                Console.WriteLine("[4] Exit");

                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            Console.Clear();
                            ToggleLocalDbActive();
                            validInput = true;
                            break;
                        case 2:
                            Console.Clear();
                            ToggleRemoteDbActive();
                            validInput = true;
                            break;
                        case 3:
                            Console.Clear();
                            DisplaySettingsMenu();
                            validInput = true;
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
                else
                {
                    Console.Clear();
                    ShowHeader();
                    Console.WriteLine("Main Menu");
                    Console.WriteLine("---------");
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("[1] Local Db");
                    Console.WriteLine("[2] Remote Db");
                    Console.WriteLine("[3] Settings");
                    Console.WriteLine("[4] Exit");
                }
            }
        }

        static void DisplaySettingsMenu()
        {
            bool validInput = false;
            while (!validInput)
            {
                ShowHeader();
                Console.WriteLine($"Current project path: \n\t{connectionStrings.ConfigPath} \n");
                Console.WriteLine($"Current local Db connection string: \n\t{connectionStrings.LocalDbPath}\n");
                Console.WriteLine($"Current remote Db connection string: \n\t{connectionStrings.RemoteDbPath}\n");
                Console.WriteLine();
                Console.WriteLine("Settings");
                Console.WriteLine("---------");
                Console.WriteLine("[1] Set Local Db");
                Console.WriteLine("[2] Set Remote Db");
                Console.WriteLine("[3] Set project path");
                Console.WriteLine("[4] Main Menu");

                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:
                            Console.Clear();
                            DisplaySetLocalDbMenu();
                            validInput = true;
                            break;
                        case 2:
                            Console.Clear();
                            DisplaySetRemoteDbMenu();
                            validInput = true;
                            break;
                        case 3:
                            Console.Clear();
                            DisplaySetProjectPath();
                            validInput = true;
                            break;
                        case 4:
                            Console.Clear();
                            DisplayMainMenu();
                            validInput = true;
                            break;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
                else
                {
                    Console.Clear();
                    ShowHeader();
                    Console.WriteLine($"Current project path: \n\t{connectionStrings.ConfigPath} \n");
                    Console.WriteLine($"Current local Db connection string: \n\t{connectionStrings.LocalDbPath}\n");
                    Console.WriteLine($"Current remote Db connection string: \n\t{connectionStrings.RemoteDbPath}\n");
                    Console.WriteLine();
                    Console.WriteLine("Settings");
                    Console.WriteLine("---------");
                    Console.WriteLine("[1] Set Local Db");
                    Console.WriteLine("[2] Set Remote Db");
                    Console.WriteLine("[3] Set project path");
                    Console.WriteLine("[4] Main Menu");
                }
            }
        }


        static void DisplaySetLocalDbMenu()
        {
            while (true)
            {
                ShowHeader();
                Console.WriteLine($"Current local connection string: \n{connectionStrings.LocalDbPath}\n");
                Console.WriteLine("Enter your local Db connection string or [x] to return to the settings menu:");
                var userInput = Console.ReadLine() ?? string.Empty;

                if (userInput == "x")
                {
                    Console.Clear();
                    break; // Exits the loop and goes back to the previous menu
                }
                else
                {
                    connectionStrings.LocalDbPath = userInput;
                    connectionStrings.CurrentDb = "local Db";
                    SaveConnectionStrings(connectionStrings);
                    break;
                }
            }

            DisplaySettingsMenu();
        }


        static void DisplaySetRemoteDbMenu()
        {
            while (true)
            {
                ShowHeader();
                Console.WriteLine($"Current remote connection string: \n{connectionStrings.RemoteDbPath}\n");
                Console.WriteLine("Enter your remote Db connection string or [x] to return to the settings menu:");
                var userInput = Console.ReadLine() ?? string.Empty;

                if (userInput == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    connectionStrings.RemoteDbPath = userInput;
                    connectionStrings.CurrentDb = "remote Db";
                    SaveConnectionStrings(connectionStrings);
                    break;
                }
            }
        }


        static void DisplaySetProjectPath()
        {
            while (true)
            {
                ShowHeader();
                Console.WriteLine($"Current Web.config path: \n{connectionStrings.ConfigPath}\n");
                Console.WriteLine("Enter the complete path to the web.config file in your project or [x] to return to the settings menu:");
                string userInput = Console.ReadLine() ?? string.Empty;

                if (userInput == null || userInput == "")
                {
                    Console.Clear();
                    continue;
                }
                else if (userInput == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    SaveProjPath(userInput);
                    Console.WriteLine("Project path saved.");
                    break;
                }
            }
        }


        static void SaveProjPath(string projPath)
        {
            connectionStrings.ConfigPath = projPath;
            SaveConnectionStrings(connectionStrings);
        }

        static void ToggleLocalDbActive()
        {
            connectionStrings.CurrentDb = "Local Db";
            ReplaceConnectionStringsBlock(connectionStrings.ConfigPath, CreateConnectionStringBlock(true));
            SaveConnectionStrings(connectionStrings);
        }

        static void ToggleRemoteDbActive()
        {
            connectionStrings.CurrentDb = "Remote Db";
            ReplaceConnectionStringsBlock(connectionStrings.ConfigPath, CreateConnectionStringBlock(false));
            SaveConnectionStrings(connectionStrings);
        }


        static void SaveConnectionStrings(ConnectionStrings connectionStrings)
        {
            string jsonString = JsonConvert.SerializeObject(connectionStrings);
            File.WriteAllText("connectionStrings.json", jsonString);
            if (isShowingMenu)
            {
                Console.Clear();
                DisplaySettingsMenu();
            }
        }

        public static ConnectionStrings ReadConnectionStrings()
        {
            string filePath = "connectionStrings.json";
            if (!File.Exists(filePath))
            {
                // Create file with default settings
                var defaultSettings = new ConnectionStrings
                {
                    LocalDbPath = "",
                    RemoteDbPath = "",
                    ConfigPath = "",
                    CurrentDb = ""
                };
                File.WriteAllText(filePath, JsonConvert.SerializeObject(defaultSettings));
            }
            string jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ConnectionStrings>(jsonString);
        }


        static void ReplaceConnectionStringsBlock(string webConfigPath, string newConnectionStringsBlock)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(webConfigPath);

            // Find the existing connectionStrings node
            XmlNode oldConnectionStringsNode = doc.SelectSingleNode("//connectionStrings");

            // Create a new element for connectionStrings and parse the new block
            XmlElement newConnectionStringsNode = doc.CreateElement("connectionStrings");
            newConnectionStringsNode.InnerXml = newConnectionStringsBlock;

            if (oldConnectionStringsNode != null)
            {
                // Replace the old node with the new one
                doc.DocumentElement.ReplaceChild(newConnectionStringsNode, oldConnectionStringsNode);
            }
            else
            {
                // If the connectionStrings node doesn't exist, append the new one
                doc.DocumentElement.AppendChild(newConnectionStringsNode);
            }

            doc.Save(webConfigPath);
        }



        static string CreateConnectionStringBlock(bool isLocal)
        {
            string connectionString = isLocal ? connectionStrings.LocalDbPath : connectionStrings.RemoteDbPath;

            return
                   $"<add name=\"localDbName\" connectionString=\"{connectionString}\" providerName=\"System.Data.SqlClient\" />";
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