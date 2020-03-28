using Discord.WebSocket;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using MusicBAWT.Objects;

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
    }
}