using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Classes
{
    public enum GameState
    {
        WaitingSecondPlayer,
        Started,
        Finished
    }

    public class Game
    {
        public string Code { get; set; }
        public GameState GameState { get; set; }
        public bool IsWhiteTurn { get; set; }
        public string Chessoard { get; set; }

        public Game()
        {
            this.GameState = GameState.WaitingSecondPlayer;
            //this.Chessboard = ...
            this.IsWhiteTurn = true;
            Random rnd = new Random();
            this.Code = DateTime.Now.Ticks + "!" + rnd.Next(1000); 
        }
    }
}
