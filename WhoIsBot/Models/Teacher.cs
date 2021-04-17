using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
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
    }
}