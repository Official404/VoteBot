using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class AddVoteSong
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            var songLink = command.Data.Options.First().Value.ToString();

            var temp1 = songLink.Remove(0, 31);
            var temp2 = temp1.Remove(22, temp1.Length - 22);

            List<string> testDatabase = database.GetSongs();
            
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Song is already added!");
                    return;
                }
            }

            database.AddSong(songLink);

            testDatabase = database.GetSongs();
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Song was added to vote list!");
                    return;
                }
            }
            await command.RespondAsync("Something went wrong, song was not added to vote list!");
        }
    }
}
