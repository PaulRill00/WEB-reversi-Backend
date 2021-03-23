using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ReversiRestApi.Enums;
using ReversiRestAPI.Enums;
using ReversiRestApi.Interfaces;
using ReversiRestAPI.Models.API;

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
        public int MoveCount { get; set; } = 4;
        public GameStatus Status { get; set; }
        public string? Winner { get; set; }
        public Dictionary<Color, string> Players { get; set; }


        public int Size { get; set;  } = 8;

        /**
         * Initialize the game, and fill the board with values
         */
        public Game()
        {
            Status = GameStatus.Waiting;
            Board = new Color[Size, Size];
            Players = new Dictionary<Color, string>();
            Moving = Color.None;

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

        /**
         * Get the other color
         */
        public Color GetOpposingColor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public Color GetPlayerColor(string player)
        {
            return Players.First(x => x.Value == player).Key;
        }

        /**
         * Debug print the board to the console
         */
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

        /**
         * Pass on move
         *
         * @return The player has passed
         */
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

        /**
         * Get all empty cells on the board
         *
         * @return Cell[] all empty cells
         */
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

        /**
         * Swap the moving player
         */
        public void SwapMoving()
        {
            Moving = OppositeMoving;
        }

        /**
         * See if the game has finished
         */
        public bool Finished()
        {
            if (MoveCount >= 60)
                return true;

            return !PlayerHasPossibleMoves(Moving) && !PlayerHasPossibleMoves(OppositeMoving);
        }

        /**
         * Check if a player has possible moves
         *
         * @param Color player The player to check for
         *
         * @return The player can move
         */
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

        /**
         * Run a task for all cells
         *
         * @param Func<int,int,bool> function The function to run for all cells
         */
        public void RunForAllCells(Func<int,int,bool> function)
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    function(row, col);
                }
            }
        }

        /**
         * Get the winning color based on amount of fields
         *
         * @return Color The opposing color
         */
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

        /**
         * Calculate all valid directions starting at a single cell
         *
         * @return Vector2[] of valid directions
         */
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

        /**
         * Check if the move is valid
         *
         * @param int rowMove The row to start on
         * @param int colMove The col to start on
         */
        public bool IsPossible(int rowMove, int colMove)
        {
            if (!(rowMove >= 0 && rowMove < Size && colMove >= 0 && colMove < Size))
                return false;

            if (Board[rowMove, colMove] != Color.None)
                return false;

            return GetPossibleDirections(rowMove, colMove).Length >= 1;
        }

        /**
         * Flip all colors to the moving player within the directions
         *
         * @param int row   The row to start on
         * @param int col   The col to start on
         * @param Vector2[] directions
         */
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

        /**
         * Set a move on the row and col
         *
         * @param int rowMove The row to start at
         * @param int colMove The col to tart at
         *
         * @return If the move was valid
         */
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

        /**
         * Get a colored cell based on row and col
         *
         * @param int row The row to get at
         * @param int col The col to get at
         *
         * @return The color on the row and col
         */
        public Color GetCell(int row, int col)
        {
            return OnBoard(row, col) ? Board[row, col] : Color.None;
        }

        /**
         * Set the cell and check if the board contains the field
         *
         * @param int row       The row to set at
         * @param int col       The col to set at
         * @param Color color   The color to write to the cell
         */
        public void SetCell(int row, int col, Color color)
        {
            if (OnBoard(row, col) && GetCell(row, col) == Color.None)
                Board[row, col] = color;
        }

        /**
         * Check if the cell is on the board
         *
         * @param int row Row to check
         * @param int col Col to check
         *
         * @return If the cell is on the board
         */
        public bool OnBoard(int row, int col)
        {
            return (row >= 0 && row < Size && col >= 0 && col < Size);
        }

        public bool Join(string joiningPlayer)
        {
            if (!(Player2Token is null) && Player1Token != joiningPlayer && joiningPlayer != "")
            {
                Player2Token = joiningPlayer;
                Status = GameStatus.Starting;
                return true;
            }

            return false;
        }

        /**
         * Assign the colors to the players
         */
        public void AssignColors()
        {
            if (Player1Token is null || Player2Token is null)
                return;

            var colorOne = new Random().Next(0, 1) == 1 ? Color.White : Color.Black;

            Players[colorOne]                   = Player1Token;
            Players[GetOpposingColor(colorOne)] = Player2Token;
        }

        /**
         * Start the game with a starting player
         *
         * @param string startPlayer The token of the starting player
         *
         * @return The game has started
         */
        public bool StartGame(string startingPlayer)
        {
            if (Player2Token is null && Status != GameStatus.Starting)
                return false;

            AssignColors();
            Moving = GetPlayerColor(startingPlayer);

            Status = GameStatus.Running;
            return true;
        }

        /**
         * Finish the game with a winning player
         *
         * @param string winner Winning player string
         */
        public void FinishGame(string winner)
        {
            Winner = winner;
            Status = GameStatus.Finished;
        }

        /**
         * Surrender the game with a surrendering player token
         *
         * @param string surrenderer The player token of the person who gave up
         */
        public void Surrender(string surrenderer)
        {
            var winner = surrenderer == Player1Token ? Player2Token : Player1Token;
            FinishGame(winner);
        }

        public List<GameAction> GetGameActions()
        {
            List<GameAction> list = new List<GameAction>();
            if (Status == GameStatus.Starting)
            {
                list.Add(new GameAction
                {
                    Label = "Start Game",
                    Action = "start"
                });
            }

            if (Status == GameStatus.Running)
            {
                list.Add(new GameAction
                {
                    Label = "Surrender Game",
                    Action = "surrender"
                });
            }

            return list;
        }
    }
}
