using Discord;
using MediatR;

namespace LecturerLookup.DiscordBot
{
    public record ReactionAddedEvent : INotification
    {
        public ulong MessageId { get; init; }

        public ulong ChannelId { get; init; }

        public IReaction Reaction { get; init; }

        public ReactionAddedEvent(ulong id, ulong channelId, IReaction reaction) =>
            (MessageId, ChannelId, Reaction) = (id, channelId, reaction);
    }
}