using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
