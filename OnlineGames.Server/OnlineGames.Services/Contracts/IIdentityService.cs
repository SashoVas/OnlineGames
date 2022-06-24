using OnlineGames.Data.Models;

namespace OnlineGames.Services.Contracts
{
    public interface IIdentityService
    {
        Task<string> Login(string userName, string password, string secret);
        Task<string> Register(string username, string password, string confirmPassword);
        string GetJwt(User user, string Secret);
    }
}
