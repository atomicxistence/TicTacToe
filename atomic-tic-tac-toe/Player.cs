using System;

namespace atomic_games.TicTacToe
{
    public class Player
    {
        public string Title { get; private set; }
        public string PlayerMark { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int Ties { get; private set; }
        public ConsoleColor Color { get; private set; }
        public int InfoXOffset { get; private set; }

        public Player(string title, string playerMark)
        {
            Title = title;
            PlayerMark = playerMark;
            switch (title)
            {
                case "Player One":
                    InfoXOffset = -26;
                    Color = ConsoleColor.DarkGreen;
                    break;
                case "Player Two":
                    InfoXOffset = 20;
                    Color = ConsoleColor.DarkYellow;
                    break;
            }
        }

        public void AddGameResult(GameResults result)
        {
            switch (result)
            {
                case GameResults.Win:
                    Wins += 1;
                    break;
                case GameResults.Lose:
                    Losses += 1;
                    break;
                case GameResults.Tie:
                    Ties += 1;
                    break;
            }
        }
    }
}
