﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChessServer.Classes;

namespace ChessServer.Controllers
{
    [Route("/")]
    public class ChessController : Controller
    {
        // Dirty hack: never use 'static' in Web apps
        private static List<Game> games = new List<Game>();
        private static List<string> playerIDs = new List<string>();

        [HttpGet]
        [Route("games/new")]
        public Game CreateNewGame(string playerId, int mins, int increment)
        {
            Game newGame = new Game();
            newGame.PlayerIdWhite = playerId;
            newGame.Time = mins;
            newGame.Increment = increment;
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
                string move = game.Chessboard[startRow][startCol] + toPos;
                game.Moves.Add(move);
                game.Chessboard[finalRow][finalCol] = game.Chessboard[startRow][startCol];
                game.Chessboard[startRow][startCol] = ' ';
                game.NumberOfMoves++;
            }

            return game;
        }
    }
}