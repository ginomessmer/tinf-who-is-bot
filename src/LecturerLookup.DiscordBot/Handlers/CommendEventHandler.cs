using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using LecturerLookup.Core.Abstractions;
using LecturerLookup.Core.Database;
using LecturerLookup.DiscordBot.Events;
using MediatR;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace LecturerLookup.DiscordBot.Handlers
{
    public class CommendEventHandler : INotificationHandler<ReactionAddedEvent>
    {
        private readonly WhoIsDbContext _dbContext;
        private readonly DiscordSocketClient _client;
        private readonly TelemetryClient _telemetryClient;

        public CommendEventHandler(WhoIsDbContext dbContext, DiscordSocketClient client, TelemetryClient telemetryClient)
        {
            _dbContext = dbContext;
            _client = client;
            _telemetryClient = telemetryClient;
        }

        public async Task Handle(ReactionAddedEvent notification, CancellationToken cancellationToken)
        {
            using var operation = _telemetryClient.StartOperation<RequestTelemetry>(nameof(CommendEventHandler));

            var result = await _dbContext.TeacherTagVotes.SingleOrDefaultAsync(x =>
                x.MessageId == notification.MessageId, cancellationToken);

            if (result is null)
                return;

            var score = notification.Reaction.Emote.Name switch
            {
                Emojis.ArrowUp => 1,
                Emojis.ArrowDown => -1,
                _ => 0
            };

            result.Score = score;
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Acknowledge
            var channel = await _client.GetDMChannelAsync(notification.ChannelId);
            var message = await channel.GetMessageAsync(notification.MessageId);
            await message.AddReactionAsync(new Emoji(Emojis.WhiteCheckMark));

            _telemetryClient.TrackEvent("Teacher commended", new Dictionary<string, string>
            {
                [nameof(score)] = score.ToString()
            });

            operation.Telemetry.Context.User.AuthenticatedUserId = channel.Recipient.Id.ToString();
        }
    }
}