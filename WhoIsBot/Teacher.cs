using System;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Location { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }
    }
}