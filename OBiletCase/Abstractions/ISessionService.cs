using OBiletCase.Models;

namespace OBiletCase.Abstractions
{
    public interface ISessionService
    {
        Session? Get();
        void Set(Session session);
    }
}
