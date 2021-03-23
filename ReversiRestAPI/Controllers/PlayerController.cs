using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/player/{player}/wins
        [HttpGet("{player}/wins")]
        public async Task<ActionResult<int>> GetPlayerWins(string player)
        {
            var games = await iRepository.GetPlayerGames(player);
            return games.Count(x => x.Winner == player);
        }
    }
}
