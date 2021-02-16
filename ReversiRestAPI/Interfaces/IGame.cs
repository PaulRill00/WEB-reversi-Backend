using ReversiRestApi.Enums;
using ReversiRestAPI.Enums;

namespace ReversiRestApi.Interfaces
{
    public interface IGame
    {
        int ID { get; set; }
        string Description { get; set; }
        string Token { get; set; }
        string Player1Token { get; set; }
        string Player2Token { get; set; }
        Color[,] Board { get; set; }
        Color Moving { get; set; }
        public int MoveCount { get; set; }
        public GameStatus Status { get; set; }
        public string? Winner { get; set; }

        bool Pass();
        bool Finished();
        Color WinningColor();
        bool IsPossible(int rowMove, int colMove);
        bool Move(int rowMove, int colMove);

    }
}
