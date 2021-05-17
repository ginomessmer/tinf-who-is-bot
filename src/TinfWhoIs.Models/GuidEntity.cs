using System;

namespace TinfWhoIs.Models
{
    public abstract class GuidEntity : Entity<Guid>
    {
        protected GuidEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}