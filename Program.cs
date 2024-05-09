using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using OMNI.Commands;
using OMNI.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OMNI
{

    internal class Program
    {
        private static DiscordClient Client { get; set; }

        static async Task Main(string[] args)
        {
            // Fancify
            Console.Title = "OMNI";
            string fileContents = File.ReadAllText("..\\..\\intro.txt");
            Console.WriteLine(fileContents);

            // Config and directory paths
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configFilePath = Path.Combine(appDataPath, ".omni", "config.json");
            string directoryPath = Path.GetDirectoryName(configFilePath);

            // Check if config already exists
            if (!File.Exists(configFilePath))
            {
                // Read user input for token
                Console.WriteLine("Token:");
                string token = Console.ReadLine();

                // Insert user input into a dictionary variable
                var tokenData = new Dictionary<string, string>
                {
                    { "Token", token }
                };

                // Create directory
                Directory.CreateDirectory(directoryPath);

                // Create config inside of directory and append dictionary
                using (StreamWriter file = File.CreateText(configFilePath))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };
                    jsonSerializer.Serialize(file, tokenData);
                }
            }

            // Deserialize data from config
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON(configFilePath);

            // Bot configuration setup   
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            // Apply config to DiscordClient
            Client = new DiscordClient(discordConfig);

            // Set default timeout for commands with interactivity
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            // Set Task Handler Ready event
            // Additionally other event handlers if needed
            Client.Ready += OnClientReady;

            // Allow client to use slash commands
            var slashCommandsConfiguration = Client.UseSlashCommands();

            // Register commands
            slashCommandsConfiguration.RegisterCommands<OmniCommands>();

            // Connect client to Discord gateway and run infinitely
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            await sender.UpdateStatusAsync(new DiscordActivity("tickets!", ActivityType.Watching));
        }
    }
}
