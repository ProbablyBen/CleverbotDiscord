using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cleverbot.Net
{
    public class CleverbotResponse
    {
        [JsonProperty("interaction_count")] public int InteractionCount { get; set; }

        [JsonProperty("clever_accuracy")] public string Accuracy { get; set; }

        [JsonProperty("cs")] public string ConversationState { get; set; }

        [JsonProperty("clever_output")] public string Message { get; set; }

        /// <summary>
        ///     The user's latest message
        /// </summary>
        [JsonProperty("input")]
        public string Input { get; set; }

        internal static async Task<CleverbotResponse> CreateAsync(string message, string conversationHistory, string apiKey)
        {
            var conversationLine =
                string.IsNullOrWhiteSpace(conversationHistory) ? "" : $"&cs={conversationHistory}";

            var url =
                $"https://www.cleverbot.com/getreply?key={apiKey}&wrapper=cleverbot.net&input={message}{conversationLine}&callback=ProcessReply";

            var result = await HttpUtil.Client.GetStringAsync(url).ConfigureAwait(false);

            result = result.Replace("ProcessReply(", "").Replace(");", ""); // Hack

            return JsonConvert.DeserializeObject<CleverbotResponse>(result);
        }
    }
}