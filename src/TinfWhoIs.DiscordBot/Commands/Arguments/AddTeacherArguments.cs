using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using FluentValidation;

namespace TinfWhoIs.DiscordBot.Commands.Arguments
{
    [NamedArgumentType]
    public class AddTeacherArguments
    {
        public string Name { get; set; }

        public string Office { get; set; }

        public string AvatarUrl { get; set; }

        public string Location { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Courses { get; set; } = new List<string>();

        public class Validator : AbstractValidator<AddTeacherArguments>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Telephone);
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Office).MaximumLength(30);
                RuleFor(x => x.AvatarUrl)
                    .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute)).WithMessage("Avatar is not a valid URL.")
                    .Unless(x => string.IsNullOrEmpty(x.AvatarUrl));
            }
        }
    }
}
