using OBilet.SDK.Abstractions;
using OBilet.SDK.Models;
using OBiletCase.Abstractions;
using OBiletCase.Models;

namespace OBiletCase.Services.Journeys
{
    public class DefaultJourneyService : IJourneyService
    {
        private readonly IOBiletClientService _obiletClientService;
        private readonly ISessionService _sessionService;

        public DefaultJourneyService(IOBiletClientService obiletClientService, ISessionService sessionService)
        {
            _obiletClientService = obiletClientService;
            _sessionService = sessionService;
        }

        public async Task<IEnumerable<Journey>> GetByAsync(int originId, int destinationId, DateTime departureDate)
        {
            Session session = _sessionService.Get();
            GetJourneyRequest request = new();
            request.DeviceSession = new() { DeviceId = session.DeviceId, Id = session.Id };
            request.Date = DateTime.Now;
            request.Language = "tr-TR";
            request.Data = new() { OriginId = originId, DestinationId = destinationId, DepartureDate = departureDate };

            GetJourneysResponse response = await _obiletClientService.GetJourneys(request);
            IEnumerable<Journey> result = Map(response);

            return result;
        }

        private IEnumerable<Journey> Map(GetJourneysResponse response)
        {
            List<Journey> result = new List<Journey>();
            foreach (var item in response.Data.OrderBy(t => t.Journey.Departure))
            {
                JourneyData journeyData = item.Journey;
                Journey journey = new()
                {
                    Arrival = journeyData.Arrival,
                    Departure = journeyData.Departure,
                    DestinationName = journeyData.Destination,
                    OriginName = journeyData.Origin,
                    Price = journeyData.InternetPrice,
                    Currency = journeyData.Currency
                };
                result.Add(journey);
            }
           
            
            return result;
        }
    }
}
