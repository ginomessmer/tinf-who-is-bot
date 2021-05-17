using System;

namespace LecturerLookup.Models
{
    public abstract class GuidEntity : Entity<Guid>
    {
        protected GuidEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}