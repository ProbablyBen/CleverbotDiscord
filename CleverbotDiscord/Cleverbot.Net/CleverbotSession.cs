using System;
using System.Threading.Tasks;

namespace Cleverbot.Net
{
    public class CleverbotSession
    {
        private readonly string _apiKey;

        /// <summary>
        ///     The state of the cleverbot conversation.
        ///     It is an encoded string of the last 50 interactions
        ///     between the user, and the bot.
        /// </summary>
        private string _conversationState;

        /// <summary>
        ///     Creates a Cleverbot instance.
        /// </summary>
        /// <param name="apikey">Your api key obtained from https://cleverbot.com/api/ </param>
        /// <param name="sendTestMessage">Send a test message to be sure you're connected</param>
        public CleverbotSession(string apikey, bool sendTestMessage = true)
        {
            if (string.IsNullOrWhiteSpace(apikey)) throw new Exception("You can't connect without a API key.");

            _apiKey = apikey;

            if (sendTestMessage)
            {
                var test = GetResponseAsync("test").GetAwaiter().GetResult();
            }
        }

        public DateTime LastMessageSent { get; private set; } = DateTime.MinValue;

        /// <summary>
        ///     Send a message to cleverbot asynchronously and get a response.
        /// </summary>
        /// <param name="message">your message sent to cleverbot</param>
        /// <returns>response from the cleverbot.com api</returns>
        public async Task<CleverbotResponse> GetResponseAsync(string message)
        {
            LastMessageSent = DateTime.Now;
            var resp = await CleverbotResponse.CreateAsync(message, _conversationState, _apiKey);
            _conversationState = resp.ConversationState;
            return resp;
        }
    }
}