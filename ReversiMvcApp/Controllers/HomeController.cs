using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Models;
using System.Diagnostics;
using System.Threading.Tasks;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            await _playerController.GetLoggedInPlayer(this);

            return View();
        }
    }
}

