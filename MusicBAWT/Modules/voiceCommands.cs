using System;
using Discord.WebSocket;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using MusicBAWT.Objects;

namespace MusicBAWT.Modules
{
    public class voiceCommands : ModuleBase
    {
        /// <summary>
        /// Join the voice channel of that the user is currently in
        /// </summary>
        /// <returns></returns>
        [Command("Join", RunMode = RunMode.Async)]
        public async Task JoinChannelCommand()
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;
            
            // Check if the user is actually in a voice channel
            if (user.VoiceChannel == null)
            {
                await ReplyAsync("Sorry " + user.Username + ", but I can't join your voice channel.");
                return;
            }
        
            var voiceChannel = user.VoiceChannel;
            // Check if the bot isnt already connected.
            for (int i = 0; i < Program.ConnectedChannels.Count; i++)
            {
                if (Program.ConnectedChannels[i].VoiceChannel.Id == user.VoiceChannel.Id)
                {
                    await Program.ConnectedChannels[i].AudioClient.StopAsync();
                    Program.ConnectedChannels.RemoveAt(i);
                }
            }
            // (Re)connect to the voice channel
            var connectedChannel = await voiceChannel.ConnectAsync();
            Program.ConnectedChannels.Add(new Channel(connectedChannel, voiceChannel));
            await ReplyAsync("Joined channel " + voiceChannel.Name);
        }
        [Command("Leave", RunMode = RunMode.Async)]
        public async Task LeaveChannelCommand()
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;
            if (user.VoiceChannel == null)
            {
                await ReplyAsync("Sorry " + user.Username + ", but I can't leave your voice channel.");
                return;
            }

            // Leave the voice channel
            for (int i = 0; i < Program.ConnectedChannels.Count; i++)
            {
                if (Program.ConnectedChannels[i].VoiceChannel.Id == user.VoiceChannel.Id)
                {
                    await Program.ConnectedChannels[i].AudioClient.StopAsync();
                    Program.ConnectedChannels.RemoveAt(i);
                }
            }
        }
    }
}