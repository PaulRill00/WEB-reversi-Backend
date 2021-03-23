using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReversiRestAPI.Interfaces;
using ReversiRestApi.Models;
using ReversiRestAPI.Models;
using ReversiRestAPI.Models.Database;

namespace ReversiRestAPI.DAL
{
    public class GameAccessLayer : IGameRepository
    {
        public DatabaseContext Context { get; }
        private DbSet<GameModel> Games { get; }

        public GameAccessLayer(DatabaseContext context)
        {
            Context = context;
            Games = Context.Games;
        }

        public async Task<string> AddGame(Game game)
        {
            game.Token = Helpers.GenerateRandomString(16);
            Context.Add(GameModel.FromGame(game));
            await Context.SaveChangesAsync();
            return game.Token;
        }

        public async Task<List<Game>> GetGames()
        {
            return await Games.Select(g => g.ToGame()).ToListAsync();
        }

        public async Task<Game> GetGameAsync(string gameToken)
        {
            var game = await Games.FirstOrDefaultAsync(x => x.Token == gameToken);
            return game.ToGame();
        }

        public async Task<List<Game>> GetPlayerGames(string playerToken)
        {
            return await Games.Where(x => x.Player1Token == playerToken || x.Player2Token == playerToken)
                .Select(x => x.ToGame()).ToListAsync();
        }

        public async Task SaveGame(Game game)
        {
            var local = Context.Games.FirstOrDefault(x => x.ID == game.ID);
            Context.Entry(local).State = EntityState.Detached;

            /*local = GameModel.FromGame(game);*/
            /*Context.Entry(local).CurrentValues.SetValues(GameModel.FromGame(game));*/
            local = GameModel.FromGame(game);
            Context.Entry(local).State = EntityState.Modified;

            await Context.SaveChangesAsync();
        }
    }
}
