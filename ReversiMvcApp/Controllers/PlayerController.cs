using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
    public class PlayerController
    {
        private ReversiDbContext context;

        public PlayerController(ReversiDbContext context)
        {
            this.context = context;
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
