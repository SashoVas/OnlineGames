using OnlineGames.Services.Models.User;

namespace OnlineGames.Services.Contracts
{
    public interface IUserService
    {
        Task<UserServiceModel> GetUser(string id);
    }
}
