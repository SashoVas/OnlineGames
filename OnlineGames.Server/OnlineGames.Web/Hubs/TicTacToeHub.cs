using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.TicTacToe;
using OnlineGames.Web.Models.TicTacToe;

namespace OnlineGames.Web.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TicTacToeHub : GameHub
    {
        private readonly ITicTacToeService ticTacToeService;
        public TicTacToeHub(IRoomService roomService, ITicTacToeService ticTacToeService) : base(roomService)
            => this.ticTacToeService = ticTacToeService;
        protected override string GetNameOfGame() => "TicTacToe";
        public async Task MakeMoveAI(TicTacToeMoveInput input)
        {
            try
            {
                string boardString;
                if (input.Row != -1 && input.Col != -1)
                {
                    boardString = await ticTacToeService.UpdateBoard(GetUserId(), input.Row, input.Col, GetUserName());
                    if (!boardString.Contains("0"))
                    {
                        //The board is full
                        return;
                    }
                }
                else
                {
                    //Skip update board if the ai is first
                    boardString = await roomService.GetUserBoard(GetUserId());
                }
                var output = this.ticTacToeService.MakeMove(boardString);
                await ticTacToeService.UpdateBoardAI(GetUserId(), output.Row, output.Col);
                await this.Clients.Caller.SendAsync("OponentMove", output);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task MakeMoveOponent(TicTacToeMoveInput input)
        {
            try
            {
                await ticTacToeService.UpdateBoard(GetUserId(), input.Row, input.Col, GetUserName());
                await this.Clients.OthersInGroup(await this.roomService.GetRoomId(GetUserId())).SendAsync("OponentMove",
                new BoardCoordinates
                {
                    Row = input.Row,
                    Col = input.Col
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
