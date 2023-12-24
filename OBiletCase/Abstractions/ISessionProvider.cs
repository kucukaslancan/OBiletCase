using OBiletCase.Models;

namespace OBiletCase.Abstractions
{
    public interface ISessionProvider
    {
        Task<Session> Create();
    }
}
