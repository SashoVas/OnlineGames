using OnlineGames.Services.Models.User;

namespace OnlineGames.Services.Contracts
{
    public interface IUserService
    {
        Task<UserServiceModel> GetUser(string name);
        Task<bool> UpdateUser(string id,string descripiton,string imgUrl,string userName);
    }
}
