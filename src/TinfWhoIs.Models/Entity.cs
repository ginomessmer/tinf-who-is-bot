using System.ComponentModel.DataAnnotations;

namespace TinfWhoIs.Models
{
    public abstract class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}