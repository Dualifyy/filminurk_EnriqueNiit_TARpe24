using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
