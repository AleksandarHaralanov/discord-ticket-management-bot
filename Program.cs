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
            Console.Title = "OMNI";
            string fileContents = File.ReadAllText("..\\..\\intro.txt");
            Console.WriteLine(fileContents);

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configFilePath = Path.Combine(appDataPath, ".omni", "config.json");
            string directoryPath = Path.GetDirectoryName(configFilePath);

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("OMNI has detected that the bot token has not yet been configured. Please set up the bot token to continue.\n");
                Console.Write(" > Token: ");
                string token = Console.ReadLine();
                Console.WriteLine("\nToken will be saved at: " + configFilePath + "\n");
                Console.WriteLine("Please note this directory before proceeding, as entering an incorrect token will cause the bot to crash.\nIf this occurs, delete the configuration and restart the setup.\n");
                Console.Write(" > Press any key to continue.");
                Console.ReadKey();

                var tokenData = new Dictionary<string, string>
                {
                    { "Token", token }
                };

                Directory.CreateDirectory(directoryPath);

                using (StreamWriter file = File.CreateText(configFilePath))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };
                    jsonSerializer.Serialize(file, tokenData);
                }
            }

            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON(configFilePath);

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            Client.Ready += OnClientReady;

            var slashCommandsConfiguration = Client.UseSlashCommands();

            slashCommandsConfiguration.RegisterCommands<OmniCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            await sender.UpdateStatusAsync(new DiscordActivity("tickets!", ActivityType.Watching));
        }
    }
}
