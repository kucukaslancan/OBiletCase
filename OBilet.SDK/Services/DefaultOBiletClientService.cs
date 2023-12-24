using OBilet.SDK.Abstractions;
using OBilet.SDK.Models;
using RestSharp;

namespace OBilet.SDK.Services
{
    public class DefaultOBiletClientService : IOBiletClientService
    {
        private readonly RestClient _client;

        public DefaultOBiletClientService(OBiletSDKConfiguration configuration)
        {
            _client = new RestClient(configuration.BaseApiUrl);
            _client.AddDefaultHeader("Authorization", configuration.Authorization);
        }


        public async Task<GetSessionResponse> GetSession(GetSessionRequest request)
        {
            GetSessionResponse response = await _client.PostJsonAsync<GetSessionRequest, GetSessionResponse>("client/getsession", request);

            return response;
        }

        public async Task<GetBusLocationResponse> GetBusLocations(GetBusLocationRequest request)
        {
            GetBusLocationResponse response = await _client.PostJsonAsync<GetBusLocationRequest, GetBusLocationResponse>("location/getbuslocations", request);

            return response;
        }

        public async Task<GetJourneysResponse> GetJourneys(GetJourneyRequest request)
        {
            GetJourneysResponse response = await _client.PostJsonAsync<GetJourneyRequest, GetJourneysResponse>("journey/getbusjourneys", request);

            return response;
        }
    }
}
