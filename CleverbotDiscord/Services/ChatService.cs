using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading.Tasks;
using Cleverbot.Net;
using Discord;
using Discord.Commands;

namespace CleverbotDiscord.Services
{
    public class ChatService
    {
        private readonly string _apiKey;
        private readonly TimeSpan _rateLimitSeconds = TimeSpan.FromSeconds(3);

        private readonly ConcurrentDictionary<IUser, CleverbotSession> _sessions;

        public ChatService()
        {
            _sessions = new ConcurrentDictionary<IUser, CleverbotSession>();
            _apiKey = ConfigurationManager.AppSettings["CleverbotKey"];
            if (string.IsNullOrEmpty(_apiKey)) throw new Exception("The cleverbot api key is empty in `App.config`.");
        }

        public async Task ReplyAsync(SocketCommandContext context, string message)
        {
            var session = GetSession(context.User);

            // Ignore people spamming the bot
            if (DateTime.Now - session.LastMessageSent > _rateLimitSeconds)
            {
                using (context.Channel.EnterTypingState())
                {
                    try
                    {
                        var response = await session.GetResponseAsync(message);
                        await context.Channel.SendMessageAsync($"{context.User.Mention} {response.Message}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private CleverbotSession GetSession(IUser user)
        {
            while (true)
            {
                if (_sessions.TryGetValue(user, out var session)) return session;

                var newSession = new CleverbotSession(_apiKey, false);

                if (_sessions.TryAdd(user, newSession)) return newSession;
            }
        }
    }
}