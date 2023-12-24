using OBilet.SDK.Models;

namespace OBilet.SDK.Abstractions
{
    public interface IOBiletClientService
    {
        Task<GetSessionResponse> GetSession(GetSessionRequest request);
        Task<GetBusLocationResponse> GetBusLocations(GetBusLocationRequest request);
        Task<GetJourneysResponse> GetJourneys(GetJourneyRequest request);
    }
}
