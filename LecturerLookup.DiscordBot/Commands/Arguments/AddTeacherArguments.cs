using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace LecturerLookup.DiscordBot.Commands.Arguments
{
    [NamedArgumentType]
    public class AddTeacherArguments
    {
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Subtitle { get; set; }

        public IEnumerable<string> Courses { get; set; } = new List<string>();
    }
}
