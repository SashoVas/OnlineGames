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
        private readonly IRoomService roomService;
        public TicTacToeHub(ITicTacToeService ticTacToeService, IRoomService roomService)
        {
            this.ticTacToeService = ticTacToeService;
            this.roomService = roomService;
        }

        public async Task TestAll()
        {
            await this.Clients.All.SendAsync("TestTicTacToeHub",5);
        }
        public async Task MakeMoveAI(int row,int col)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Skip update board if the ai is first
            if (row!=-1 && col!=-1)
            {
                await roomService.UpdateBoard(userId, row, col);
            }
            var boardString = await roomService.GetUserRoom(userId);
            var currentPlayer = await roomService.GetTurn(userId);
            var output =await this.ticTacToeService.MakeMove(boardString,currentPlayer);
            await roomService.UpdateBoardAI(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier),output.Row,output.Col);
            await this.Clients.Caller.SendAsync("OponentMove", output);
        }
        public async Task AddToGroup(string groupName)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (groupName==null)
            {
                //Here if the oponent is ai and we dont want our room id to be exposed
                groupName =await this.roomService.CreateTicTacToeRoom(this.Context.User.Identity.Name);
            }
            await this.ticTacToeService.SetRoomName(userId,groupName);
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }
        public async Task MakeMoveOponent(int row,int col)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await roomService.UpdateBoard(userId,row,col);
            await this.Clients.OthersInGroup(await this.ticTacToeService.GetRoomName(userId)).SendAsync("OponentMove",
                new BoardCoordinates 
                { 
                    Row = row,
                    Col=col
                });
        }
        public async Task ClearBoard()
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.roomService.ClearBoard(userId);
            await this.Clients.Group(await this.ticTacToeService.GetRoomName(userId)).SendAsync("ClearBoard");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.roomService.RemoveTicTacToeRoom(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await base.OnDisconnectedAsync(exception);
        }
    }
}
