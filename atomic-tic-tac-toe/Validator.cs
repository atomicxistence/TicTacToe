using System;

namespace atomic_games.TicTacToe
{
    public class Validator
    {
        Board Board { get; set; }
        GameStatus GameStatus { get; set; }

        public Validator(Board board, GameStatus gameStatus)
        {
            Board = board;
            GameStatus = gameStatus;
        }

        public void UserInput()
        {
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (IsActivelyPlaying())
                    {
                        ValidateMove(1, 0);
                    }
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (IsActivelyPlaying())
                    {
                        ValidateMove(-1, 0);
                    }
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (IsActivelyPlaying())
                    {
                        ValidateMove(0, -1);
                    }
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (IsActivelyPlaying())
                    {
                        ValidateMove(0, 1);
                    }
                    break;
                case ConsoleKey.Enter:
                    if (GameStatus.GameState == GameState.WinOrTie)
                    {
                        GameStatus.ChangeGameState(GameState.GameReset);
                        break;
                    }

                    if (!IsActivelyPlaying())
                    {
                        GameStatus.ChangeGameState(GameState.Playing);
                        break;
                    }

                    if (EmptySpace())
                    {
                        GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X,
                                                      GameStatus.CurrentCursor.Y]
                                                    = GameStatus.CurrentPlayer.PlayerMark;
                        GameStatus.ChangeGameState(GameState.NextTurn);
                        break;
                    }
                    break;
                case ConsoleKey.Q:
                    GameStatus.ChangeGameState(GameState.GameOver);
                    break;
                default:
                    break;
            }
        }

        private bool IsActivelyPlaying()
        {
            return GameStatus.GameState == GameState.Playing ||
                   GameStatus.GameState == GameState.NextTurn;
        }

        private bool EmptySpace()
        {
            return GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, GameStatus.CurrentCursor.Y] == null;
        }

        private void ValidateMove(int xAdjust, int yAdjust)
        {
            ValidateXMove(xAdjust);
            ValidateYMove(yAdjust);
        }

        private void ValidateXMove(int xAdjust)
        {
            int newX = GameStatus.CurrentCursor.X + xAdjust;

            if (newX >= 0 && newX <= 2)
            {
                GameStatus.CurrentCursor.X = newX;
            }
        }

        private void ValidateYMove(int yAdjust)
        {
            int newY = GameStatus.CurrentCursor.Y + yAdjust;

            if (newY >= 0 && newY <= 2)
            {
                GameStatus.CurrentCursor.Y = newY;
            }
        }

        public void GameEndCheck()
        {
            string lastPlacedMark = GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, GameStatus.CurrentCursor.Y];

            if (lastPlacedMark != null)
            {
                HorizontalWinCheck(lastPlacedMark);
                VerticalWinCheck(lastPlacedMark);
                DiagonalWinCheck(lastPlacedMark);
                TieGameCheck();
            }
        }

        private void TieGameCheck()
        {
            int emptyFields = 0;
            foreach (var field in GameStatus.EnteredFieldMatrix)
            {
                if (field == null)
                {
                    emptyFields++;
                }
            }
            if (emptyFields == 0)
            {
                foreach (var player in GameStatus.Players)
                {
                    player.AddGameResult(GameResults.Tie);
                }
                GameStatus.ChangeGameState(GameState.WinOrTie);
                GameStatus.SetGameResult(GameResults.Tie);
            }
        }

        private void HorizontalWinCheck(string lastPlacedMark)
        {
            string rowLeft = GameStatus.EnteredFieldMatrix[0, GameStatus.CurrentCursor.Y];
            string rowMiddle = GameStatus.EnteredFieldMatrix[1, GameStatus.CurrentCursor.Y];
            string rowRight = GameStatus.EnteredFieldMatrix[2, GameStatus.CurrentCursor.Y];

            if (lastPlacedMark == rowLeft &&
                lastPlacedMark == rowMiddle &&
                lastPlacedMark == rowRight)
            {
                WinnerLoser();
            }
        }

        private void VerticalWinCheck(string lastPlacedMark)
        {
            string columnTop = GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, 2];
            string columnMiddle = GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, 1];
            string columnBottom = GameStatus.EnteredFieldMatrix[GameStatus.CurrentCursor.X, 0];

            if (lastPlacedMark == columnTop &&
                lastPlacedMark == columnMiddle &&
                lastPlacedMark == columnBottom)
            {
                WinnerLoser();
            }

        }

        private void DiagonalWinCheck(string lastPlacedMark)
        {
            string topRight = GameStatus.EnteredFieldMatrix[2, 2];
            string topLeft = GameStatus.EnteredFieldMatrix[0, 2];
            string middle = GameStatus.EnteredFieldMatrix[1, 1];
            string bottomRight = GameStatus.EnteredFieldMatrix[2, 0];
            string bottomLeft = GameStatus.EnteredFieldMatrix[0, 0];

            if (lastPlacedMark == topRight &&
               lastPlacedMark == middle &&
               lastPlacedMark == bottomLeft)
            {
                WinnerLoser();
            }

            if (lastPlacedMark == topLeft &&
               lastPlacedMark == middle &&
               lastPlacedMark == bottomRight)
            {
                WinnerLoser();
            }
        }

        private void WinnerLoser()
        {
            GameStatus.CurrentPlayer.AddGameResult(GameResults.Win);
            foreach (var player in GameStatus.Players)
            {
                if (player != GameStatus.CurrentPlayer)
                {
                    player.AddGameResult(GameResults.Lose);
                }
            }
            GameStatus.ChangeGameState(GameState.WinOrTie);
            GameStatus.SetGameResult(GameResults.Win);
        }
    }
}