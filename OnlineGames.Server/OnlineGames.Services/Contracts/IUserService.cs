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
        Task<bool> AcceptFriendRequest();
    }
}
