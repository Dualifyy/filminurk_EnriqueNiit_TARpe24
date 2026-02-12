using Filminurk.Core.Domain;
using Filminurk.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class SignalRController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public SignalRController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        { 
            _userManager = userManager; 
            _signInManager = signInManager; 
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new ChatViewModel { };

            vm.DisplayName = user.DisplayName;


            return View("Index" ,vm);
        }
    }
}
