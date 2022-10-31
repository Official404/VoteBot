using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoteBot
{
    internal class SpotifyAPI
    {

        private readonly SpotifyClient _SpotifyClient;
        public SpotifyAPI()
        {
            _SpotifyClient = new SpotifyClient(Environment.GetEnvironmentVariable("SKEY"));
        }

        public bool CheckSpotifyPlaylist(string ListLink)
        {
            var playList = _SpotifyClient.Playlists.Get(ListLink);
            if (playList == null)
            {
                Console.WriteLine("Playlist not found!");
                return false;
            }
            return true;
        }
    }
}
