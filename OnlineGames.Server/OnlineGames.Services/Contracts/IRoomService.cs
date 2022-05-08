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
        Task<string> CreateRoom(string username, bool isPrivate, string gameName, int board);
        Task RemoveRoom(string userId);
        Task SetRoomToUser(string userId,string roomId);
        Task ClearBoard(string userId, string username);
        Task<string> GetUserBoard(string userId);
        Task<int> GetTurn(string userId);
        Task<Room> GetRoom(string roomId);
        Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms(string game, int count, int page);
        Task<string> GetRoomId(string userId);
        Task UpdateBoard(Room room);

    }
}
