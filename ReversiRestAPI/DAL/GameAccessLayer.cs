using System.Collections.Generic;
using System.Linq;
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

        public string AddGame(Game game)
        {
            game.Token = Helpers.GenerateRandomString(16);
            Context.Add(GameModel.FromGame(game));
            Context.SaveChanges();
            return game.Token;
        }

        public List<Game> GetGames()
        {
            return Games.Select(g => g.ToGame()).ToList();
        }

        public Game? GetGame(string gameToken)
        {
            return Games.FirstOrDefault(x => x.Token == gameToken)?.ToGame() ?? null;
        }

        public IList<Game> GetPlayerGames(string playerToken)
        {
            return Games.Where(x => x.Player1Token == playerToken || x.Player2Token == playerToken)
                .Select(x => x.ToGame()).ToList();
        }

        public void SaveGame(Game game)
        {
            var local = Context.Games.FirstOrDefault(x => x.ID == game.ID);
            Context.Entry(local).State = EntityState.Detached;

            /*local = GameModel.FromGame(game);*/
            /*Context.Entry(local).CurrentValues.SetValues(GameModel.FromGame(game));*/
            local = GameModel.FromGame(game);
            Context.Entry(local).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}
