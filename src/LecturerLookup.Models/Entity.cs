using System.ComponentModel.DataAnnotations;

namespace LecturerLookup.Models
{
    public abstract class Entity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}