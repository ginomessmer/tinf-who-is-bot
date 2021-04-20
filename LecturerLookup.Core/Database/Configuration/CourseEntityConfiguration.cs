using System.Collections.Generic;
using LecturerLookup.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecturerLookup.Core.Database.Configuration
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(Seed());
        }

        public static IEnumerable<Course> Seed() => new Course[]
        {
            new("analysis", "Mathematik - Analysis"),
            new("algebra", "Mathematik - Lineare Algebra"),
            new("statistik", "Mathematik - Statistik"),

            new("ic", "Intercultural Communication"),
            new("pm", "Projektmanagement"),
            new("bwl", "BWL"),
            new("marketing", "Marketing"),

            new("prog", "Programmieren"),
            new("se", "Software Engineering"),
            new("ase", "Advanced Software Engineering"),

            new("ti1", "Theoretische Informatik I"),
            new("ti2", "Theoretische Informatik II"),
            new("fs", "Formale Sprachen"),
            new("cb", "Compilerbau"),

            new("we1", "Web Engineering I"),
            new("we2", "Web Engineering II"),
        };
    }
}