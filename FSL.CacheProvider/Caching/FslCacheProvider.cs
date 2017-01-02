using FSL.CsharpUsefulExtensionsMethods;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FSL.CacheProvider.Caching
{
    public class FslCacheProvider : ICacheProvider, IAsyncCacheProvider
    {
        public FslCacheProvider(ICache cache)
        {
            _cache = cache;
        }

        public T Cache<T>(Expression<Func<T>> func, params object[] keys)
        {
            return Cache(true, GetExpirationDefault(), func, keys);
        }

        public T Cache<T>(Expression<Func<T>> func, Func<object[]> keys)
        {
            if (keys == null)
            {
                return Cache(true, func);
            }

            return Cache(true, func, keys());
        }

        public T Cache<T>(Func<bool> useCache, Expression<Func<T>> func, params object[] keys)
        {
            return Cache(useCache(), GetExpirationDefault(), func, keys);
        }

        public T Cache<T>(bool useCache, Expression<Func<T>> func, params object[] keys)
        {
            return Cache(useCache, GetExpirationDefault(), func, keys);
        }

        public T Cache<T>(DateTime expiration, Expression<Func<T>> func, params object[] keys)
        {
            return Cache(true, expiration, func, keys);
        }

        public T Cache<T>(Func<bool> useCache, DateTime expiration, Expression<Func<T>> func, params object[] keys)
        {
            return Cache(useCache(), expiration, func, keys);
        }

        public T Cache<T>(bool useCache, DateTime expiration, Expression<Func<T>> func, params object[] keys)
        {
            if (_cache.IsCacheNull == null || !useCache)
            {
                return func.Compile()();
            }

            var cacheKey = BuildCacheKey(func, keys);
            if (cacheKey.IsNullOrEmpty())
            {
                return func.Compile()();
            }

            var cacheValue = _cache.Get(cacheKey);
            if (cacheValue != null)
            {
                return (T)cacheValue;
            }
            else
            {
                var data = func.Compile()();

                InsertCache(cacheKey, data, expiration);

                return data;
            }
        }

        public void Remove<T>(Expression<Func<Task<T>>> func, params object[] keys)
        {
            var cacheKey = BuildCacheKey(func, keys);

            lock (cacheObject)
            {
                _cache.Remove(cacheKey);
            }
        }

        public async Task<T> CacheAsync<T>(Expression<Func<Task<T>>> func, params object[] keys)
        {
            return await CacheAsync(true, GetExpirationDefault(), func, keys);
        }

        public async Task<T> CacheAsync<T>(Expression<Func<Task<T>>> func, Func<object[]> keys)
        {
            if (keys == null)
            {
                return await CacheAsync(true, func);
            }

            return await CacheAsync(true, func, keys());
        }

        public async Task<T> CacheAsync<T>(Func<bool> useCache, Expression<Func<Task<T>>> func, params object[] keys)
        {
            return await CacheAsync(useCache(), GetExpirationDefault(), func, keys);
        }

        public async Task<T> CacheAsync<T>(bool useCache, Expression<Func<Task<T>>> func, params object[] keys)
        {
            return await CacheAsync(useCache, GetExpirationDefault(), func, keys);
        }

        public async Task<T> CacheAsync<T>(DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys)
        {
            return await CacheAsync(true, expiration, func, keys);
        }

        public async Task<T> CacheAsync<T>(Func<bool> useCache, DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys)
        {
            return await CacheAsync(useCache(), expiration, func, keys);
        }

        public async Task<T> CacheAsync<T>(bool useCache, DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys)
        {
            if (_cache.IsCacheNull || !useCache)
            {
                return await func.Compile()();
            }

            var cacheKey = BuildCacheKey(func, keys);
            if (cacheKey.IsNullOrEmpty())
            {
                return await func.Compile()();
            }

            var cacheValue = _cache.Get(cacheKey);
            if (cacheValue != null)
            {
                return (T)cacheValue;
            }
            else
            {
                var data = await func.Compile()();

                InsertCache(cacheKey, data, expiration);

                return data;
            }
        }

        private string BuildCacheKey<T>(Expression<Func<T>> func, object[] keys)
        {
            var cacheKey = "";

            if (keys != null)
            {
                keys.ToList().ForEach(x => cacheKey += (x == null ? "" : "_" + x.ToString()));
                
                //if (cacheKey.IsNullOrEmpty())
                //{
                //    return null;
                //}
            }

            var methodCall = func.Body as MethodCallExpression;
            cacheKey = (methodCall != null ? methodCall.Method.DeclaringType.Name + "." + methodCall.Method.Name : "") + cacheKey;

            return cacheKey.Remove(" ");
        }


        private static object cacheObject = new object();
        private readonly ICache _cache;

        private void InsertCache(string cacheKey, object data, DateTime expiration)
        {
            if (data == null) return;

            lock (cacheObject)
            {
                _cache.Insert(cacheKey, data, expiration, TimeSpan.Zero);
            }
        }

        private DateTime GetExpirationDefault()
        {
            return DateTime.Now.AddMinutes(1);
        }
    }
}