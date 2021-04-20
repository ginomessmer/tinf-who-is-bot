using LecturerLookup.Core.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LecturerLookup.DiscordBot
{
    public class CommendEventHandler : INotificationHandler<ReactionAddedEvent>
    {
        private readonly WhoIsDbContext _dbContext;

        public CommendEventHandler(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ReactionAddedEvent notification, CancellationToken cancellationToken)
        {
            var result = await _dbContext.TeacherTagVotes.SingleOrDefaultAsync(x =>
                x.MessageId == notification.MessageId, cancellationToken);

            if (result is null)
                return;

            var score = notification.Reaction.Emote.Name switch
            {
                "⬆" => 1,
                "⬇" => -1,
                _ => 0
            };

            result.Score = score;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}