using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            return GetPlayerOrCreate(origin.User);
        }

        public Player GetLoggedInPlayer(ClaimsPrincipal user)
        {
            return GetPlayerOrCreate(user);
        }

        public Player GetPlayerOrCreate(ClaimsPrincipal user)
        {
            var guid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var name = user.FindFirst(ClaimTypes.Name).Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            return GetPlayer(guid) ?? CreatePlayer(guid, name, email ?? name);
        }

        public bool IsLoggedIn(Controller origin)
        {
            ClaimsPrincipal currentUser = origin.User;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier) != null;
        }

        public Player CreatePlayer(string guid, string name, string email)
        {
            Player player = new Player()
            {
                Guid = guid,
                AmountWon = 0,
                AmountLost = 0,
                AmountDraw = 0,
                Name = name,
                Email = email,
            };
            context.Players.Add(player);
            context.SaveChangesAsync();
            return player;
        }

        public async Task SavePlayer(Player player)
        {
            var current = await context.Players.FirstOrDefaultAsync(x => x.Email == player.Email);
            context.Entry(current).State = EntityState.Detached;
            
            current = player;
            context.Entry(current).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public Player GetPlayer(string guid)
        {
            return context.Players.FirstOrDefault(x => x.Guid == guid);
        }
    }
}
