using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using OnlineGames.Web.Models.TicTacToe;
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
        public async Task MakeMoveAI(TicTacToeMoveAIInput input)
        {
            try
            {
                //Skip update board if the ai is first
                if (input.Row!=-1 && input.Col!=-1)
                {
                    await roomService.UpdateBoardTicTacToe(GetUserId(), input.Row, input.Col,GetUserName());
                }
                var boardString = await roomService.GetUserBoard(GetUserId());
                if (!boardString.Contains("0"))
                {
                    //The board is full
                    return;
                }
                var currentPlayer = await roomService.GetTurn(GetUserId());
                var output =await this.ticTacToeService.MakeMove(boardString,currentPlayer);
                await roomService.UpdateBoardAITicTacToe(GetUserId(), output.Row,output.Col);
                await this.Clients.Caller.SendAsync("OponentMove", output);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public async Task MakeMoveOponent(int row, int col)
        {
            try
            {
                await roomService.UpdateBoardTicTacToe(GetUserId(), row, col,GetUserName());
                await this.Clients.OthersInGroup(await this.roomService.GetRoomId(GetUserId())).SendAsync("OponentMove",
                new BoardCoordinates
                {
                    Row = row,
                    Col = col
                });
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
