using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly PlayerController _playerController;

        public HomeController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            if (!_playerController.IsLoggedIn(this))
            {
                return Redirect("Identity/Account/Login");
            }
                
            return View();
        }
    }
}
