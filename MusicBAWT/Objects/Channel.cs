using Discord.Audio;
using Discord.WebSocket;

namespace MusicBAWT.Objects
{
    public class Channel
    {
        public IAudioClient AudioClient;
        public SocketVoiceChannel VoiceChannel;

        public Channel(IAudioClient audioClient, SocketVoiceChannel voiceChannel)
        {
            this.AudioClient = audioClient;
            this.VoiceChannel = voiceChannel;
        }
        
    }
}