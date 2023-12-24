using OBiletCase.Abstractions;
using OBiletCase.Models;

namespace OBiletCase.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISessionProvider _sessionProvider;
        private readonly ISessionService _sessionService;

        public SessionMiddleware(RequestDelegate next, ISessionProvider sessionProvider, ISessionService sessionService)
        {
            _next = next;
            _sessionProvider = sessionProvider;
            _sessionService = sessionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Session? currentSession = _sessionService.Get();
            if (currentSession is null)
            {
                Session session = await _sessionProvider.Create();
                _sessionService.Set(session);
            }


            await _next(context);
        }


    }
}
