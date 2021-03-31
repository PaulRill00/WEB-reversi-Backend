using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestAPI.Enums;
using ReversiRestAPI.Interfaces;
using ReversiRestAPI.Models.API;

namespace ReversiRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IGameRepository iRepository;

        public PlayerController(IGameRepository iRepository)
        {
            this.iRepository = iRepository;
        }

        // GET api/player/{player}/games
        [HttpGet("{player}/games")]
        public async Task<ActionResult<IEnumerable<APIGame>>> GetGamesByPlayerToken(string player)
        {
            var games = await iRepository.GetPlayerGames(player);
            return games.Select(APIGame.FromGame).ToList();
        }

        // GET api/player/{player}/stats
        [HttpGet("{player}/stats")]
        public async Task<ActionResult<APIStats>> GetPlayerStats(string player)
        {
            var games = await iRepository.GetPlayerGames(player);

            var wins = games.Count(x => x.Status == GameStatus.Finished && x.Winner == player);
            var draws = games.Count(x => x.Status == GameStatus.Finished && x.Winner == "");
            var losses = games.Count(x => x.Status == GameStatus.Finished) - (wins + draws);

            return new APIStats()
            {
                PlayerToken = player,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                GamesCreated = games.Count(x =>  x.Player1Token == player),
                GamesJoined = games.Count(x => x.Player2Token == player),
            };
        }

        // GET api/player/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<string>>> GetPlayers()
        {
            var games = await iRepository.GetGames();
            var player1Tokens = games.Select(x => x.Player1Token).Distinct();
            var player2Tokens = games.Where(x => x.Player2Token != null).Select(x => x.Player2Token).Distinct();

            return player1Tokens.Union(player2Tokens).ToArray();
        }

        // GET api/player/stats
        [HttpGet("stats")]
        public async Task<ActionResult<IEnumerable<APIStats>>> GetPlayersStats()
        {
            var players = (await GetPlayers()).Value;

            var stats = new List<APIStats>();

            foreach (string player in players)
            {
                if (player != "")
                {
                    stats.Add((await GetPlayerStats(player)).Value);
                }
            }

            return stats.ToArray();
        }
    }
}
