using System;
using System.Collections.Generic;

namespace ChessServer.Classes
{
    public enum GameState
    {
        WaitingSecondPlayer,
        Started,
        Finished
    }



    public class Game
    {
        public DateTime TimeOfCreation { get; set; }
        public string  Name { get; set; }
        public string Code { get; set; }
        public GameState GameState { get; set; }
        public State StateAfterFinish { get; set; }
        public bool IsWhiteTurn { get; set; }
        public List<char[]> Chessboard { get; set; }
        public int Time { get; set; }
        public int Increment { get; set; }
        public string PlayerIdWhite { get; set; }
        public string PlayerIdBlack { get; set; }
        public List<string> Moves { get; set; }
        public int NumberOfMoves { get; set; }

        public Game()
        {
            this.GameState = GameState.WaitingSecondPlayer;
            this.Chessboard = new List<char[]>()
            {
                ("RNBKQBNR").ToCharArray(),
                ("PPPPPPPP").ToCharArray(),
                ("        ").ToCharArray(),
                ("        ").ToCharArray(),
                ("        ").ToCharArray(),
                ("        ").ToCharArray(),
                ("pppppppp").ToCharArray(),
                ("rnbkqbnr").ToCharArray()
            };
            this.IsWhiteTurn = true;
            Random rnd = new Random();
            this.Code = DateTime.Now.Ticks + "!" + rnd.Next(1000);
            this.Moves = new List<string>();
            this.NumberOfMoves = 0;
        }
    }
}
