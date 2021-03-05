using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
    public class PlayerController : Controller
    {
        private ReversiDbContext context;

        public PlayerController(ReversiDbContext context)
        {
            this.context = context;
        }

        [Authorize]
        public Player GetLoggedInPlayer(Controller origin)
        {
            ClaimsPrincipal currentUser = origin.User;
            var guid = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return GetPlayer(guid) ?? CreatePlayer(guid, currentUser.FindFirst(ClaimTypes.Name).Value);
        }

        public Player CreatePlayer(string guid, string name)
        {
            Player player = new Player()
            {
                Guid = guid,
                AmountWon = 0,
                AmountLost = 0,
                AmountDraw = 0,
                Name = name,
            };
            context.Players.Add(player);
            context.SaveChangesAsync();
            return player;
        }

        public Player GetPlayer(string guid)
        {
            return context.Players.FirstOrDefault(x => x.Guid == guid);
        }

    }
}
