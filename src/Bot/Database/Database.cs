using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Numerics;

namespace VoteBot
{
	internal class Database
	{
		private readonly string _connectionString;
        private readonly SpotifyAPI _spotifyAPI;

        public Database(String ConnectionString, SpotifyAPI spotifyAPI)
		{
			_connectionString = ConnectionString;
            _spotifyAPI = spotifyAPI;
		}

		public void TestConnection()
		{
			SqlConnection cnn;
			cnn = new SqlConnection(_connectionString);
			cnn.Open();
			Console.WriteLine("Connection Open!");
			cnn.Close();
		}

		public List<ulong> GetTrustedUsers()
		{
			SqlConnection cnn;
			cnn = new SqlConnection(_connectionString);
			cnn.Open();

			List<ulong> output = new List<ulong>();
            string sql = "SELECT * FROM TrustedUsers";
            SqlCommand command;
			SqlDataReader dataReader;

			command = new SqlCommand(sql, cnn);
			dataReader = command.ExecuteReader();

			while (dataReader.Read())
			{
				output.Add(ulong.Parse(dataReader.GetValue(0).ToString()));
			}

			cnn.Close();

			return output;
		}

		public void AddTrustedUser(ulong UserId) {
			SqlConnection cnn;
			cnn = new SqlConnection(_connectionString);
			cnn.Open();

			string sql = $"INSERT INTO TrustedUsers (DiscordID) VALUES ({UserId}); ALTER TABLE SongsToVoteOn ADD \"{UserId}\" INT";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();

            cnn.Close();
		}

        public void RemoveTrustedUser(ulong UserId)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(_connectionString);
            cnn.Open();

            string sql = $"DELETE FROM TrustedUsers WHERE DiscordID = {UserId}; ALTER TABLE SongsToVoteOn DROP COLUMN \"{UserId}\"";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();

            cnn.Close();
        }

        public List<String> GetPlaylists()
		{
            SqlConnection cnn;
            cnn = new SqlConnection(_connectionString);
            cnn.Open();

            List<string> output = new List<string>();
            string sql = "SELECT * FROM Playlists";
            SqlCommand command;
            SqlDataReader dataReader;

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                output.Add(dataReader.GetValue(0).ToString());
            }

            cnn.Close();

            return output;
        }

        public void AddPlaylist(string PlaylistLink)
        {

            if (_spotifyAPI.CheckSpotifyPlaylist(PlaylistLink) == false) {
                Console.WriteLine("Playlist not found or none excistant!");
                return;
            }

            SqlConnection cnn;
            cnn = new SqlConnection(_connectionString);
            cnn.Open();

            string sql = $"INSERT INTO Playlists (PlaylistLink) VALUES ('{PlaylistLink}')";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();

            cnn.Close();
        }

        public void RemovePlaylist(string PlaylistLink)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(_connectionString);
            cnn.Open();

            string sql = $"DELETE FROM Playlists WHERE PlaylistLink = '{PlaylistLink}'";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();

            cnn.Close();
        }
    }
}
