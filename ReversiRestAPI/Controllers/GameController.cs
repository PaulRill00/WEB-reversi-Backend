using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ReversiRestAPI.DAL;
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
        public async Task<ActionResult<IEnumerable<APIGame>>> GetAllGames()
        {
            var games = await iRepository.GetGames();
            return games.Select(APIGame.FromGame).ToList();
        }

        // GET api/game/waiting
        [HttpGet("waiting")]
        public async Task<ActionResult<IEnumerable<APIGame>>> GetGameDescriptionsOfGamesWithWaitingPlayers()
        {
            var games = await iRepository.GetGames();
            return games.FindAll(x => x.Player2Token == "").Select(APIGame.FromGame).ToList();
        }

        // POST api/game
        [HttpPost]
        public async Task AddNewGame([FromBody] APIGame game)
        {
            await iRepository.AddGame(new Game()
            {
                Player1Token = game.Player1Token ?? "",
                Description = game.Description ?? "",
            });
        }

        // GET api/game/{token}
        [HttpGet("{token}")]
        public async Task<ActionResult<APIGame>> GetGameByToken(string token)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Game not found");

            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/start
        [HttpPut("{token}/start")]
        public async Task<ActionResult<APIGame>> StartGame(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Game not found");

            var result = game.StartGame(body.Player);

            if (!result)
                throw new Exception("Could not start the game");

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/join
        [HttpPut("{token}/join")]
        public async Task<ActionResult<APIGame>> JoinGame(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Game not found");

            var result = game.Join(body.Player);

            if (!result)
                throw new HttpRequestException("Could not join the game");

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // GET api/game/{token}/turn
        [HttpGet("{token}/turn")]
        public async Task<ActionResult<string>> GetCurrentPlayer(string token)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return NotFound();

            return game.Moving.ToString();
        }

        // PUT api/game/{token}/move
        [HttpPut("{token}/move")]
        public async Task<ActionResult<APIGame>> SetMove(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return StatusCode((int)HttpStatusCode.NotFound, "Game not found");

            if (game.GetPlayerColor(body.Player) != game.Moving)
                return StatusCode((int)HttpStatusCode.Unauthorized, "It is not your turn");

            var result = game.Move(body.RowMove, body.ColMove);
            if (!result)
                return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid move");

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/surrender
        [HttpPut("{token}/surrender")]
        public async Task<ActionResult<APIGame>> Surrender(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return NotFound();

            game.Surrender(body.Player);
            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }
    }
}
