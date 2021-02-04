using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ReversiRestAPI.Interfaces;

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

        // GET api/Game
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetGameDescriptionsOfGamesWithWaitingPlayers() =>
            iRepository.GetGames().FindAll(x => x.Player2Token is null).Select(x => x.Description).ToList();

    }
}
