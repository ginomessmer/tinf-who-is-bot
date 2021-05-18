using System;
using Discord;
using Discord.Commands;
using TinfWhoIs.Core.Database;
using TinfWhoIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentValidation;
using TinfWhoIs.DiscordBot.Commands.Arguments;
using TinfWhoIs.DiscordBot.Internal;
using TinfWhoIs.DiscordBot.Properties;
using TinfWhoIs.DiscordBot.Services;

namespace TinfWhoIs.DiscordBot.Commands
{
    public partial class TeacherCommands
    {
        [Command("commend")]
        public async Task Commend(int teacherId, [Remainder] string text)
        {
            try
            {
                var validator = new InlineValidator<string>();
                validator.RuleFor(x => x)
                    .MinimumLength(10)
                    .MaximumLength(2000);

                await validator.ValidateAndThrowAsync(text);
            }
            catch (ValidationException ex)
            {
                await ReplyAsync(ex.Message);
                return;
            }

            var teacher = await _dbContext.Teachers.FindAsync(teacherId);
            if (teacher is null)
                return;

            teacher.Commends.Add(new TeacherCommend
            {
                AuthoredBy = Context.User.Id,
                Content = text
            });

            await _dbContext.SaveChangesAsync();
            await ReplyAsync("Deine Empfehlung wurde gespeichert.");
        }
    }

    /// <summary>
    /// Teacher management commands.
    /// </summary>
    public partial class TeacherCommands
    {
        [Command("add")]
        public async Task Add(AddTeacherArguments arguments)
        {
            var validationResult = await _validator.ValidateAsync(arguments);
            if (!validationResult.IsValid)
            {
                await Context.Message.ReplyAsync(embed: validationResult.ToEmbed());
                return;
            }

            var teacher = _mapper.Map<Teacher>(arguments);

            if (arguments.Courses.Any())
            {
                // TODO: Exception handling
                var courses = await Task.WhenAll(arguments.Courses.Select(x => _dbContext.Courses.FindAsync(x).AsTask()));
                var escapedCourses = courses.Where(x => x is not null);
                teacher.Courses.AddRange(escapedCourses);
            }

            await _dbContext.Teachers.AddAsync(teacher);
            await _dbContext.SaveChangesAsync();

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle("Vielen Dank!")
                .WithDescription("Dein Eintrag wurde aufgenommen.")
                .WithColor(Color.Green)
                .Build());
        }
    }

    /// <summary>
    /// Teacher basic commands
    /// </summary>
    public partial class TeacherCommands : ModuleBase<SocketCommandContext>
    {
        private readonly WhoIsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<AddTeacherArguments> _validator;

        public TeacherCommands(WhoIsDbContext dbContext,
            IMapper mapper,
            IValidator<AddTeacherArguments> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        [Command("whois")]
        public async Task WhoIs(string term)
        {
            var teacher = await _dbContext.Teachers
                .Include(x => x.Courses)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, $"%{term}%"));

            if (teacher is null)
            {
                await ReplyAsync("Kein Ergebnis gefunden.");
                return;
            }

            // Courses
            var courses = teacher.Courses;
            var coursesValue = courses.Any() ? string.Join(", ", courses.Select(x => x.Name)) : "-";


            var fields = new List<EmbedFieldBuilder>
            {
                new EmbedFieldBuilder()
                    .WithName("Vorlesungen")
                    .WithValue(coursesValue),
                new EmbedFieldBuilder()
                    .WithName("Kontakt")
                    .WithValue(teacher.FormatContactDetails())
            };

            // Tags - only display them in DM channels
            if (Context.Channel is IDMChannel)
            {
                var tags = teacher.Tags;

                var evaluationService = new ScoreEvaluationService();

                var tagFields = tags.Select(x => new EmbedFieldBuilder()
                    .WithName(x.Tag.Key)
                    .WithValue(evaluationService.GetLabel(x.Evaluation.CalculatedScore))
                    .WithIsInline(false));

                fields.AddRange(tagFields);
            }

            await ReplyAsync(embed: new EmbedBuilder()
                .WithTitle(teacher.Name)
                .WithThumbnailUrl(teacher.AvatarUrl)
                .WithDescription(teacher.Office)
                .WithFields(fields) 
                .WithFooter(FormatFooter())
                .Build());

            string FormatFooter()
            {
                var elements = new List<string> {$"#{teacher.Id}"};
                if (!teacher.IsApproved)
                    elements.Add("Dieser Eintrag wurde noch nicht überprüft");

                return string.Join(" - ", elements);
            }
        }

        [Command("rate")]
        [RequireContext(ContextType.DM)]
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