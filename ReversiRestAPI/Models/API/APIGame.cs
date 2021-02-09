﻿using Newtonsoft.Json;
using ReversiRestApi.Enums;
using ReversiRestApi.Models;

namespace ReversiRestAPI.Models.API
{
    public class APIGame
    {
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string Board { get; set; }
        public string MovingPlayer { get; set; }
        public string Moving { get; set; }
        public string Status { get; set; }

        public static APIGame FromGame(Game game)
        {
            return new APIGame
            {
                Player1Token = game.Player1Token,
                Player2Token = game.Player2Token,
                Description = game.Description,
                Token = game.Token,
                Board = JsonConvert.SerializeObject(game.Board),
                MovingPlayer = game.Moving != Color.None ? game.Players[game.Moving] : null,
                Moving = game.Moving != Color.None ? game.Moving.ToString() : null,
                Status = game.Status.ToString(),
            };
        }
    }
}
