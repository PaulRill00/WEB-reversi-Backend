using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReversiRestAPI.Helpers;
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
        private readonly ResponseHelper _helper;

        public GameController(IGameRepository iRepository)
        {
            this.iRepository = iRepository;
            _helper = new ResponseHelper(this);
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
        public async Task<ActionResult> AddNewGame([FromBody] APIGame game)
        {
            if (string.IsNullOrEmpty(game.Description))
                return _helper.GetStatusCode(HttpStatusCode.BadRequest, "No description provided", true);

            await iRepository.AddGame(new Game()
            {
                Player1Token = game.Player1Token ?? "",
                Description = game.Description ?? "",
            });

            return _helper.GetStatusCode(HttpStatusCode.OK, "Game Added");
        }

        // GET api/game/{token}
        [HttpGet("{token}")]
        public async Task<ActionResult<APIGame>> GetGameByToken(string token)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/start
        [HttpPut("{token}/start")]
        public async Task<ActionResult<APIGame>> StartGame(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            var result = game.StartGame(body.Player);

            if (!result)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "Could not start the game", true);

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/join
        [HttpPut("{token}/join")]
        public async Task<ActionResult<APIGame>> JoinGame(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            if (game.Player1Token == body.Player)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "You can't join your own game", true);

            var result = game.Join(body.Player);

            if (!result)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "Could not join the game", true);

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // GET api/game/{token}/turn
        [HttpGet("{token}/turn")]
        public async Task<ActionResult<string>> GetCurrentPlayer(string token)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            return game.Moving.ToString();
        }

        // PUT api/game/{token}/move
        [HttpPut("{token}/move")]
        public async Task<ActionResult<APIGame>> SetMove(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            if (game.GetPlayerColor(body.Player) != game.Moving)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "It is not your turn", true);

            var result = game.Move(body.RowMove, body.ColMove);
            if (!result)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "Invalid move", true);

            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/pass
        [HttpPut("{token}/pass")]
        public async Task<ActionResult<APIGame>> Pass(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            if (game.GetPlayerColor(body.Player) != game.Moving)
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "It is not your turn", true);

            var result = game.Pass();
            if (!result)
            {
                return _helper.GetStatusCode(HttpStatusCode.Unauthorized, "You shall not pass, you have valid moves", true);
            }
            
            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }

        // PUT api/game/{token}/surrender
        [HttpPut("{token}/surrender")]
        public async Task<ActionResult<APIGame>> Surrender(string token, [FromBody] APIAction body)
        {
            var game = await iRepository.GetGameAsync(token);
            if (game is null)
                return _helper.GetStatusCode(HttpStatusCode.NotFound, "Game not found", true);

            game.Surrender(body.Player);
            await iRepository.SaveGame(game);
            return APIGame.FromGame(game);
        }
    }
}
