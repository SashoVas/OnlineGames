using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicTacToeHub:Hub
    {
        private readonly ITicTacToeService ticTacToeService;

        public TicTacToeHub(ITicTacToeService ticTacToeService)
        {
            this.ticTacToeService = ticTacToeService;
        }

        public async Task TestAll()
        {
            await this.Clients.All.SendAsync("TestTicTacToeHub",5);
        }
        public async Task MakeMoveAI(string boardString,int currentPlayer)
        {
            var output =await this.ticTacToeService.MakeMove(boardString,currentPlayer);
            await this.Clients.Caller.SendAsync("OponentMove", output);
        }
        public async Task AddToGroup(string groupName)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.ticTacToeService.SetRoomName(userId,groupName);
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }
        public async Task MakeMoveOponent(int row,int col)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.Clients.OthersInGroup(await this.ticTacToeService.GetRoomName(userId)).SendAsync("OponentMove",
                new BoardCoordinates 
                { 
                    Row = row,
                    Col=col
                });
            
        }
    }
}
