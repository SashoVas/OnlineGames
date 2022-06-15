using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;

namespace OnlineGames.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repo;
        public UserService(IRepository<User> repo) 
            => this.repo = repo;
        public async Task<UserServiceModel> GetUser(string name) 
            => await repo.GetAll()
                .Where(u => u.UserName == name)
                .Select(u => new UserServiceModel
                {
                    Username = u.UserName,
                    Description=u.Description,
                    ImgUrl = u.ImgUrl,
                })
                .FirstOrDefaultAsync();

        public async Task<UserServiceModel> UpdateUser(string id, string descripiton, string imgUrl, string userName)
        {
            var user =await repo.GetAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            user.UserName = userName;
            user.Description = descripiton;
            user.ImgUrl = imgUrl;
            repo.Update(user);
            await repo.SaveChangesAsync();
            return new UserServiceModel
            {
                ImgUrl = user.ImgUrl,
                IsMe=true,
                Description=user.Description,
                Username=user.UserName
            };
        }

        public async Task<bool> IsUserInRoom(string userId, string roomId)
            => await repo.GetAll()
                .AnyAsync(u => u.Id == userId && (u.Room1!=null || u.Room2!=null));

        public async Task<string> GetUserIdFromName(string name) 
            => await repo.GetAll()
                .Where(u => u.UserName == name)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
    }
}
