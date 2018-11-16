using System;

namespace atomic_games.TicTacToe
{
    public class Refresh
    {
        Board Board { get; set; }
        GameStatus GameStatus { get; set; }

        readonly int yOffset = 8;

        public Refresh(Board board, GameStatus gameStatus)
        {
            Board = board;
            GameStatus = gameStatus;
        }

        public void Game()
        {
            string message = "";
            string instructions = "";

            switch (GameStatus.GameState)
            {
                case GameState.Start:
                    // TODO: make code to set the player's names?
                case GameState.GameReset:
                    var newGame = new NewGame(GameStatus);
                    newGame.ResetGame();
                    message = "Welcome to atomic-tic-tac-toe!";
                    instructions = "Press ENTER to continue. Press Q to quit the game";
                    break;
                case GameState.Playing:
                    message = $"{GameStatus.CurrentPlayer.Title}'s turn.  Place your {GameStatus.CurrentPlayer.PlayerMark}.";
                    instructions = "Use the arrow keys or WASD to move your mark. " +
                                    $"Press ENTER to place your {GameStatus.CurrentPlayer.PlayerMark}.";
                    break;
                case GameState.NextTurn:
                    GameStatus.NextPlayersTurn();
                    message = $"{GameStatus.CurrentPlayer.Title}'s turn.  Place your {GameStatus.CurrentPlayer.PlayerMark}.";
                    instructions = "Use the arrow keys or WASD to move your mark. " +
                                    $"Press ENTER to place your {GameStatus.CurrentPlayer.PlayerMark}.";
                    break;
                case GameState.WinOrTie:
                    switch (GameStatus.GameResults)
                    {
                        case GameResults.Win:
                            message = $"{GameStatus.CurrentPlayer.Title} has won!";
                            break;
                        case GameResults.Tie:
                            message = "It's a tie game";
                            break;
                    }
                    instructions = "Press ENTER to play again. Press Q to quit the game";
                    break;
                case GameState.GameOver:
                    message = "GAME OVER";
                    instructions = "Press ENTER to quit";
                    break;
            }
            RefreshScreen(message, instructions);
        }

        private void RefreshScreen(string message, string instructions)
        {
            Console.Clear();
            Board.Print();
            PrintMessage(message);
            PrintInstructions(instructions);
            PrintPlayerInfo();
        }

        private void PrintInstructions(string instructions)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition((Console.WindowWidth / 2) - (instructions.Length / 2), (Console.WindowHeight / 2) + yOffset);
            Console.Write(instructions);
            Console.ResetColor();
        }

        private void PrintMessage(string message)
        {
            Console.ForegroundColor = 
                GameStatus.GameState == GameState.Playing ||
                GameStatus.GameState == GameState.NextTurn ||
                GameStatus.GameState == GameState.WinOrTie
                ? GameStatus.CurrentPlayer.Color
                : ConsoleColor.White;

            Console.SetCursorPosition((Console.WindowWidth / 2) - (message.Length / 2), (Console.WindowHeight / 2) - yOffset);
            Console.Write(message);
            Console.ResetColor();
        }

        private void PrintPlayerInfo()
        {
            PrintPlayerStats(GameStatus.Players[0]);
            PrintPlayerStats(GameStatus.Players[1]);
            if (GameStatus.GameState == GameState.Playing ||
                GameStatus.GameState == GameState.NextTurn)
            {
                HighlightCurrentPlayer();
            }
        }

        private void PrintPlayerStats(Player player)
        {
            Console.ForegroundColor = player.Color;
            Console.SetCursorPosition((Console.WindowWidth / 2) + player.InfoXOffset, (Console.WindowHeight / 2) - 2);
            Console.Write(player.Title);
            Console.SetCursorPosition((Console.WindowWidth / 2) + player.InfoXOffset, (Console.WindowHeight / 2));
            Console.Write($"Wins: {player.Wins}");
            Console.SetCursorPosition((Console.WindowWidth / 2) + player.InfoXOffset, (Console.WindowHeight / 2) + 1);
            Console.Write($"Losses: {player.Losses}");
            Console.SetCursorPosition((Console.WindowWidth / 2) + player.InfoXOffset, (Console.WindowHeight / 2) + 2);
            Console.Write($"Ties: {player.Ties}");
            Console.ResetColor();
        }

        private void HighlightCurrentPlayer()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = GameStatus.CurrentPlayer.Color;
            Console.SetCursorPosition((Console.WindowWidth / 2) + GameStatus.CurrentPlayer.InfoXOffset, 
                                      (Console.WindowHeight / 2) - 2);
            Console.Write(GameStatus.CurrentPlayer.Title);
            Console.ResetColor();
        }
    }
}
