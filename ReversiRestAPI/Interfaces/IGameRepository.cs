using System.Collections.Generic;
using System.Threading.Tasks;
using ReversiRestApi.Models;

namespace ReversiRestAPI.Interfaces
{
    public interface IGameRepository
    {
        Task<string> AddGame(Game game);
        Task<List<Game>> GetGames();
        Task<Game> GetGameAsync(string gameToken);
        Task<List<Game>> GetPlayerGames(string playerToken);
        Task SaveGame(Game game);
    }
}
