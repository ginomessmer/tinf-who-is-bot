using Discord;
using Discord.Commands;
using LecturerLookup.Core.Database;
using LecturerLookup.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LecturerLookup.DiscordBot.Commands.Arguments;
using LecturerLookup.DiscordBot.Properties;
using LecturerLookup.DiscordBot.Services;

namespace LecturerLookup.DiscordBot.Commands
{
    /// <summary>
    /// Teacher management commands.
    /// </summary>
    public partial class TeacherCommands
    {
        [Command("add")]
        public async Task Add(AddTeacherArguments arguments)
        {
            
        }
    }

    /// <summary>
    /// Teacher basic commands
    /// </summary>
    [RequireContext(ContextType.DM)]
    public partial class TeacherCommands : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;

        public TeacherCommands(WhoIsDbContext dbContext)
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
                .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, $"%{term}%"));

            if (result is null)
                return;

            // Courses
            var courses = result.Courses;
            var coursesValue = courses.Any() ? string.Join(", ", courses.Select(x => x.Name)) : "-";

            // Tags
            var tags = result.Tags;

            var evaluationService = new ScoreEvaluationService();

            var tagFields = tags.Select(x => new EmbedFieldBuilder()
                .WithName(x.Tag.Key)
                .WithValue(evaluationService.GetLabel(x.Evaluation.CalculatedScore))
                .WithIsInline(false));

            var fields = new List<EmbedFieldBuilder>();
            fields.Add(new EmbedFieldBuilder()
                    .WithName("Vorlesungen")
                    .WithValue(coursesValue));
            fields.AddRange(tagFields);
            fields.Add(new EmbedFieldBuilder()
                    .WithName("Kontakt")
                    .WithValue(result.FormatContactDetails()));

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(result.Name)
                .WithThumbnailUrl(result.AvatarUrl)
                .WithDescription(result.Office)
                .WithFields(fields) 
                .WithFooter($"#{result.Id}")
                .Build());
        }

        [Command("rate")]
        public async Task Rate(int teacherId)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Votes)
                .FirstOrDefaultAsync(x => x.Id == teacherId);

            // Append tags
            var tags = _dbContext.Tags.ToList();
            var missingTags = tags.Except(teacher.Tags.Select(x => x.Tag));

            teacher.Tags.AddRange(missingTags.Select(x => new TeacherTag(x)));

            // Post instructions
            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(Resources.CommendTitle)
                .WithDescription(Resources.CommendInstructions)
                .WithColor(Color.LightOrange)
                .Build());

            foreach (var teacherTag in teacher.Tags)
            {
                var message = await ReplyAsync(embed: new EmbedBuilder()
                    .WithTitle(teacherTag.Tag.Key)
                    .WithDescription(teacherTag.Tag.Description)
                    .Build());

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