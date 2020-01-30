using ChessServer;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessClient_Blazor.Hubs
{
    public class GameHub : Hub
    {
        public ChessLogic chessLogic { get; set; }
        public async Task Move()
        {
            await Clients.All.SendAsync("Move");
        }
    }
}
