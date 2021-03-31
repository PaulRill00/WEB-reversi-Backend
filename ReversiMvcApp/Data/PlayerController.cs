using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
    public class PlayerController : Controller
    {
        private ReversiDbContext context;
        private readonly UserManager<IdentityUser> _userController;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PlayerController(ReversiDbContext context, UserManager<IdentityUser> userController, RoleManager<IdentityRole> roleController)
        {
            this.context = context;
            _userController = userController;
            _roleManager = roleController;
        }

        [Authorize]
        public async Task<Player> GetLoggedInPlayer(Controller origin)
        {
            return await GetPlayerOrCreate(origin.User);
        }

        public async Task<Player> GetLoggedInPlayer(ClaimsPrincipal user)
        {
            return await GetPlayerOrCreate(user);
        }

        public async Task<Player> GetPlayerOrCreate(ClaimsPrincipal user)
        {
            var guid = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var name = user.FindFirst(ClaimTypes.Name).Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            return GetPlayer(guid) ?? await CreatePlayer(guid, name, email ?? name);
        }

        public bool IsLoggedIn(Controller origin)
        {
            ClaimsPrincipal currentUser = origin.User;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier) != null;
        }

        public async Task<Player> CreatePlayer(string guid, string name, string email)
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
            await context.SaveChangesAsync();

            var user = await _userController.FindByIdAsync(player.Guid);


            var role = await _roleManager.FindByNameAsync("User");


            await _userController.AddToRoleAsync(user, "User");

            return player;
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await context.Players.ToListAsync();
        }

        public async Task SavePlayer(Player player)
        {
            var current = await context.Players.FirstOrDefaultAsync(x => x.Guid == player.Guid);
            context.Entry(current).State = EntityState.Detached;
            
            current = player;
            context.Entry(current).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public Player GetPlayer(string guid)
        {
            return context.Players.FirstOrDefault(x => x.Guid == guid);
        }

        public async Task DeletePlayer(string guid)
        {
            var player = context.Players.FirstOrDefault(x => x.Guid == guid);

            if (player != null)
            {
                context.Players.Remove(player);
                await context.SaveChangesAsync();
            }
        }
    }
}
