using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Net;
using Newtonsoft.Json;
using System.Reactive;

namespace VoteBot
{
    class VoteBot
    {
        static Task Main(string[] args) {
            while (true) {
                try
                {
                    new VoteBot().MainAsync().GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private readonly DiscordSocketClient _Client;
        private readonly CommandService _Command;
        private  SlashCommandHandler _sCmdHandler;
        private  Database _BotDatabase;
        private  SpotifyAPI _SpotifyAPI;

        private VoteBot()
        {
            Console.WriteLine("Starting VoteBot...");
            Console.WriteLine("Setting up configs...");
            _Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 50,
                GatewayIntents = GatewayIntents.All
            });

            _Command = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                CaseSensitiveCommands = false
            });
        }

        private async Task MainAsync()
        {
            Console.WriteLine("Initializing Logger...");
            LoggingService logger = new LoggingService(_Client, _Command);
            Console.WriteLine("Initializing command handler...");

            _SpotifyAPI = new SpotifyAPI();

            Console.WriteLine("Initializing database...");
            _BotDatabase = new Database("Server=localhost;Database=TestDiscordBot;Trusted_Connection=True;", _SpotifyAPI);
            _BotDatabase.TestConnection();

            Console.WriteLine("Installing Commands...");
            CommandHandler cmdHandler = new CommandHandler(_Client, _Command);
            await cmdHandler.InstallCommandsAsync();
            _sCmdHandler = new SlashCommandHandler(_Client, _Command, _BotDatabase);

            Console.WriteLine("Logging in to discord...");
            await _Client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DKEY"));
            await _Client.StartAsync();

            _Client.Ready += Client_Ready;


            await Task.Delay(-1);
        }

        public async Task Client_Ready()
        {
            Console.WriteLine("Bot connected!");
            _Client.SetStatusAsync(UserStatus.Online);

            await _sCmdHandler.initCommands();
        }
    }
}
