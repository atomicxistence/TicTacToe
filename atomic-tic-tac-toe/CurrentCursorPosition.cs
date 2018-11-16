using System;

namespace atomic_games.TicTacToe
{
    public class CurrentCursorPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CurrentCursorPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
