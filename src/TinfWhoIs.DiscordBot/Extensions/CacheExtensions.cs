using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace TinfWhoIs.DiscordBot.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var content = await cache.GetStringAsync(key);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}