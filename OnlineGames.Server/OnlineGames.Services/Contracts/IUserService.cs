using OnlineGames.Services.Models.User;

namespace OnlineGames.Services.Contracts
{
    public interface IUserService
    {
        Task<UserServiceModel> GetUser(string name);
        Task<UpdateUserServiceModel> UpdateUser(string id, string descripiton, string imgUrl, string userName, string secret);
        Task<bool> IsUserInRoom(string userId, string roomId);
        Task<string> GetUserIdFromName(string name);
        Task<UserCardServiceModel> GetUserCard(string userId);
    }
}
