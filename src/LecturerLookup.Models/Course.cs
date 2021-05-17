using System;
using System.Collections.Generic;

namespace LecturerLookup.Models
{
    public class Course : Entity<string>
    {
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
        
        public Course(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Course()
        {
        }
    }
}