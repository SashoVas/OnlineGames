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
        public async Task<UserServiceModel> GetUser(string name) 
            => await dbContext.Users
                .Where(u => u.UserName == name)
                .Select(u => new UserServiceModel
                {
                    Username = u.UserName
                })
                .FirstOrDefaultAsync();

        public async Task<bool> UpdateUser(string id, string descripiton, string imgUrl, string userName)
        {
            var user =await dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            user.UserName = userName ?? user.UserName;
            user.Description = descripiton ?? user.UserName;
            //todo:imageUrl
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
