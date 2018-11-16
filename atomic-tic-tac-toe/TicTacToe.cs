using System;
using System.Collections.Generic;

namespace atomic_games.TicTacToe
{
    class TicTacToe
    {
        static void Main(string[] args)
        {
            // intitialize classes and variables
            var playerOne = new Player("Player One", "X");
            var playerTwo = new Player("Player Two", "O");
            var players = new List<Player>() { playerOne, playerTwo };
            var cursor = new CurrentCursorPosition(0, 2);
            var gameStatus = new GameStatus(players, cursor);
            var board = new Board(gameStatus);
            var validator = new Validator(board, gameStatus);
            var refresh = new Refresh(board, gameStatus);

            Console.CursorVisible = false;

            // game loop
            while (gameStatus.GameState != GameState.GameOver)
            {
                // refresh - draws board with updates and messages
                refresh.Game();
                // waits for keypress (move playermark or enter to place it)
                validator.UserInput();
                // checks for win, tie, or quit - game reset intialized
                if (gameStatus.GameState == GameState.Playing ||
                    gameStatus.GameState == GameState.NextTurn)
                {
                    validator.GameEndCheck();
                }
            }
            refresh.Game();
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
