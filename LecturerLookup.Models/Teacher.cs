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

        public ICollection<TeacherRate> Ratings { get; set; } = new List<TeacherRate>();
    }
}