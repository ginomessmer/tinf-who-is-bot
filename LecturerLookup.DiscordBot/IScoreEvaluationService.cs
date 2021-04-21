namespace LecturerLookup.DiscordBot.Commands
{
    public interface IScoreEvaluationService
    {
        string GetLabel(double score);
    }
}