using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;
        public AccuWeatherController(IWeatherForecastServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
