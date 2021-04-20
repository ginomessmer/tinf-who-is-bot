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
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Name.Contains(term));

            if (result is null)
                return;

            // Courses
            var courses = result.Courses;
            var coursesValue = courses.Any() ? string.Join(", ", courses.Select(x => x.Name)) : "-";

            // Tags
            var tags = result.DetermineTopTags();
            var tagsValue = tags.Any() ? string.Join(", ", tags.Select(x => x.Tag.Key)) : "-";

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(result.Name)
                .WithThumbnailUrl(result.AvatarUrl)
                .WithDescription(result.Office)
                .WithFields(
                    new EmbedFieldBuilder()
                        .WithName("Vorlesungen")
                        .WithValue(coursesValue),
                    new EmbedFieldBuilder()
                        .WithName("Attribute")
                        .WithValue(tagsValue),
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

        [Command("commend")]
        public async Task Tag(int teacherId, params int[] tagIds)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            
            if (teacher is null)
                return;

            // Post tags
            var tags = new List<Tag>();
            foreach (var tagId in tagIds)
            {
                tags.Add(await _dbContext.Tags.FindAsync(tagId));
            }
            
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


            // Post instructions
            await ReplyAsync("", embed: new EmbedBuilder()
                .WithTitle(Resources.CommendTitle)
                .WithDescription(Resources.CommendInstructions)
                .Build());

            foreach (var teacherTag in teacher.Tags)
            {
                var message = await ReplyAsync($"{teacherTag.Tag.Key}?");
                await message.AddReactionsAsync(new []
                {
                    new Emoji("⬆"),
                    new Emoji("⬇")
                });

                if (await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_dbContext.TeacherTagVotes, x =>
                    x.TeacherTagId == teacherTag.Id && x.VoterId == Context.User.Id) is TeacherTagVote vote)
                {
                    // Update
                    vote.MessageId = message.Id;
                }
                else
                {
                    teacherTag.Votes.Add(new TeacherTagVote(Context.User.Id)
                    {
                        MessageId = message.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}