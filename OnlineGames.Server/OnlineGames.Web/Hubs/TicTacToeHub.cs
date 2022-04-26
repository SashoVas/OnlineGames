using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using System.Security.Claims;

namespace OnlineGames.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicTacToeHub: GameHub
    {
        private readonly ITicTacToeService ticTacToeService;
        public TicTacToeHub(IRoomService roomService, ITicTacToeService ticTacToeService) : base(roomService)
        {
            this.ticTacToeService = ticTacToeService;
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
            var boardString = await roomService.GetUserBoard(userId);
            if (!boardString.Contains("0"))
            {
                //The board is full
                return;
            }
            var currentPlayer = await roomService.GetTurn(userId);
            var output =await this.ticTacToeService.MakeMove(boardString,currentPlayer);
            await roomService.UpdateBoardAI(this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier),output.Row,output.Col);
            await this.Clients.Caller.SendAsync("OponentMove", output);
        }
        
        public async Task MakeMoveOponent(int row,int col)
        {
            var userId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await roomService.UpdateBoard(userId,row,col);
            await this.Clients.OthersInGroup(await this.roomService.GetRoomId(userId)).SendAsync("OponentMove",
                new BoardCoordinates 
                { 
                    Row = row,
                    Col=col
                });
        }
    }
}
