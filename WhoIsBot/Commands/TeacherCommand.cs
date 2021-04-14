using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace WhoIsBot.Commands
{
    [Group("dozent")]
    [RequireContext(ContextType.DM)]
    public class TeacherCommand : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public TeacherCommand(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Command]
        public async Task WhoIs(string term)
        {
            var result = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Name.Contains(term));
            if (result is null)
                return;

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(result.Name)
                .WithThumbnailUrl(result.AvatarUrl)
                .WithDescription(result.Office)
                .WithFields(
                    new EmbedFieldBuilder()
                        .WithName("Email")
                        .WithValue(result.Email)
                        .WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("Telefon")
                        .WithValue(result.Telephone ?? "")
                        .WithIsInline(true))               
                .Build());
        }
    }
}