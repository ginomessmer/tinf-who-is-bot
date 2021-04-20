using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using LecturerLookup.Core.Database;

namespace WhoIsBot.Commands
{
    [Group("tags")]
    public class TagCommands : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public TagCommands(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Command]
        public async Task Tags()
        {
            var tags = await _dbContext.Tags.ToListAsync();
            var list = tags.Select(x => $"`{x.Id}`: {x.Key}");
            var textList = string.Join('\n', list);

            await ReplyAsync(textList);
        }
    }
}