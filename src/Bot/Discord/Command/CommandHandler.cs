using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    public class CommandHandler {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;

        public CommandHandler(DiscordSocketClient client, CommandService command)
        {
            _client = client;
            _command = command;
        }
        
        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _command.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), null);
        }

        public async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.Author.IsBot)) return;

            var context = new SocketCommandContext(_client, message);

            await _command.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}
