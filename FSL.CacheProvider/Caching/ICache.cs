using System;

namespace FSL.CacheProvider.Caching
{
    public interface ICache
    {
        object Get(string key);
        void Insert(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        object Remove(string key);
        bool IsCacheNull { get; }
    }
}