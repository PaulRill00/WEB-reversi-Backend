using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReversiRestAPI.Interfaces;
using ReversiRestApi.Models;
using ReversiRestAPI.Models.API;

namespace ReversiRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository iRepository;

        public GameController(IGameRepository iRepository)
        {
            this.iRepository = iRepository;
        }

        // GET api/game
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetGameDescriptionsOfGamesWithWaitingPlayers() =>
            iRepository.GetGames().FindAll(x => x.Player2Token is null).Select(x => x.Description).ToList();

        // POST api/game
        [HttpPost]
        public void AddNewGame([FromBody] APIGame game)
        {
            Debug.WriteLine(game);
            iRepository.AddGame(new Game()
            {
                Player1Token = game.Player1Token,
                Player2Token = game.Player2Token,
                Description = game.Description,
            });
        }

        // GET api/game/{token}
        [HttpGet("{token}")]
        public ActionResult<APIGame> GetGameByToken(string token)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            return APIGame.FromGame(game);
        }
    }
}
