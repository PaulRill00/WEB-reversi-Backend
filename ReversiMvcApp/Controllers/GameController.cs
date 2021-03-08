using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ReversiMvcApp.Data;
using ReversiRestAPI.Models.API;
using ReversiRestAPI.Models.Database;

namespace ReversiMvcApp.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlayerController _playerController;
        private readonly ApiController _apiController;

        public GameController(ILogger<HomeController> logger, PlayerController playerController, ApiController apiController)
        {
            _logger = logger;
            _playerController = playerController;
            _apiController = apiController;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var games = await _apiController.GetListAsync<APIGame>("game/waiting");

            ViewData["Error"] = ViewData["Error"] ?? "";

            return View(games);
        }

        [Authorize]
        public async Task<IActionResult> Joined()
        {
            var playerToken = _playerController.GetLoggedInPlayer(this).Guid;
            var games = await _apiController.GetListAsync<APIGame>($"player/{playerToken}/games");

            ViewData["Error"] = TempData["Error"] ?? "";

            return View(games);
        }

        [Authorize]
        public async Task<IActionResult> Join(string gameToken)
        {
            var playerToken = _playerController.GetLoggedInPlayer(this).Guid;
            try
            {
                await _apiController.PutAsync($"game/{gameToken}/join",
                    new APIGame() {Player2Token = playerToken});
            }
            catch (HttpRequestException e)
            {
                TempData["Error"] = "Could not join the game";
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string description)
        {
            var playerToken = _playerController.GetLoggedInPlayer(this).Guid;
            await _apiController.PostAsync("game", new APIGame()
            {
                Player1Token = playerToken,
                Description = description,
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/{token}")]
        public async Task<IActionResult> Game(string token)
        {
            var game = await _apiController.GetAsync<APIGame>($"game/{token}/");

            return View();
        } 
    }
}
