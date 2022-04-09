using Microsoft.AspNetCore.SignalR;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Web.Hubs
{
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
        public async Task MakeMove(string boardString,int currentPlayer)
        {
            var output =await this.ticTacToeService.MakeMove(boardString,currentPlayer);
            await this.Clients.Caller.SendAsync("OponentMove", output);
        }

    }
}
