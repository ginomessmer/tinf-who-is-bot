using System.Collections.Generic;
using System.Linq;

namespace LecturerLookup.Models
{
    public class Teacher : Entity<int>
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public List<TeacherTag> Tags { get; set; } = new List<TeacherTag>();


        public IReadOnlyCollection<TeacherTag> DetermineTopTags(int top = 3) =>
            Tags?.OrderByDescending(x => x.Evaluation.CalculatedScore)
                .Take(top)
                .ToList();
    }
}