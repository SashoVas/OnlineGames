using OnlineGames.Logic.Connect4;
using OnlineGames.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class Connect4Service : IConnect4Service
    {

        private readonly Connect4 connect4;
        public Connect4Service()
        {
            this.connect4=new Connect4();
        }
        public async Task<int> MakeMove(string boardSring, int currentPlayer,int difficulty)
        {
            return this.connect4.GetBestMove(boardSring, currentPlayer, difficulty);
        }
    }
}
