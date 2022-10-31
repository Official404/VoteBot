using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    public class PingCommand
    {
        public static async Task Respons(SocketSlashCommand command)
        {
            await command.RespondAsync("Pong!");
        }
    }
}
