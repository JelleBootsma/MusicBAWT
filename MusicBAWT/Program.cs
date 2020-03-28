using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Discord;

using Discord.Commands;
using Discord.WebSocket;
using MusicBAWT.Objects;
using MusicBAWT.Services;

namespace MusicBAWT
{
    public class Program
    {
        private DiscordSocketClient _client;
        private readonly IConfiguration _config;
        public static List<Channel> ConnectedChannels = new List<Channel>();
        public Program()
        {
            // create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");  

            // build the configuration and assign to _config          
            _config = _builder.Build();
        }
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // call ConfigureServices to create the ServiceCollection/Provider for passing around the services
            using (var services = ConfigureServices())
            {
                // Create the Discord client by using Required Services
                var client = services.GetRequiredService<DiscordSocketClient>();
                _client = client;

                // Add logging method
                client.Log += Log;
                services.GetRequiredService<CommandService>().Log += Log;

                // Load Token from JSON config
                await client.LoginAsync(TokenType.Bot, _config["Token"]);
                await client.StartAsync();

                // Get the CommandHandler and initialize it
                await services.GetRequiredService<CommandHandler>().InitializeAsync();

                // Make sure the bot doesn't quit
                await Task.Delay(-1);
            }
        }
        
        /// <summary>
        /// Logs a message to the console output
        /// </summary>
        /// <param name="msg">message to be logged</param>
        /// <returns>Task.CompletedTask</returns>
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        //  Configure the services that this bot will use
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
        
    }
}