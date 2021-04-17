using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhoIsBot.Models
{
    public class Course : Entity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}