using System;
using System.Web;

namespace Snippy.Web.Infrastructure
{
    public abstract class BaseCacheService
    {
        protected T Get<T>(string cacheKey, Func<T> getItemCallback)
           where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheKey, item);
                return item;
            }

            return item;
        }
    }
}