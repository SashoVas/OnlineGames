using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;

namespace OnlineGames.Services
{
    public class UserService : IUserService
    {
        private OnlineGamesDbContext dbContext;
        public UserService(OnlineGamesDbContext dbContext) 
            => this.dbContext = dbContext;
        public async Task<UserServiceModel> GetUser(string id) 
            => await dbContext.Users
                .Where(u => u.Id == id)
                .Select(u => new UserServiceModel
                {
                    Username = u.UserName
                })
                .FirstOrDefaultAsync();
    }
}
