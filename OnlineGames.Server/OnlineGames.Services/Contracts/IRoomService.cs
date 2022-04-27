using OnlineGames.Data.Models;
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
        Task<string> CreateTicTacToeRoom(string username);
        Task<string> CreateConnect4Room(string username);
        Task RemoveRoom(string userId);
        Task SetRoomToUser(string userId,string roomId);
        Task UpdateBoardTicTacToe(string userId, int row, int col);
        Task UpdateBoardConnect4(string userId,int col);
        Task UpdateBoardAI(string userId, int row, int col);
        Task ClearBoard(string userId);
        Task<string> GetUserBoard(string userId);
        Task<int> GetTurn(string userId);
        Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms();
        Task<string> GetRoomId(string userId);

    }
}
