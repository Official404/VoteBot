using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class RemovePlaylistCommand
    {
        public static async Task Respons(SocketSlashCommand command, Database database)
        {
            var playlistName = command.Data.Options.First().Value.ToString();

            var temp1 = playlistName.Remove(0, 34);
            var temp2 = temp1.Remove(22, temp1.Length - 22);

            database.RemovePlaylist(temp2);
            List<string> testDatabase = database.GetPlaylists();
            foreach (string id in testDatabase)
            {
                if (id.Contains(temp2))
                {
                    await command.RespondAsync("Something went wrong, playlist was not removed from roster!");
                    return;
                }
            }
            
            await command.RespondAsync("Playlist removed from roster!");
        }
    }
}
