using Microsoft.Extensions.Caching.Memory;
using OBiletCase.Extensions;

namespace OBiletCase.Services
{
    public class CacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSetData<T>(string cacheKey, Func<Task<T>> dataRetrievalFunction)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out T cachedData))
            {
                cachedData = await dataRetrievalFunction();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                };
                _memoryCache.Set(cacheKey, cachedData, cacheEntryOptions);
            }

            return cachedData;
        }
    }
}
