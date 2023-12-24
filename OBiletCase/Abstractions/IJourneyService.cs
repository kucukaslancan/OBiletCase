using OBiletCase.Models;

namespace OBiletCase.Abstractions
{
    public interface IJourneyService
    {
        Task<IEnumerable<Journey>> GetByAsync(int originId, int destinationId, DateTime departureDate);
    }
}
