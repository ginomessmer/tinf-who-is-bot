using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using WhoIsBot.Database;
using WhoIsBot.Models;

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
            var tags = await AsyncEnumerable.ToListAsync(_dbContext.Tags);
            var list = tags.Select(x => $"`{x.Id}`: {x.Key}");
            var textList = string.Join('\n', list);

            await ReplyAsync(textList);
        }
    }

    [RequireContext(ContextType.DM)]
    public class TeacherCommand : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public TeacherCommand(WhoIsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Command("whois")]
        public async Task WhoIs(string term)
        {
            var result = await AsyncEnumerable.FirstOrDefaultAsync(_dbContext.Teachers, x => x.Name.Contains(term));
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
                .WithFooter($"ID: {result.Id}")
                .Build());
        }

        [Command("commend")]
        public async Task Tag(int teacherId, params int[] tagIds)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            
            if (teacher is null)
                return;

            var tags = await Task.WhenAll(tagIds.Select(x => _dbContext.Tags.FindAsync(x).AsTask()));
            var teacherTags = tags.Select(x => new TeacherTag(x, Context.User.Id));

            teacher.Tags.AddRange(teacherTags);
            await _dbContext.SaveChangesAsync();
        }

        [Command("eval")]
        public async Task Evaluate(int teacherId)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Votes)
                .FirstOrDefaultAsync(x => x.Id == teacherId);

            foreach (var teacherTag in teacher.Tags)
            {
                var message = await ReplyAsync($"{teacherTag.Tag.Key}?");
                await message.AddReactionsAsync(new []
                {
                    new Emoji("✅"),
                    new Emoji("❌")
                });

                teacherTag.Votes.Add(new TeacherTagVote(Context.User.Id));
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}