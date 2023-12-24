using OBiletCase.Abstractions;
using OBiletCase.Models;

namespace OBiletCase.Services.Locations
{
    public class CacheDecorator : ILocationService
    {
        private readonly ILocationService _decorated;
        private readonly CacheService _cacheService;

        public CacheDecorator(ILocationService decorated, CacheService cacheService)
        {
            _cacheService = cacheService;
            _decorated = decorated;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            IEnumerable<Location> result = await _cacheService.GetOrSetData<IEnumerable<Location>>(nameof(GetAllAsync), _decorated.GetAllAsync);

            return result;
        }

        public async Task<IEnumerable<Location>> GetByAsync(string text)
        {
            IEnumerable<Location> cachedLocationData = await this.GetAllAsync();

            List<Location> result = cachedLocationData.Where(location => location.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
           .ToList();

            return result;
        }

        public async Task<IEnumerable<Location>> GetPopularsAsync()
        {
            IEnumerable<Location> result = await _cacheService.GetOrSetData<IEnumerable<Location>>(nameof(GetPopularsAsync), _decorated.GetPopularsAsync);

            return result;
        }
    }
}
