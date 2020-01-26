﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private char[,] chessBoard = new char[8, 8]
        {
            {'R' , 'N', 'B', 'K', 'Q', 'B', 'N', 'R'},
            {'P' , 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
            {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ' , ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {'p' , 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
            {'r' , 'n', 'b', 'k', 'q', 'b', 'n', 'r'}
        };

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            StringBuilder sb = new StringBuilder();
            var move = new ChessLogic();
            chessBoard = move.Controller(chessBoard);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    sb.Append(chessBoard[i, j] + " ");
                }
                sb.AppendLine();
            }
            return sb.ToString().TrimEnd();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}