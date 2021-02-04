using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{player}/games")]
        public ActionResult<IEnumerable<APIGame>> GetGamesByPlayerToken(string player) =>
            iRepository.GetPlayerGames(player).Select(APIGame.FromGame).ToList();
    }
}
