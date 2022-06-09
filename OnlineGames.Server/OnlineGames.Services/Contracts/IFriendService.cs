﻿using OnlineGames.Services.Models.Friend;

namespace OnlineGames.Services.Contracts
{
    public interface IFriendService
    {
        Task<bool> SendFriendRequest(string userId,string friendUserName);
        Task<IEnumerable<FriendServiceModel>> GetFriends(string userId);
        Task<bool> AcceptFriendRequest(string userId,string friendId);
        Task<bool> FriendExist(string userId, string friendUserName);
        Task<string> GetFriendId(string userId, string friendId);
        Task<bool> IsUserInRoom(string userId,string roomId);
        Task<bool> DeleteFriend(string userId,string friendId);
    }
}