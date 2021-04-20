using Discord;
using Discord.Commands;
using LecturerLookup.Core.Database;
using LecturerLookup.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LecturerLookup.DiscordBot.Properties;

namespace LecturerLookup.DiscordBot.Commands
{

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
            var result = await _dbContext.Teachers
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(x => x.Name.Contains(term));

            if (result is null)
                return;

            // Courses
            var courses = result.Courses;
            var coursesValue = courses.Any() ? string.Join(", ", courses.Select(x => x.Name)) : "-";
            

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(result.Name)
                .WithThumbnailUrl(result.AvatarUrl)
                .WithDescription(result.Office)
                .WithFields(
                    new EmbedFieldBuilder()
                        .WithName("Vorlesungen")
                        .WithValue(coursesValue),
                    new EmbedFieldBuilder()
                        .WithName("Email")
                        .WithValue(result.Email)
                        .WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("Telefon")
                        .WithValue(result.Telephone ?? "")
                        .WithIsInline(true)) 
                .WithFooter($"#{result.Id}")
                .Build());
        }

        [Command("rate")]
        public async Task Rate(int id)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == id);

            teacher.Ratings.Add(new TeacherRate
            {
                VotedBy = Context.User.Id
            });

            var message = await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle($"{teacher.Name} bewerten")
                .WithDescription("TODO Beschreibung")
                .Build());

            await message.AddReactionsAsync(new IEmote[]
            {
                new Emoji(Emojis.ExplodingHead),
                new Emoji(Emojis.StarStruck),
                new Emoji(Emojis.ThumbsUp),
                new Emoji(Emojis.PersonShrugging),
                new Emoji(Emojis.ThumbsDown),
                new Emoji(Emojis.TiredFace),
                new Emoji(Emojis.Angry),
            });
        }
    }
}