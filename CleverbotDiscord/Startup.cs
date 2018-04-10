using System.Threading.Tasks;
using CleverbotDiscord.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CleverbotDiscord
{
    public class Startup
    {
        public Startup(string[] args)
        {
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            // Start logging and command handler service
            await provider.GetRequiredService<LoggingService>().StartAsync();
            await provider.GetRequiredService<CommandHandlerService>().StartAsync();

            // Run startup service
            await provider.GetRequiredService<StartupService>().StartAsync();

            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000 // Cache 1,000 messages per channel
                }))
                .AddSingleton<StartupService>()
                .AddSingleton<LoggingService>()
                .AddSingleton<ChatService>()
                .AddSingleton<CommandHandlerService>();
        }
    }
}