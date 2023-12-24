using OBilet.SDK.Abstractions;
using OBilet.SDK.Models;
using OBiletCase.Abstractions;
using OBiletCase.Models;
using UAParser;

namespace OBiletCase.Services
{
    public class OBiletSessionProvider : ISessionProvider
    {

        private readonly IOBiletClientService _oBiletClientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OBiletSessionProvider(IOBiletClientService oBiletClientService, IHttpContextAccessor httpContextAccessor)
        {
            _oBiletClientService = oBiletClientService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Session> Create()
        {
            Connection connection = GetConnection();
            Browser browser = GetBrowser();

            GetSessionRequest request = Map(connection, browser);
            GetSessionResponse response = await _oBiletClientService.GetSession(request);

            Session result = Map(response);

            return result;
        }

        private GetSessionRequest Map(Connection connection, Browser browser)
        {
            GetSessionRequest request = new() { Type = 1 };
            request.Browser = new()
            {
                Name = browser.Name,
                Version = browser.Version
            };

            request.Connection = new()
            {
                IpAddress = connection.IpAddress,
                Port = connection.Port
            };

            return request;
        }

        private Session Map(GetSessionResponse response)
        {
            Session session = new()
            {
                DeviceId = response.Data.DeviceId,
                Id = response.Data.SessionId
            };

            return session;
        }

        private Browser GetBrowser()
        {
            string? userAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.FirstOrDefault();
            var uaParser = Parser.GetDefault();

            ClientInfo c = uaParser.Parse(userAgent);
            Browser browser = new() { Name = c.UA.Family, Version = c.UA.Major };

            return browser;
        }

        private Connection GetConnection()
        {
            ConnectionInfo connection = _httpContextAccessor.HttpContext.Connection;
            string ipAddress = connection.RemoteIpAddress.ToString();
            string port = connection.RemotePort.ToString();

            Connection result = new() { IpAddress = ipAddress, Port = port };

            return result;
        }
    }
}
