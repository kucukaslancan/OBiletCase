using OBiletCase.Models;

namespace OBiletCase.Abstractions
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<IEnumerable<Location>> GetPopularsAsync();
        Task<IEnumerable<Location>> GetByAsync(string text);
    }
}
