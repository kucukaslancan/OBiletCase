using Microsoft.AspNetCore.Mvc;
using OBiletCase.Abstractions;
using OBiletCase.Models;
using System.Diagnostics;

namespace OBiletCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocationService _locationService;

        public HomeController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetPopularLocations()
        {
            IEnumerable<Location> response = await _locationService.GetPopularsAsync();

            return Json(response);
        }

        public async Task<JsonResult> SearchLocationAsync(string searchText)
        {
            IEnumerable<Location> response = await _locationService.GetByAsync(searchText);

            return Json(response);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}