using System.Linq;
using LecturerLookup.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace LecturerLookup.DiscordBot
{
    public class CommendEventHandler : INotificationHandler<ReactionAddedEvent>
    {
        private readonly WhoIsDbContext _dbContext;
        private readonly DiscordSocketClient _client;

        public CommendEventHandler(WhoIsDbContext dbContext, DiscordSocketClient client)
        {
            _dbContext = dbContext;
            _client = client;
        }

        public async Task Handle(ReactionAddedEvent notification, CancellationToken cancellationToken)
        {
            var result = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_dbContext.TeacherTagVotes, x =>
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
        }
    }
}