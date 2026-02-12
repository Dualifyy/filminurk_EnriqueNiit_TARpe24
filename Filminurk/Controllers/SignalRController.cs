using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class SignalRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
