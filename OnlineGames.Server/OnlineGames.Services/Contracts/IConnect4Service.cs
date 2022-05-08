﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IConnect4Service
    {
        Task<int> MakeMove(string boardSring, int currentPlayer,int difficulty);
        Task UpdateBoard(string userId, int col, string username);
        Task UpdateBoardAI(string userId, int col);
    }
}
