using OBilet.SDK.Abstractions;
using OBilet.SDK.Models;
using OBiletCase.Abstractions;
using OBiletCase.Extensions;
using OBiletCase.Models;

namespace OBiletCase.Services.Locations
{
    public class DefaultLocationService : ILocationService
    {
        private readonly IOBiletClientService _obiletClientService;
        private readonly ISessionService _sessionService;

        public DefaultLocationService(IOBiletClientService obiletClientService, ISessionService sessionService)
        {
            _obiletClientService = obiletClientService;
            _sessionService = sessionService;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            IEnumerable<Location> response = await GetByAsync(string.Empty);

            return response;    
        }

        public async Task<IEnumerable<Location>> GetByAsync(string? text)
        {
            Session session = _sessionService.Get();
            GetBusLocationRequest request = Map(session, text);

            GetBusLocationResponse response = await _obiletClientService.GetBusLocations(request);
            List<Location> result = response.Data.Select(t => new Location { Id = t.Id, Name = t.LongName }).ToList();

            return result;
        }

        public async Task<IEnumerable<Location>> GetPopularsAsync()
        {
            Session session = _sessionService.Get();
            GetBusLocationRequest request = Map(session);

            GetBusLocationResponse response = await _obiletClientService.GetBusLocations(request);
            List<Location> result = response.Data.Select(t => new Location { Id = t.Id, Name = t.LongName }).Take(10).ToList();

            return result;
        }

        private GetBusLocationRequest Map(Session? session)
        {
            GetBusLocationRequest request = new();
            request.DeviceSession = new()
            {
                DeviceId = session.DeviceId,
                Id = session.Id
            };
            request.Date = DateTime.Now;
            request.Data = string.Empty;
            request.Language = "tr-TR";

            return request;
        }

        private GetBusLocationRequest Map(Session? session, string text)
        {
            GetBusLocationRequest request = new();
            request.DeviceSession = new()
            {
                DeviceId = session.DeviceId,
                Id = session.Id
            };
            request.Date = DateTime.Now;
            request.Data = text;
            request.Language = "tr-TR";

            return request;
        }
    }
}
