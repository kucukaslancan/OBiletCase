using Microsoft.AspNetCore.Mvc;
using OBiletCase.Abstractions;
using OBiletCase.Exceptions;
using OBiletCase.Models;
using Vereyon.Web;

namespace OBiletCase.Controllers
{
    public class JourneyController : Controller
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(JourneyIndexRequest request)
        {
            var response = await _journeyService.GetByAsync(request.Origin, request.Destination, request.DepartureDate);

            ViewData["existJourneyList"] = true;
            ViewData["JourneyArea"] = response.First().OriginName + " - " + response.First().DestinationName;
            ViewData["JourneyDate"] = response.First().Departure.ToString("dd MMMM dddd");

            return View(response);
        }
    }
}
