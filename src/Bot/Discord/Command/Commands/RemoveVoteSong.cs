using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class RemoveVoteSong
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            var songLink = command.Data.Options.First().Value.ToString();
            
            var temp1 = songLink.Remove(0, 31);
            var temp2 = temp1.Remove(22, temp1.Length - 22);

            database.RemoveSong(songLink);
            List<string> testDatabase = database.GetSongs();
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Something went wrong, song was not removed from vote list!");
                    return;
                }
            }

            await command.RespondAsync("Song removed from the vote list!");
        }
    }
}
