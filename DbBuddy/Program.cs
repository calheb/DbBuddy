using System.Security;
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
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
            else
            {
                Console.WriteLine(@"usage: dbb [start] [set local] [set remote]");
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
                Console.WriteLine($"local Db name: \n\t{connectionStrings.LocalDbName}\n");
                Console.WriteLine($"local Db connection string: \n\t{connectionStrings.LocalDb}\n");
                Console.WriteLine($"remote Db name: \n\t{connectionStrings.RemoteDbName}\n");
                Console.WriteLine($"remote Db connection string: \n\t{connectionStrings.RemoteDb}\n");
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
                    Console.WriteLine($"Current local Db connection string: \n\t{connectionStrings.LocalDb}\n");
                    Console.WriteLine($"Current remote Db connection string: \n\t{connectionStrings.RemoteDb}\n");
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
                Console.WriteLine("Enter the name of the Local Db (maps to the name in the connection string) [x] to return to the settings menu:");
                var userInput1 = Console.ReadLine() ?? string.Empty;
                if (userInput1 == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    connectionStrings.LocalDbName = userInput1;
                }

                Console.WriteLine($"Current Local Db connection string: \n{connectionStrings.LocalDb}\n");
                Console.WriteLine("Enter your Local Db connection string or [x] to return to the settings menu:");
                var userInput2 = Console.ReadLine() ?? string.Empty;

                if (userInput2 == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    connectionStrings.LocalDb = userInput2;
                    connectionStrings.CurrentDb = "Local Db";
                    SaveConnectionStrings(connectionStrings);
                    break;
                }
            }
        }

        static void DisplaySetRemoteDbMenu()
        {
            while (true)
            {
                ShowHeader();
                Console.WriteLine("Enter the name of the remote Db (maps to the name in the connection string) [x] to return to the settings menu:");
                var userInput1 = Console.ReadLine() ?? string.Empty;
                if (userInput1 == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    connectionStrings.RemoteDbName = userInput1;
                }

                Console.WriteLine($"Current Remote Db connection string: \n{connectionStrings.RemoteDb}\n");
                Console.WriteLine("Enter your remote Db connection string or [x] to return to the settings menu:");
                var userInput2 = Console.ReadLine() ?? string.Empty;

                if (userInput2 == "x")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    connectionStrings.RemoteDb = userInput2;
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
            connectionStrings = ReadConnectionStrings();
            connectionStrings.CurrentDb = "Local Db";
            if (string.IsNullOrWhiteSpace(connectionStrings.ConfigPath))
            {
                Console.WriteLine("Web.config path is not set.");
                return;
            }
            ReplaceConnectionStringsBlock(connectionStrings.ConfigPath, CreateConnectionStringBlock(true));
            SaveConnectionStrings(connectionStrings);
            Console.WriteLine("Active Db set to Local Db.\n");
            Console.WriteLine($"Local Db connection string: \n\t{connectionStrings.LocalDb}");
        }

        static void ToggleRemoteDbActive()
        {
            connectionStrings = ReadConnectionStrings();
            connectionStrings.CurrentDb = "Remote Db";
            if (string.IsNullOrWhiteSpace(connectionStrings.ConfigPath))
            {
                Console.WriteLine("Web.config path is not set.");
                return;
            }
            ReplaceConnectionStringsBlock(connectionStrings.ConfigPath, CreateConnectionStringBlock(false));
            SaveConnectionStrings(connectionStrings);
            Console.WriteLine("Active Db set to Remote Db.\n");
            Console.WriteLine($"Remote Db connection string: \n\t{connectionStrings.RemoteDb}");
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
                    LocalDb = "Default",
                    LocalDbName = "Default",
                    RemoteDb = "Default",
                    RemoteDbName = "Default",
                    ConfigPath = "Default",
                    CurrentDb = "Default"
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

            XmlNode oldConnectionStringsNode = doc.SelectSingleNode("//connectionStrings");

            XmlElement newConnectionStringsNode = doc.CreateElement("connectionStrings");
            newConnectionStringsNode.InnerXml = newConnectionStringsBlock;

            if (oldConnectionStringsNode != null)
            {
                doc.DocumentElement.ReplaceChild(newConnectionStringsNode, oldConnectionStringsNode);
            }
            else
            {
                doc.DocumentElement.AppendChild(newConnectionStringsNode);
            }

            doc.Save(webConfigPath);
        }



        static string CreateConnectionStringBlock(bool isLocal)
        {
            string connectionString = isLocal ? connectionStrings.LocalDb : connectionStrings.RemoteDb;
            string dbName = isLocal ? connectionStrings?.LocalDbName : connectionStrings?.RemoteDbName;
            string escapedConnectionString = SecurityElement.Escape(connectionString);

            return
                   $"<add name=\"{dbName}\" connectionString=\"{escapedConnectionString}\" providerName=\"System.Data.SqlClient\" />";
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
