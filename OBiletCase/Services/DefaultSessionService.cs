using OBiletCase.Abstractions;
using OBiletCase.Extensions;
using OBiletCase.Models;

namespace OBiletCase.Services
{
    public class DefaultSessionService : ISessionService
    {
        private const string SESSION_KEY = "SESSION_KEY";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Session Get()  => _httpContextAccessor.HttpContext.Session.Get<Session>(SESSION_KEY);
        public void Set(Session session) => _httpContextAccessor.HttpContext.Session.Set<Session>(SESSION_KEY, session);

    }
}
