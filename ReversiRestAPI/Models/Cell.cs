using ReversiRestApi.Enums;

namespace ReversiRestApi.Models
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public Color Color { get; set; }

        public Cell(int row, int col, Color color)
        {
            Row = row;
            Col = col;
            Color = color;
        }
    }
}
