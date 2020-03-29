using System;
using Discord.WebSocket;
using Discord.Commands;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MusicBAWT.Services;
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
        
        [Command("Weather")]
        public async Task WeatherCommand(string location)
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            WeatherHandler weatherHandler = new WeatherHandler();
            Weather weather = await weatherHandler.getWeather(location);

            sb.AppendLine("In " + weather.Location + " the weather is " + weather.Description.ToLower());
            sb.AppendLine("Temperature: " + Math.Round(weather.Temperature, 0).ToString() + "°C");
            sb.AppendLine("Humidity: " + Math.Round(weather.Humidity, 0).ToString() + "%");
            // send simple string reply
            await ReplyAsync(sb.ToString());
        }
    }
}