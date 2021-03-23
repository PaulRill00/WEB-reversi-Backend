using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReversiRestAPI.Interfaces;
using ReversiRestApi.Models;

namespace ReversiRestAPI.Models
{
    public class GameRepository : IGameRepository
    {
        private List<Game> Games { get; set; }

        public GameRepository()
        {
            Games = new List<Game>
            {
                new Game
                {
                    Token = "aaabbb",
                    Player1Token = "abcdef",
                    Description = "Potje snel reveri, dus niet lang nadenken",
                },
                new Game
                {
                    Token = "cccddd",
                    Player1Token = "ghijkl",
                    Player2Token = "mnopqr",
                    Description = "Ik zoek eenn gevorderde tegenspeler"
                },
                new Game
                {
                    Token = "eeefff",
                    Player1Token = "stuvwx",
                    Description = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander"
                }
            };
        }

        public Task<string> AddGame(Game game)
        {
            game.Token = Helpers.GenerateRandomString(16);
            Games.Add(game);
            return Task.Run(() => game.Token);
        }

        public Task<List<Game>> GetGames() => Task.Run(() => Games);

        public Task<Game> GetGameAsync(string gameToken) => Task.Run(() => Games.FirstOrDefault(x => x.Token == gameToken));

        public Task<List<Game>> GetPlayerGames(string playerToken) =>
            Task.Run(() => Games.FindAll(x => x.Player1Token == playerToken || x.Player2Token == playerToken));

        public async Task SaveGame(Game game)
        {
            await Task.Run(() => { });
        }
    }
}
