using System.Collections.Generic;
using ReversiRestApi.Models;

namespace ReversiRestAPI.Interfaces
{
    public interface IGameRepository
    {
        string AddGame(Game game);
        List<Game> GetGames();
        Game? GetGame(string gameToken);
        IList<Game> GetPlayerGames(string playerToken);
    }
}
