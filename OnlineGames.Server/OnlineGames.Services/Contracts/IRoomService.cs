﻿using OnlineGames.Data.Models;
using OnlineGames.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IRoomService
    {
        Task<string> CreateTicTacToeRoom(string username,bool isPrivate);
        Task<string> CreateConnect4Room(string username, bool isPrivate);
        Task RemoveRoom(string userId);
        Task SetRoomToUser(string userId,string roomId);
        Task UpdateBoardTicTacToe(string userId, int row, int col,string username);
        Task UpdateBoardConnect4(string userId,int col, string username);
        Task UpdateBoardAITicTacToe(string userId, int row, int col);
        Task UpdateBoardAIConnect4(string userId, int col);
        Task ClearBoard(string userId, string username);
        Task<string> GetUserBoard(string userId);
        Task<int> GetTurn(string userId);
        Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms(string game, int count, int page);
        Task<string> GetRoomId(string userId);

    }
}
