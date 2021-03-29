using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestAPI.Models.API
{
    public class APIStats
    {
        public string PlayerToken { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GamesCreated { get; set; }
        public int GamesJoined { get; set; }
    }
}
