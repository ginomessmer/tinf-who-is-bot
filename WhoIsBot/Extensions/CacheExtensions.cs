using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WhoIsBot.Models;

namespace WhoIsBot.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var content = await cache.GetStringAsync(key);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }

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