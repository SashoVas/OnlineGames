using Microsoft.AspNetCore.Identity;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Logic.TicTacToe;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class TicTacToeService : ITicTacToeService
    {
        private readonly TicTacToe ticTacToe;
        private readonly UserManager<User> userManager;
        private readonly OnlineGamesDbContext dbContext;
        private readonly IRoomService roomService;
        public TicTacToeService(OnlineGamesDbContext dbContext, UserManager<User> userManager, IRoomService roomService)
        {
            this.ticTacToe=new TicTacToe();
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.roomService = roomService;
        }

        public async Task<string> GetRoomName(string userId)
        {
            return (await this.userManager.FindByIdAsync(userId)).TicTacToeRoomId;
        }

        public async Task<BoardCoordinates> MakeMove(string boardSring,int currentPlayer)
        {
            var output=this.ticTacToe.CreateSolver(boardSring,currentPlayer);

            return new BoardCoordinates 
            {
                Row= output.X,
                Col= output.Y,
            };
        }
    }
}
