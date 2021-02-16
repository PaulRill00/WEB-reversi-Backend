using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReversiRestAPI.Enums;
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
        public ActionResult<IEnumerable<APIGame>> GetAllGames() =>
            iRepository.GetGames().Select(APIGame.FromGame).ToList();

        // GET api/game/waiting
        [HttpGet("waiting")]
        public ActionResult<IEnumerable<APIGame>> GetGameDescriptionsOfGamesWithWaitingPlayers() =>
            iRepository.GetGames().FindAll(x => x.Player2Token == "").Select(APIGame.FromGame).ToList();

        // POST api/game
        [HttpPost]
        public void AddNewGame([FromBody] APIGame game)
        {

            iRepository.AddGame(new Game()
            {
                Player1Token = game.Player1Token ?? "",
                Description = game.Description ?? "",
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

        // PUT api/game/{token}/start
        [HttpPut("{token}/start")]
        public ActionResult<APIGame> StartGame(string token, [FromBody] APIAction body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();
            
            var result = game.StartGame(body.Player);

            if (!result)
                return Forbid();

            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/join
        [HttpPut("{token}/join")]
        public ActionResult<APIGame> JoinGame(string token, [FromBody] APIGame body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (body.Player2Token is null)
            {

            }

            var result = game.Join(body.Player2Token);

            if (!result)
                return Forbid();

            return APIGame.FromGame(game);
        }

        // GET api/game/{token}/turn
        [HttpGet("{token}/turn")]
        public ActionResult<string> GetCurrentPlayer(string token)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            return game.Moving.ToString();
        }

        // PUT api/game/{token}/move
        [HttpPut("{token}/move")]
        public ActionResult<APIGame> SetMove(string token, [FromBody] APIAction body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            if (game.GetPlayerColor(body.Player) != game.Moving)
                return Unauthorized();

            var result = game.Move(body.RowMove, body.ColMove);
            if (result)
                return APIGame.FromGame(game);

            return Unauthorized();
        }

        // PUT api/game/{token}/surrender
        [HttpPut("{token}/surrender")]
        public ActionResult<APIGame> Surrender(string token, [FromBody] APIAction body)
        {
            var game = iRepository.GetGame(token);
            if (game is null)
                return NotFound();

            game.Surrender(body.Player);
            return APIGame.FromGame(game);
        }
    }
}
