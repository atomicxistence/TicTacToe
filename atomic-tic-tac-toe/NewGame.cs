using System;
namespace atomic_games.TicTacToe
{
    public class NewGame
    {
        GameStatus GameStatus { get; set; }

        public NewGame(GameStatus gameStatus)
        {
            GameStatus = gameStatus;
        }

        public void ResetGame()
        {
            StartingPlayer();
            ResetBoard();
        }

        // choose a random player to start playing
        private void StartingPlayer()
        {
            var rng = new Random();
            int startingPlayerIndex = rng.Next(GameStatus.Players.Count);
            GameStatus.SetCurrentPlayer(GameStatus.Players[startingPlayerIndex]);
        }

        private void ResetBoard()
        {
            Array.Clear(GameStatus.EnteredFieldMatrix, 0, GameStatus.EnteredFieldMatrix.Length);
        }
    }
}
