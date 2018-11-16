using System;

namespace atomic_games
{
    public enum GameResults
    {
        Win,
        Lose,
        Tie
    }

    public enum GameState
    {
        Start,
        WinOrTie,
        GameReset,
        Playing,
        NextTurn,
        GameOver
    }
}
