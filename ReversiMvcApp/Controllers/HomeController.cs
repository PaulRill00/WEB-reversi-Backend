using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReversiMvcApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using ReversiMvcApp.Data;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlayerController _playerController;
        private readonly ApiController _apiController;

        public HomeController(ILogger<HomeController> logger, PlayerController playerController, ApiController apiController)
        {
            _logger = logger;
            _playerController = playerController;
            _apiController = apiController;
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
            return View();
        }
    }
}
