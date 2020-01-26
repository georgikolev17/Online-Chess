using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTest.Controllers
{
    [Route("/")]
    public class ChessController : Controller
    {
        // Dirty hack: never use 'static' in Web apps
        private static List<Game> games = new List<Game>();
        private static List<string> playerIDs = new List<string>();

        [HttpGet]
        [Route("games/new")]
        public Game CreateNewGame(string playerId)
        {
            Game newGame = new Game();
            newGame.PlayerIdWhite = playerId;
            games.Add(newGame);
            return newGame;
        }

        [HttpGet]
        [Route("games/list")]
        public ActionResult<ICollection<Game>> ListAllGames()
        {
            return games;
        }

        [HttpGet]
        [Route("games/join")]
        public ActionResult<Game> JoinGame(string playerId, string gameCode)
        {
            Game game = games.Where(g => g.Code == gameCode).FirstOrDefault();
            if (game == null)
            {
                return NotFound(new { ErrorMsg = "Invalid game code: " + gameCode });
            }

            game.GameState = GameState.Started;
            game.PlayerIdBlack = playerId;
            return game;
        }

        [HttpGet]
        [Route("games/state")]
        public ActionResult<Game> CheckGameState(string gameCode)
        {
            Game game = games.Where(g => g.Code == gameCode).FirstOrDefault();
            if (game == null)
            {
                return NotFound(new { ErrorMsg = "Invalid game code: " + gameCode });
            }

            return game;
        }

        [HttpGet]
        [Route("games/move")]
        public ActionResult<Game> Move(string playerId, string gameCode, string fromPos, string toPos)
        {
            ChessLogic chessLogic = new ChessLogic();

            Game game = games.Where(g => g.Code == gameCode).FirstOrDefault();
            if (game == null)
            {
                return NotFound(new { ErrorMsg = "Invalid game code: " + gameCode });
            }

            if(game.IsWhiteTurn && game.PlayerIdWhite != playerId)
            {
                return game;
            }
            if(!game.IsWhiteTurn && game.PlayerIdBlack != playerId)
            {
                return game;
            }
            if(chessLogic.Controller(fromPos, toPos) != State.ImpossibleMove)
            {
                game.IsWhiteTurn = !game.IsWhiteTurn;
                int startRow = int.Parse(fromPos[1].ToString()) - 1;
                int startCol = 7 - ((int)fromPos[0] - 97);
                int finalRow = int.Parse(toPos[1].ToString()) - 1;
                int finalCol = 7 - ((int)toPos[0] - 97);
                game.Chessboard[finalRow][finalCol] = game.Chessboard[startRow][startCol];
                game.Chessboard[startRow][startCol] = ' ';
            }
            Console.WriteLine(chessLogic.Controller(fromPos, toPos));
            return game;
        }
    }
}
