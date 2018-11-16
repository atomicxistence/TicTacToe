using System;
using System.Collections.Generic;

namespace atomic_games.TicTacToe
{
    public class GameStatus
    {
        public List<Player> Players { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public GameState GameState { get; private set; }
        public GameResults GameResults { get; private set; }
        public string[,] EnteredFieldMatrix { get; set; } = new string[3, 3];
        public CurrentCursorPosition CurrentCursor { get; set; }

        public GameStatus(List<Player> players, CurrentCursorPosition cursor)
        {
            Players = players;
            CurrentCursor = cursor;
            GameState = GameState.Start;
        }

        public void SetGameResult(GameResults result)
        {
            GameResults = result;
        }

        public void SetCurrentPlayer(Player currentPlayer)
        {
            CurrentPlayer = currentPlayer;
        }

        public void NextPlayersTurn()
        {
            CurrentPlayer = CurrentPlayer == Players[0] ? Players[1] : Players[0];
            ChangeGameState(GameState.Playing);
        }

        public void ChangeGameState(GameState newState)
        {
            GameState = newState;
        }
    }

}
