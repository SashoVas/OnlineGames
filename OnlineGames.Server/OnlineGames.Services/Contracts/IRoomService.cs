﻿using OnlineGames.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IRoomService
    {
        Task<string> CreateTicTacToeRoom(string username);
        Task RemoveTicTacToeRoom(string userId);
        Task SetTicTacToeRoomToUser(User user,string roomId);
        Task UpdateBoard(string userId, int row, int col);
        Task ClearBoard(string userId);
    }
}
