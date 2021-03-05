namespace ReversiMvcApp.Models
{
    public class Game
    {
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        public string? Player2Token { get; set; }
        public string Board { get; set; }
    }
}
