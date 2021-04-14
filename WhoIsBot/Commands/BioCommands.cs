using System.Linq;
using System.Threading.Tasks;
using Azure.AI.TextAnalytics;
using Discord;
using Discord.Commands;

namespace WhoIsBot.Commands
{
    [Group("bio")]
    [RequireContext(ContextType.DM)]
    public class BioCommands : ModuleBase<SocketCommandContext>
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;
        private readonly WhoIsDbContext _dbContext;

        public BioCommands(TextAnalyticsClient textAnalyticsClient, WhoIsDbContext dbContext)
        {
            _textAnalyticsClient = textAnalyticsClient;
            _dbContext = dbContext;
        }

        [Command("about")]
        public async Task SetupProfile([Remainder] string bioText)
        {
            using var typing = Context.Channel.EnterTypingState();
            var response = await _textAnalyticsClient.RecognizeEntitiesAsync(bioText);
            var result = response.Value;

            var interests = result.Select(x => new ProfileInterest()
            {
                ProfileId = Context.User.Id,
                Username = Context.User.Username,
                Category = x.Category.ToString(),
                ConfidenceScore = x.ConfidenceScore,
                SubCategory = x.SubCategory,
                Text = x.Text
            }).ToList();

            var profileEmbed = new EmbedBuilder()
                .WithFields(
                    new EmbedFieldBuilder()
                        .WithName("Name")
                        .WithValue(result
                            .OrderByDescending(x => x.ConfidenceScore)
                            .FirstOrDefault(x => x.Category == EntityCategory.Person).Text ?? "Idk..."),
                    new EmbedFieldBuilder()
                        .WithName("Skills")
                        .WithValue(string.Join(", ", result.Where(x => x.Category == EntityCategory.Skill)
                            .Select(x => x.Text))),
                    new EmbedFieldBuilder()
                        .WithName("Type")
                        .WithValue(string.Join(", ", result.Where(x => x.Category == EntityCategory.PersonType)
                            .Select(x => x.Text))));

            await ReplyAsync("Great success. Here's what I found out about you:", embed: profileEmbed.Build());

            var profile = new Profile(Context.User.Id, interests.ToArray())
            {
                About = bioText,
                Name = Context.User.Username
            };

            var existingProfile = await _dbContext.Profiles.FindAsync(profile.Id);

            if (existingProfile is null)
                await _dbContext.Profiles.AddAsync(profile);
            else
                _dbContext.Profiles.Update(profile);

            await _dbContext.SaveChangesAsync();
        }
    }
}