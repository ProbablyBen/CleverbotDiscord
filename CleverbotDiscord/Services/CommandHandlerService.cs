using System;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CleverbotDiscord.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _provider;

        public CommandHandlerService(DiscordSocketClient client, IServiceProvider provider)
        {
            _client = client;
            _provider = provider;
        }

        public async Task StartAsync()
        {
            _client.MessageReceived += OnMessageReceievedAsync;
        }

        private async Task OnMessageReceievedAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return; // Make sure message is from a user or a bot

            if (msg.Author.Id == _client.CurrentUser.Id) return;

            var context = new SocketCommandContext(_client, msg);

            var argPos = 0;
            if (msg.HasMentionPrefix(_client.CurrentUser, ref argPos) || context.IsPrivate)
            {
                // Filter message by removing the @user mention
                // Extra space at the end because a mention looks like '@user messsage'... not '@usermessage'
                var mention = context.Client.CurrentUser.Mention.Replace("!", "") + " ";
                var message = context.Message.Content.Replace(mention, "");

                // I hope this isn't terrible practice
                // It works though and makes sure the gateway can process further messages
                new Thread(async () =>
                {
                    await _provider.GetRequiredService<ChatService>().ReplyAsync(context, message);
                }).Start();
            }
        }
    }
}