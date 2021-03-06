using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinfWhoIs.Models
{
    public class TeacherCommend : Entity<int>
    {
        public ulong AuthoredBy { get; set; }

        public string Content { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }

    public class Teacher : Entity<int>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsApproved { get; set; }

        public List<TeacherCommend> Commends { get; set; } = new();

        public List<Course> Courses { get; set; } = new();

        public List<TeacherTag> Tags { get; set; } = new();


        public IReadOnlyCollection<TeacherTag> DetermineTopTags(int top = 3) =>
            Tags?.OrderByDescending(x => x.Evaluation.CalculatedScore)
                .Take(top)
                .ToList();

        public string FormatContactDetails() => new StringBuilder()
            .AppendLine(Email)
            .AppendLine(Telephone)
            .ToString();
    }
}