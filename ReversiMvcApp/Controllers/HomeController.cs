using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
