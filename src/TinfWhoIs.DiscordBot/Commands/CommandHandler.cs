using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TinfWhoIs.DiscordBot.Commands
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _serviceProvider;
        private readonly TelemetryClient _telemetryClient;
        private readonly ILogger<CommandHandler> _logger;

        // Retrieve client and CommandService instance via ctor
        public CommandHandler(DiscordSocketClient client,
            CommandService commands,
            IServiceProvider serviceProvider,
            TelemetryClient telemetryClient,
            ILogger<CommandHandler> logger)
        {
            _commands = commands;
            _serviceProvider = serviceProvider;
            _telemetryClient = telemetryClient;
            _logger = logger;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived event into our command handler
            _client.MessageReceived += HandleCommandAsync;

            // Here we discover all of the command modules in the entry 
            // assembly and load them. Starting from Discord.NET 2.0, a
            // service provider is required to be passed into the
            // module registration method to inject the 
            // required dependencies.
            //
            // If you do not use Dependency Injection, pass null.
            // See Dependency Injection guide for more information.
            _commands.Log += CommandsOnLog;
            using var scope = _serviceProvider.CreateScope();
            await _commands.AddModulesAsync(typeof(CommandHandler).Assembly, scope.ServiceProvider);
        }

        private Task CommandsOnLog(LogMessage arg)
        {
            var level = arg.Severity switch
            {
                LogSeverity.Verbose => LogLevel.Trace,
                LogSeverity.Debug => LogLevel.Debug,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Critical => LogLevel.Critical,

                _ => LogLevel.None
            };

            _logger.Log(level, arg.Exception, arg.Message);
            return Task.CompletedTask;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            if (!(messageParam is SocketUserMessage message)) return;

            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasCharPrefix('.', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            var commandResult = _commands.Search(context, argPos);
            if (!commandResult.IsSuccess)
                return;

            using var operation = _telemetryClient.StartOperation<RequestTelemetry>(commandResult.Commands.First().Command.Name);
            operation.Telemetry.Context.User.AuthenticatedUserId = message.Author.Id.ToString();
            operation.Telemetry.Properties.Add("content", message.Content);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            using var scope = _serviceProvider.CreateScope();
            await _commands.ExecuteAsync(context, argPos, scope.ServiceProvider);
        }
    }
}
