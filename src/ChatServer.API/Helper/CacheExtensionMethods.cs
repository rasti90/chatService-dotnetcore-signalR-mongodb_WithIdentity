using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.API.Helper
{
    public static class CacheExtensionMethods
    {
        public static void SetObject(this IDistributedCache cache, string key, object value)
        {
            cache.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public static async Task SetObjectAsync(this IDistributedCache cache, string key, object value)
        {
            await cache.SetStringAsync(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public static TResult GetObject<TResult>(this IDistributedCache cache, string key)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(cache.GetString(key));
            }
            catch
            {
                return default;
            }
        }

        public static async Task<TResult> GetObjectAsync<TResult>(this IDistributedCache cache, string key)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(await cache.GetStringAsync(key));
            }
            catch
            {
                return default;
            }
        }

    }
}
