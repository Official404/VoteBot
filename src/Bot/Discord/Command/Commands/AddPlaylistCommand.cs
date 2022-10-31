using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class AddPlaylistCommand
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            string playlist = command.Data.Options.First().Value.ToString();

            var temp1 = playlist.Remove(0,34);
            var temp2 = temp1.Remove(22, temp1.Length - 22);

            List<string> testDatabase = database.GetPlaylists();
            
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Playlist is already added!");
                    return;
                }
            }

            database.AddPlaylist(temp2);

            testDatabase = database.GetPlaylists();
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Playlist was added to roster!");
                    return;
                }
            }
            await command.RespondAsync("Something went wrong, playlist was not added to roster!");
        }
    }
}
