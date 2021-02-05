using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ReversiRestApi.Enums;
using ReversiRestAPI.Enums;
using ReversiRestApi.Interfaces;

namespace ReversiRestApi.Models
{
    public class Game : IGame
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public string Player1Token { get; set; }
        public string Player2Token { get; set; }
        public Color[,] Board { get; set; }
        public Color Moving { get; set; }
        public Color OppositeMoving => GetOpposingColor(Moving);
        public int MoveCount { get; set; }
        public GameStatus Status { get; set; }
        public string Winner { get; set; }
        public Dictionary<Color, string> Players { get; set; }


        private int Size { get; } = 8;

        public Game()
        {
            Status = GameStatus.Waiting;
            Board = new Color[Size, Size];
            Players = new Dictionary<Color, string>();

            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    Board[row, col] = Color.None;

                    if ((row == Size / 2 - 1 && col == Size / 2 - 1) || (row == Size / 2 && col == Size / 2))
                        Board[row, col] = Color.White;

                    if ((row == Size / 2 - 1 && col == Size / 2) || (row == Size / 2 && col == Size / 2 - 1))
                        Board[row, col] = Color.Black;
                }
            }
        }

        public Color GetOpposingColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public Color GetPlayerColor(string player)
        {
            return Players.First(x => x.Value == player).Key;
        }

        public void Print()
        {
            RunForAllCells((row, col) =>
            {
                Console.Write($"{Board[row, col],-5} ");
                if (col == Size - 1)
                    Console.WriteLine();
                return false;
            });
        }

        public bool Pass()
        {
            var possible = false;

            foreach (var emptyCell in GetEmptyCells())
            {
                if (IsPossible(emptyCell.Row, emptyCell.Col))
                    possible = true;
            }

            if (!possible)
            {
                SwapMoving();
                return true;
            }

            return false;
        }

        public Cell[] GetEmptyCells()
        {
            var cells = new List<Cell>();

            RunForAllCells((row, col) =>
            {
                var color = Board[row, col];
                if (color == Color.None)
                    cells.Add(new Cell(row, col, color));

                return false;
            });

            return cells.ToArray();
        }

        public void SwapMoving()
        {
            Moving = OppositeMoving;
        }

        public bool Finished()
        {
            if (MoveCount >= 60)
                return true;

            return !PlayerHasPossibleMoves(Moving) && !PlayerHasPossibleMoves(OppositeMoving);
        }

        public bool PlayerHasPossibleMoves(Color player)
        {
            var possibleMoves = false;
            RunForAllCells((row, col) =>
            {
                if (IsPossible(row, col))
                    possibleMoves = true;

                return false;
            });

            return possibleMoves;
        }

        public bool RunForAllCells(Func<int,int,bool> function)
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    function(row, col);
                }
            }

            return false;
        }

        public Color WinningColor()
        {
            var scoreCount = new Dictionary<Color, int>() 
            {
                { Color.None, 0 },
                { Color.White, 0 },
                { Color.Black, 0 }
            };

            RunForAllCells((row, col) =>
            {
                scoreCount[Board[row, col]]++;
                return false;
            });

            if (scoreCount[Color.White] == scoreCount[Color.Black])
                return Color.None;

            return scoreCount[Color.White] > scoreCount[Color.Black] ? Color.White : Color.Black;
        }

        public Vector2[] GetPossibleDirections(int rowMove, int colMove)
        {
            var vectors = new List<Vector2>();

            foreach (var direction in new[]
            {
                new Vector2(0, -1), new Vector2(1, -1), new Vector2(1, 0), new Vector2(1, 1),
                new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, -1)
            })
            {
                var startRow = rowMove;
                var startCol = colMove;
                var foundOpp = false;
                var valid = false;

                while (OnBoard(startRow, startCol))
                {
                    startRow += (int)direction.X;
                    startCol += (int)direction.Y;

                    var cell = GetCell(startRow, startCol);

                    if (cell == Color.None && (startRow != rowMove || startCol != colMove))
                        break;

                    if (cell == OppositeMoving)
                        foundOpp = true;

                    if (foundOpp && cell == Moving)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                    vectors.Add(direction);
            }

            return vectors.ToArray();
        }

        public bool IsPossible(int rowMove, int colMove)
        {
            if (!(rowMove >= 0 && rowMove < Size && colMove >= 0 && colMove < Size))
                return false;

            if (Board[rowMove, colMove] != Color.None)
                return false;

            return GetPossibleDirections(rowMove, colMove).Length >= 1;
        }

        public void FlipColorsFromDirections(int row, int col, Vector2[] directions)
        {
            foreach (var direction in directions)
            {
                var startRow = row;
                var startCol = col;

                while (OnBoard(startRow, startCol))
                {
                    startRow += (int) direction.X;
                    startCol += (int) direction.Y;

                    var cell = GetCell(startRow, startCol);

                    if (cell == Moving && (startRow != row || startCol != col))
                        break;
                    
                    Board[startRow, startCol] = Moving;
                }
            }
        }

        public bool Move(int rowMove, int colMove)
        {
            if (!IsPossible(rowMove, colMove))
                return false;

            SetCell(rowMove, colMove, Moving);
            FlipColorsFromDirections(rowMove, colMove, GetPossibleDirections(rowMove, colMove));
            SwapMoving();
            MoveCount++;
            return true;
        }

        public Color GetCell(int row, int col)
        {
            return OnBoard(row, col) ? Board[row, col] : Color.None;
        }

        public void SetCell(int row, int col, Color color)
        {
            if (OnBoard(row, col) && GetCell(row, col) == Color.None)
                Board[row, col] = color;
        }

        public bool OnBoard(int row, int col)
        {
            return (row >= 0 && row < Size && col >= 0 && col < Size);
        }

        public bool Join(string joiningPlayer)
        {
            if (Player2Token is null)
            {
                Player2Token = joiningPlayer;
                Status = GameStatus.Starting;
                return true;
            }

            return false;
        }

        public void AssignColors()
        {
            if (Player1Token is null || Player2Token is null)
                return;

            var colorOne = new Random().Next(0, 1) == 1 ? Color.White : Color.Black;

            Players[colorOne]                   = Player1Token;
            Players[GetOpposingColor(colorOne)] = Player2Token;
        }

        public bool StartGame(string startingPlayer)
        {
            if (Player2Token is null && Status != GameStatus.Starting)
                return false;

            AssignColors();
            Moving = GetPlayerColor(startingPlayer);

            Status = GameStatus.Running;
            return true;
        }

        public void FinishGame(string winner)
        {
            Winner = winner;
            Status = GameStatus.Finished;
        }

        public void Surrender(string surrenderer)
        {
            var winner = surrenderer == Player1Token ? Player2Token : Player1Token;
            FinishGame(winner);
        }
    }
}
