using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        public string Key { get; set; }
    }

    public class TagVote
    {
        
    }

    public class TeacherTag
    {
        public Tag Tag { get; set; }

        public ICollection<TagVote> Votes { get; set; }
    }

    public class Teacher
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Location { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}