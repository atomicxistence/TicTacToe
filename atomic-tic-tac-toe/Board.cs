using System;

namespace atomic_games.TicTacToe
{
    public class Board
    {
        GameStatus GameStatus { get; set; }
        string[] Grid { get; } = new string[] { "   |   |   ", "---|---|---", "   |   |   ", "---|---|---", "   |   |   " };

        public Board(GameStatus gameStatus)
        {
            GameStatus = gameStatus;
        }

        public void Print()
        {
            // cursor start position
            //     |   |
            //  ---|---|---
            //     |   |
            //  ---|---|---
            //   * |   |

            int xStart = (Console.WindowWidth / 2) - (Grid[0].Length / 2) + (Grid[0].Length % 2);
            int yStart = (Console.WindowHeight / 2) - (Grid.Length / 2);
            int xOffset = 4;
            int yOffset = 2;

            PrintGrid();
            PrintEnteredFields(xStart, yStart, xOffset, yOffset);
            if (GameStatus.GameState == GameState.Playing)
            {
                PrintCurrentCursorPosition(xStart, yStart, xOffset, yOffset);
            }
        }

        private void PrintGrid()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            int x = 0;
            foreach (var line in Grid)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - (Grid[x].Length / 2),
                                          (Console.WindowHeight / 2) - (Grid.Length / 2 - x));
                Console.WriteLine(line);
                x++;
            }
        }

        private void PrintEnteredFields(int xStart, int yStart, int xOffsetNum, int yOffsetNum)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.SetCursorPosition(xStart + (xOffsetNum * x), yStart + (yOffsetNum * y));
                    if (GameStatus.EnteredFieldMatrix[x, y] != null)
                    {
                        Console.Write(GameStatus.EnteredFieldMatrix[x, y]);
                    }
                }
            }
        }

        private void PrintCurrentCursorPosition(int xStart, int yStart, int xOffsetNum, int yOffsetNum)
        {
            Console.ForegroundColor = GameStatus.CurrentPlayer.Color;
            Console.SetCursorPosition(xStart + (xOffsetNum * GameStatus.CurrentCursor.X),
                                      yStart + (yOffsetNum * GameStatus.CurrentCursor.Y));
            // displays the player's mark if it's an empty space
            if (GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, GameStatus.CurrentCursor.Y] == null)
            {
                Console.Write(GameStatus.CurrentPlayer.PlayerMark);
            }
            // highlights the mark if moving through a marked space
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = GameStatus.CurrentPlayer.Color;
                Console.SetCursorPosition(xStart + (xOffsetNum * GameStatus.CurrentCursor.X),
                                          yStart + (yOffsetNum * GameStatus.CurrentCursor.Y));
                Console.Write(GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, GameStatus.CurrentCursor.Y]);
                Console.ResetColor();
            }
        }
    }
}