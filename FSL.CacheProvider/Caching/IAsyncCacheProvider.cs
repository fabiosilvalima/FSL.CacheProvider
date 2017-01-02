using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FSL.CacheProvider.Caching
{
    public interface IAsyncCacheProvider
    {
        void Remove<T>(Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(Expression<Func<Task<T>>> func, Func<object[]> keys);
        Task<T> CacheAsync<T>(Func<bool> useCache, Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(bool useCache, Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(Func<bool> useCache, DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys);
        Task<T> CacheAsync<T>(bool useCache, DateTime expiration, Expression<Func<Task<T>>> func, params object[] keys);
    }
}