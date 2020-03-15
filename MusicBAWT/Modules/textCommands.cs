using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace MusicBAWT.Modules
{
    public class textCommands : ModuleBase
    {
        [Command("Hello")]
        public async Task HelloCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;
            
            // build out the reply
            sb.AppendLine("Hey " + user.Username + "!");

            // send simple string reply
            await ReplyAsync(sb.ToString());
        }
        
        [Command("Join")]
        public async Task JoinChannelCommand()
        {
            SocketGuildUser user = (SocketGuildUser)Context.User;
            if (user.VoiceChannel == null)
            {
                await ReplyAsync("Sorry " + user.Username + ", but I can't join your voice channel.");
                return;
            }
            _service.
            await user.SendMessageAsync("test PM");
        }
    }
}