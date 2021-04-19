using System;
using System.Collections.Generic;

namespace LecturerLookup.Models
{
    public class Course : Entity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}