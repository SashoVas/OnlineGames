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
        private readonly IIdentityService identityService;
        public UserService(IRepository<User> repo, IIdentityService identityService)
        {
            this.repo = repo;
            this.identityService = identityService;
        }

        public Task<UserServiceModel> GetUser(string name) 
            =>repo.GetAll()
                .Where(u => u.UserName == name)
                .Select(u => new UserServiceModel
                {
                    Username = u.UserName,
                    Description=u.Description,
                    ImgUrl = u.ImgUrl,
                })
                .FirstOrDefaultAsync();

        public async Task<UpdateUserServiceModel> UpdateUser(string id, string descripiton, string imgUrl, string userName,string secret)
        {
            var user =await repo.GetAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
            user.UserName = userName;
            user.Description = descripiton;
            user.ImgUrl = imgUrl;
            user.NormalizedUserName = user.UserName.ToUpper();
            repo.Update(user);
            await repo.SaveChangesAsync();

            return new UpdateUserServiceModel
            {
                ImgUrl = user.ImgUrl,
                IsMe = true,
                Description = user.Description,
                Username = user.UserName,
                Token = identityService.GetJwt(user, secret)
            };
        }

        public Task<bool> IsUserInRoom(string userId, string roomId)
            => repo.GetAll()
                .AnyAsync(u => u.Id == userId && (u.Room1!=null || u.Room2!=null));

        public Task<string> GetUserIdFromName(string name) 
            =>repo.GetAll()
                .Where(u => u.UserName == name)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

        public Task<UserCardServiceModel> GetUserCard(string userId) 
            =>repo.GetAll()
                .Where(u => u.Id == userId)
                .Select(u => new UserCardServiceModel
                {
                    ImgUrl = u.ImgUrl,
                    Username = u.UserName
                }).FirstOrDefaultAsync();
    }
}
