using OnlineGames.Data;
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
        //private readonly OnlineGamesDbContext dbContext;
        private readonly TicTacToe ticTacToe;
        public TicTacToeService(OnlineGamesDbContext dbContext)
        {
            //this.dbContext = dbContext;
            this.ticTacToe=new TicTacToe();
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
