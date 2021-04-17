using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Caching.Distributed;
using WhoIsBot.Commands;

namespace WhoIsBot
{
    public class Worker : BackgroundService
    {
        private readonly CommandHandler _commandHandler;
        private readonly ILogger<Worker> _logger;

        public Worker(CommandHandler commandHandler,
            DiscordSocketClient client,
            IDistributedCache cache,
            ILogger<Worker> logger)
        {
            _commandHandler = commandHandler;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _commandHandler.InstallCommandsAsync();
        }
    }
}
