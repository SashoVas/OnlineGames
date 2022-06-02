using OnlineGames.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> SendFriendRequest(string userId,string friendUserName);
        Task<IEnumerable<UsersServiceModel>> GetFriends(string userId);
        Task<bool> AcceptFriendRequest(string userId,string friendUserName);
        Task<bool> FriendExist(string userId, string friendUserName);
        Task<string> GetFriendId(string userId, string friendUserName);
        Task<bool> IsUserInRoom(string userId,string roomId);
    }
}
