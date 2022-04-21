using OnlineGames.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface ITicTacToeService
    {
        Task<BoardCoordinates> MakeMove(string boardSring,int currentPlayer);
        Task<string> GetRoomName(string userId);
    }
}
