using System;
using System.Web;

namespace FSL.CacheProvider.Caching
{
    public class HttpRuntimeCache : ICache
    {
        public bool IsCacheNull
        {
            get
            {
                return HttpRuntime.Cache == null;
            }
        }

        public object Get(string key)
        {
            if (IsCacheNull)
            {
                return null;
            }

            return HttpRuntime.Cache.Remove(key);
        }

        public void Insert(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            if (IsCacheNull)
            {
                return;
            }

            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, slidingExpiration);
        }

        public object Remove(string key)
        {
            if (IsCacheNull)
            {
                return null;
            }

            return HttpRuntime.Cache.Remove(key);
        }
    }
}