using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using TinfWhoIs.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace TinfWhoIs.DiscordBot.Commands
{
    [Group("courses")]
    public class CourseCommands : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public const int ItemsPerPage = 10;

        public CourseCommands(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Command]
        public async Task Search(string term)
        {
            var results = await _dbContext.Courses.AsQueryable()
                .Where(x => EF.Functions.ILike(x.Name, $"%{term}%")
                            || EF.Functions.ILike(x.Id, $"%{term}%"))
                .Take(10)
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (!results.Any())
            {
                await ReplyAsync("Keine Ergebnisse gefunden.");
                return;
            }

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle($"Kurs-Suchergebnisse zu \"{term}\"")
                .WithFields(results
                    .Select(x => new EmbedFieldBuilder()
                        .WithName(x.Name)
                        .WithValue($"`{x.Id}`")))
                .Build());
        }
    }
}