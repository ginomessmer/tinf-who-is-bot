using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using WhoIsBot.Commands;

namespace WhoIsBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Discord
                    services.AddSingleton<DiscordSocketClient>(sp => CreateDiscordSocketClient(hostContext));
                    services.AddSingleton<IDiscordClient, DiscordSocketClient>(sp => sp.GetRequiredService<DiscordSocketClient>());

                    services.AddSingleton(new TextAnalyticsClient(new Uri("https://tinf-network-whois.cognitiveservices.azure.com/"),
                        new AzureKeyCredential("e1d412123ade4305991f0d5a16d71c30")));

                    services.AddHostedService<Worker>();
                    services.AddSingleton<CommandServiceConfig>();
                    services.AddSingleton<CommandService>();
                    services.AddSingleton<CommandHandler>();

                    // DB
                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = hostContext.Configuration.GetConnectionString("Redis");
                        options.InstanceName = "WhoIs_";
                    });
                    services.AddDbContext<WhoIsDbContext>(x => x.UseSqlite("Data source=whois.sqlite"));
                });

        private static DiscordSocketClient CreateDiscordSocketClient(HostBuilderContext hostContext)
        {
            var readyEvent = new AutoResetEvent(false);

            var token = hostContext.Configuration.GetConnectionString("DiscordBotToken");

            var client = new DiscordSocketClient();
            client.Ready += () => Task.FromResult(readyEvent.Set());

            Task.WaitAll(client.LoginAsync(TokenType.Bot, token), client.StartAsync());

            readyEvent.WaitOne(TimeSpan.FromSeconds(30));
            return client;
        }
    }
}
