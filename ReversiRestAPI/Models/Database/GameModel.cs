using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using ReversiRestApi.Enums;
using ReversiRestAPI.Enums;
using ReversiRestApi.Models;
using ReversiRestAPI.Models.DbModels;

namespace ReversiRestAPI.Models.Database
{
    public class GameModel : DbModel
    {
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        public string? Player2Token { get; set; }
        public Color Moving { get; set; }
        public string Board { get; set; }

        public GameStatus Status { get; set; }
        public int MoveCount { get; set; }

        public string? Winner { get; set; }
        
        public string? WhitePlayer { get; set; }
        public string? BlackPlayer { get; set; }
        public int Size { get; set; }

        public static GameModel FromGame(Game game)
        {
            return new GameModel()
            {
                Token = game.Token,
                Description = game.Description,
                Player1Token = game.Player1Token,
                Player2Token = game.Player2Token ?? "",
                Moving = game.Moving,
                Board = JsonConvert.SerializeObject(game.Board),
                Status = game.Status,
                MoveCount = game.MoveCount,
                Winner = game.Winner,
                WhitePlayer = game.Players.Count == 2 ? game.Players[Color.White] : null,
                BlackPlayer = game.Players.Count == 2 ? game.Players[Color.Black] : null,
                Size = game.Size
            };
        }
        public Game ToGame()
        {
            return new Game()
            {
                ID = this.ID,
                Token = this.Token,
                Description = this.Description,
                Player1Token = this.Player1Token,
                Player2Token = this.Player2Token,
                Moving = this.Moving,
                Board = JsonConvert.DeserializeObject<Color[,]>(this.Board),
                Status = this.Status,
                MoveCount = this.MoveCount,
                Winner = this.Winner,
                Players = new Dictionary<Color, string>
                {
                    {Color.Black, this.BlackPlayer},
                    {Color.White, this.WhitePlayer}
                },
                Size = this.Size
            };
        }
    }
}
