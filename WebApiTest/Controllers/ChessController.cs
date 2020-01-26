using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTest.Controllers
{
    [Route("api/chess")]
    public class ChessController : Controller
    {
        // Dirty hack: never use 'static' in Web apps
        private static List<Game> games = new List<Game>();

        [HttpGet]
        [Route("games/new")]
        public Game CreateNewGame()
        {
            Game newGame = new Game();
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
        public ActionResult<Game> JoinGame(string gameCode)
        {
            Game game = games.Where(g => g.Code == gameCode).FirstOrDefault();
            if (game == null)
            {
                return NotFound(new { ErrorMsg = "Invalid game code: " + gameCode });
            }

            game.GameState = GameState.Started;
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

    }
}
