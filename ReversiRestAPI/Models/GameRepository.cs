using System.Collections.Generic;
using System.Linq;
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

        public void AddGame(Game game)
        {
            if(GetGame(game.Token) is null)
                Games.Add(game);
        }

        public List<Game> GetGames() => Games;

        public Game GetGame(string gameToken) => Games.FirstOrDefault(x => x.Token == gameToken);
    }
}
