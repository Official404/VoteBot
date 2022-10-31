using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot {
    internal class AddTrustedUserCommand
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            var guildUser = (SocketGuildUser)command.Data.Options.First().Value;
            
            List<ulong> testDatabase = database.GetTrustedUsers();
            foreach (ulong id in testDatabase)
            {
                if (guildUser.Id == id)
                {
                    await command.RespondAsync("User is already trusted!");
                    return;
                }
            }

            database.AddTrustedUser(guildUser.Id);
            
            testDatabase = database.GetTrustedUsers();
            foreach (ulong id in testDatabase)
            {
                if (guildUser.Id == id)
                {
                    await command.RespondAsync("User added as trusted!");
                    return;
                }
            }
            await command.RespondAsync("Something went wrong, user was not added as trusted!");
        }
    }
}
