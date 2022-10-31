using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class RemoveTrustedUserCommand
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            var guildUser = (SocketGuildUser)command.Data.Options.First().Value;

            List<ulong> testDatabase = database.GetTrustedUsers();
            foreach (ulong id in testDatabase)
            {
                if (guildUser.Id == id)
                {
                    await command.RespondAsync("User is already not trusted!");
                    return;
                }
            }

            database.RemoveTrustedUser(guildUser.Id);

            testDatabase = database.GetTrustedUsers();
            foreach (ulong id in testDatabase)
            {
                if (guildUser.Id == id)
                {
                    await command.RespondAsync("Something went wrong, user was not removed as trusted!");
                    return;
                }
            }
            await command.RespondAsync("User removed as trusted!");
        }
    }
}
