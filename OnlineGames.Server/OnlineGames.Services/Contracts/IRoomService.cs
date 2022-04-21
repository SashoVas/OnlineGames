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
        Task RemoveTicTacToeRoom(string userId);
        Task SetTicTacToeRoomToUser(string userId,string roomId);
        Task UpdateBoard(string userId, int row, int col);
        Task UpdateBoardAI(string userId, int row, int col);
        Task ClearBoard(string userId);
        Task<string> GetUserRoom(string userId);
        Task<int> GetTurn(string userId);
        Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms();
        

    }
}
