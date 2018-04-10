using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CleverbotDiscord.Services
{
    public class LoggingService
    {
        private readonly DiscordSocketClient _client;

        public LoggingService(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task StartAsync()
        {
            _client.Log += OnLog;
        }

        private Task OnLog(LogMessage msg)
        {
            return Console.Out.WriteLineAsync(msg.ToString());
        }
    }
}