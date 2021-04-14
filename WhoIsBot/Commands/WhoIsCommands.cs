using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;

namespace WhoIsBot.Commands
{
    [Group("whois")]
    [RequireContext(ContextType.DM)]
    public class WhoIsCommands : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public WhoIsCommands(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Command]
        public Task WhoIs() => WhoIs(Context.User);

        [Command]
        public async Task WhoIs(IUser user)
        {
            using var typing = Context.Channel.EnterTypingState();
            var profile = await _dbContext.Profiles.Include(x => x.Interests).FirstOrDefaultAsync(x => x.Id == user.Id);

            await ReplyAsync(embed: new EmbedBuilder()
                .WithAuthor(profile.Name)
                //.WithTitle("Profile")
                .WithThumbnailUrl(user.GetAvatarUrl())
                .WithDescription(profile.About)
                .WithFields(new EmbedFieldBuilder()
                    .WithName("Skills")
                    .WithValue(string.Join(", ", profile.Interests.Select(x => x.Text))))
                .Build());
        }
    }
}