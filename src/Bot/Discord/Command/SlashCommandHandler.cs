using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteBot
{
    internal class SlashCommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly Database _botDatabase;

        public SlashCommandHandler(DiscordSocketClient client, CommandService command, Database botDatabase)
        {
            _client = client;
            _command = command;
            _botDatabase = botDatabase;

            _client.SlashCommandExecuted += SlashCommandCallback;
        }

        public async Task initCommands()
        {
            if (ulong.TryParse(Environment.GetEnvironmentVariable("DGUILD"), out ulong guildId))
            {
                var guild = _client.GetGuild(guildId);
                if (guild != null)
                {
                    //TODO: Add all commands here
                    #region Commands
                    var pingCommand = new SlashCommandBuilder();
                    pingCommand.WithName("ping");
                    pingCommand.WithDescription("Pings the bot!");

                    var addTrustedUserCommand = new SlashCommandBuilder();
                    addTrustedUserCommand.WithName("addtrusteduser");
                    addTrustedUserCommand.WithDescription("Adds a trusted user!");
                    addTrustedUserCommand.AddOption("user", ApplicationCommandOptionType.User, "The user to add as trusted", true);

                    var removeTrustedUserCommand = new SlashCommandBuilder();
                    removeTrustedUserCommand.WithName("removetrusteduser");
                    removeTrustedUserCommand.WithDescription("Removes a trusted user!");
                    removeTrustedUserCommand.AddOption("user", ApplicationCommandOptionType.User, "The user to remove as trusted", true);

                    var addPlaylistCommand = new SlashCommandBuilder();
                    addPlaylistCommand.WithName("addplaylist");
                    addPlaylistCommand.WithDescription("Adds a playlist where votes end!");
                    addPlaylistCommand.AddOption("playlist", ApplicationCommandOptionType.String, "The playlist to add", true);

                    var removePlaylistCommand = new SlashCommandBuilder();
                    removePlaylistCommand.WithName("removeplaylist");
                    removePlaylistCommand.WithDescription("Removes a playlist where votes end!");
                    removePlaylistCommand.AddOption("playlist", ApplicationCommandOptionType.String, "The playlist to remove", true);

                    var addVoteSong = new SlashCommandBuilder();
                    addVoteSong.WithName("addvotesong");
                    addVoteSong.WithDescription("Adds a song to the vote list!");
                    addVoteSong.AddOption("song", ApplicationCommandOptionType.String, "Spotify Link to song", true);

                    var removeVoteSong = new SlashCommandBuilder();
                    removeVoteSong.WithName("removevotesong");
                    removeVoteSong.WithDescription("Removes a song from the vote list!");
                    removeVoteSong.AddOption("song", ApplicationCommandOptionType.String, "Spotify Link to song", true);
                    #endregion
                    // End of commands

                    try
                    {
                        //TODO: Add all commands here
                        #region Commands
                        await guild.CreateApplicationCommandAsync(pingCommand.Build());
                        await guild.CreateApplicationCommandAsync(addTrustedUserCommand.Build());
                        await guild.CreateApplicationCommandAsync(removeTrustedUserCommand.Build());
                        await guild.CreateApplicationCommandAsync(addPlaylistCommand.Build());
                        await guild.CreateApplicationCommandAsync(removePlaylistCommand.Build());
                        await guild.CreateApplicationCommandAsync(addVoteSong.Build());
                        await guild.CreateApplicationCommandAsync(removeVoteSong.Build());
                        #endregion
                        // End of commands
                    }
                    catch (ApplicationCommandException exception)
                    {
                        var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                        Console.WriteLine(json);
                    }
                }
            }
        }

        private async Task SlashCommandCallback(SocketSlashCommand command)
        {
            // If bot the dont respond
            if (command.User.IsBot) return;

            //TODO: Add all commands here
            #region Commands
            if (command.Data.Name == "ping") {
                await PingCommand.Respons(command);
            }
            #endregion
            // End of commands

            // TrustedUsers Commands
            if (_botDatabase.GetTrustedUsers().Contains(command.User.Id))
            {
                #region Commands
                //TODO: Add all commands here
                if (command.Data.Name == "addtrusteduser")
                {
                    await AddTrustedUserCommand.Respons(command, _botDatabase);
                }
                if (command.Data.Name == "removetrusteduser")
                {
                    await RemoveTrustedUserCommand.Respons(command, _botDatabase);
                }
                if (command.Data.Name == "addplaylist")
                {
                    await AddPlaylistCommand.Respons(command, _botDatabase);
                }
                if (command.Data.Name == "removeplaylist")
                {
                    await RemovePlaylistCommand.Respons(command, _botDatabase);
                }
                if (command.Data.Name == "addvotesong")
                {
                    await AddVoteSong.Respons(command, _botDatabase);
                }
                if (command.Data.Name == "removevotesong")
                {
                    await RemoveVoteSong.Respons(command, _botDatabase);
                }
                // End of commands
                #endregion
                return;
            } else
            {
                await command.RespondAsync("You are not Nomi, you can't run this command!");
                return;
            }
        }
    }
}
