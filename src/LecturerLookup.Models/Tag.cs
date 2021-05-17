using System.Collections.Generic;

namespace LecturerLookup.Models
{
    public class Tag : Entity<int>
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public ICollection<TeacherTag> TeacherTags { get; set; }

        public Tag(string key)
        {
            Key = key;
        }

        public Tag(int id, string key, string description)
        {
            Id = id;
            Key = key;
            Description = description;
        }
    }
}