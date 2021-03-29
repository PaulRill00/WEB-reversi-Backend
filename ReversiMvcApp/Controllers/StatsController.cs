using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiMvcApp.Data;
using ReversiRestAPI.Models.API;

namespace ReversiMvcApp.Controllers
{
    public class StatsController : Controller
    {
        private readonly PlayerController _playerController;
        private readonly ApiController _apiController;

        public StatsController(PlayerController playerController, ApiController apiController)
        {
            _playerController = playerController;
            _apiController = apiController;
        }
        public async Task<IActionResult> Index()
        {
            var stats = await _apiController.GetListAsync<APIStats>("player/stats/");
            return View(stats);
        }
    }
}
