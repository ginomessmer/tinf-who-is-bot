using System.Collections.Generic;
using System.Linq;

namespace WhoIsBot
{
    public class Profile
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public List<ProfileInterest> Interests { get; } = new();

        public Profile(ulong id, params ProfileInterest[] interests)
        {
            Id = id;
            Interests = interests.ToList();
        }

        public Profile()
        {
        }
    }
}