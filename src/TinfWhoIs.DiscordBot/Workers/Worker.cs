using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using TinfWhoIs.DiscordBot.Commands;
using TinfWhoIs.DiscordBot.Events;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TinfWhoIs.DiscordBot.Workers
{
    public class Worker : BackgroundService
    {
        private readonly CommandHandler _commandHandler;
        private readonly DiscordSocketClient _client;
        private readonly IMediator _mediator;
        private readonly ILogger<Worker> _logger;

        public Worker(CommandHandler commandHandler,
            DiscordSocketClient client,
            IMediator mediator,
            ILogger<Worker> logger)
        {
            _commandHandler = commandHandler;
            _client = client;
            _mediator = mediator;
            _logger = logger;

            client.ReactionAdded += ClientOnReactionAdded;
        }

        private Task ClientOnReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction) =>
            reaction.UserId == _client.CurrentUser.Id
                ? Task.CompletedTask
                : _mediator.Publish(new ReactionAddedEvent(message.Id, channel.Id, reaction));

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _commandHandler.InstallCommandsAsync();
        }
    }
}
