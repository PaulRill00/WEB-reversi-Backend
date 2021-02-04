using System.Collections.Generic;
using ReversiRestApi.Models;

namespace ReversiRestAPI.Interfaces
{
    public interface IGameRepository
    {
        void AddGame(Game game);
        List<Game> GetGames();
        Game GetGame(string gameToken);
    }
}
