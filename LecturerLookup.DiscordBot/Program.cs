using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FluentValidation;
using LecturerLookup.Core.Database;
using LecturerLookup.DiscordBot.Commands;
using LecturerLookup.DiscordBot.Workers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LecturerLookup.DiscordBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddUserSecrets<Program>())
                .ConfigureServices((hostContext, services) =>
                {
                    // Discord
                    services.AddSingleton<DiscordSocketClient>(sp => CreateDiscordSocketClient(hostContext));
                    services.AddSingleton<IDiscordClient, DiscordSocketClient>(sp => sp.GetRequiredService<DiscordSocketClient>());

                    services.AddHostedService<Worker>();
                    services.AddHostedService<CalculateScoreWorker>();
                    services.AddSingleton<CommandServiceConfig>();
                    services.AddSingleton<CommandService>();
                    services.AddSingleton<CommandHandler>();

                    // DB
                    services.AddDistributedRedisCache(options =>
                    {
                        options.Configuration = hostContext.Configuration.GetConnectionString("Redis");
                        options.InstanceName = "WhoIs_";
                    });

                    services.AddDbContext<WhoIsDbContext>(x =>
                    {
                        x.EnableSensitiveDataLogging();
                        x.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultDbContext"));
                    });

                    // Misc
                    services.AddMediatR(typeof(Program));
                    services.AddAutoMapper(typeof(Program));
                    services.AddValidatorsFromAssemblyContaining(typeof(Program));
                    services.AddApplicationInsightsTelemetryWorkerService();
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
