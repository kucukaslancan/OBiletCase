using OBiletCase.Abstractions;
using OBiletCase.Exceptions;
using OBiletCase.Models;

namespace OBiletCase.Services.Journeys
{
    public class RequestValidationDecorator : IJourneyService
    {
        private readonly IJourneyService _decorated;

        public RequestValidationDecorator(IJourneyService decorated)
        {
            _decorated = decorated;
        }

        public Task<IEnumerable<Journey>> GetByAsync(int originId, int destinationId, DateTime departureDate)
        {
            if(originId == destinationId)
            {
                throw new ClientException("Kalkış ve Varış noktaları aynı olamaz!");
            }

            if(departureDate < DateTime.Today)
            {
                throw new ClientException("Kalkış tarihi bugünden küçük olamaz!");
            }

            return _decorated.GetByAsync(originId, destinationId, departureDate);
        }
    }
}
