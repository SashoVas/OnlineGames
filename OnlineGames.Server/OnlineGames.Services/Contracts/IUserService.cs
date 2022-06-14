using OnlineGames.Services.Models.User;

namespace OnlineGames.Services.Contracts
{
    public interface IUserService
    {
        Task<UserServiceModel> GetUser(string name);
        Task<UserServiceModel> UpdateUser(string id,string descripiton,string imgUrl,string userName);
        Task<bool> IsUserInRoom(string userId, string roomId);
    }
}
