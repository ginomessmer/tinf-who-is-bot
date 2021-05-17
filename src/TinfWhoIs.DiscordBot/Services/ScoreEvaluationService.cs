using TinfWhoIs.Core.Abstractions;

namespace TinfWhoIs.DiscordBot.Services
{
    public class ScoreEvaluationService : IScoreEvaluationService
    {
        public string GetLabel(double score) => score switch
        {
            var x when x >= 0.9 => $"{Emojis.ExplodingHead} Bombastisch",
            var x when x >= 0.83 => $"{Emojis.StarStruck} Sehr gut",
            var x when x >= 0.66 => $"{Emojis.ThumbsUp} Gut",
            var x when x >= 0.33 => $"{Emojis.ThumbsDown} Schlecht",
            var x when x >= 0.16 => $"{Emojis.TiredFace} Sehr schlect",
            var x when x <= 0.1 => $"{Emojis.Angry} Unterirdisch",
        };
    }
}