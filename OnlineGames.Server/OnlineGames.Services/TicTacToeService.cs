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
        public TicTacToeService(OnlineGamesDbContext dbContext, UserManager<User> userManager)
        {
            this.ticTacToe=new TicTacToe();
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<string> GetRoomName(string userId)
        {
            return (await this.userManager.FindByIdAsync(userId)).RoomName;
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

        public async Task SetRoomName(string userId,string roomName)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            user.RoomName = roomName;
            await userManager.UpdateAsync(user);
            await dbContext.SaveChangesAsync();

        }
    }
}
