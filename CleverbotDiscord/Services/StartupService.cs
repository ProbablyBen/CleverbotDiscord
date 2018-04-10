using System;
using System.Configuration;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CleverbotDiscord.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _client;

        public StartupService(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task StartAsync()
        {
            var token = ConfigurationManager.AppSettings["DiscordToken"];

            if (string.IsNullOrEmpty(token)) throw new Exception("The discord token is empty in `App.config`.");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }
    }
}