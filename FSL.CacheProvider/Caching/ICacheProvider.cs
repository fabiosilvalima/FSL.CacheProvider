using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FSL.CacheProvider.Caching
{
    public interface ICacheProvider
    {
        T Cache<T>(Expression<Func<T>> func, params object[] keys);
        T Cache<T>(Expression<Func<T>> func, Func<object[]> keys);
        T Cache<T>(Func<bool> useCache, Expression<Func<T>> func, params object[] keys);
        T Cache<T>(bool useCache, Expression<Func<T>> func, params object[] keys);
        T Cache<T>(DateTime expiration, Expression<Func<T>> func, params object[] keys);
        T Cache<T>(Func<bool> useCache, DateTime expiration, Expression<Func<T>> func, params object[] keys);
        T Cache<T>(bool useCache, DateTime expiration, Expression<Func<T>> func, params object[] keys);
        void Remove<T>(Expression<Func<Task<T>>> func, params object[] keys);
    }
}