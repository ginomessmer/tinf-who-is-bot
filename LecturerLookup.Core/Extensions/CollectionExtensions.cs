using LecturerLookup.Models;
using System.Collections.Generic;
using System.Linq;

namespace LecturerLookup.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddOrUpdate<T, TZ>(this IList<T> entities, T entity) where T : Entity<TZ>
        {
            var foundEntity = entities.FirstOrDefault(x => x.Id.Equals(entity.Id));
            if (foundEntity is null)
                entities.Add(entity);
            else
            {
                var index = entities.IndexOf(foundEntity);
                entities.RemoveAt(index);
                entities.Insert(index, entity);
            }
        }
    }
}