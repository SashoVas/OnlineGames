using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class UserService : IUserService
    {
        private readonly OnlineGamesDbContext dbContext;
        public UserService(OnlineGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<bool> AcceptFriendRequest()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UsersServiceModel>> GetFriends(string userId) 
            => await dbContext.Friends
                       .Where(f => (f.User1Id == userId && f.Accepted) || f.User2Id == userId)
                       .Select(f => new UsersServiceModel
                       {
                           Accepted = f.Accepted,
                           UserName = f.User1Id == userId ? f.User2.UserName : f.User1.UserName
                       })
                       .ToListAsync();

        public async Task<bool> SendFriendRequest(string userId,string friendUserName)
        {
            var friendId=await dbContext.Users
                .Where(u=>u.UserName== friendUserName)
                .Select(u=>u.Id)
                .FirstOrDefaultAsync();
            if (friendId==null)
            {
                return false;
            }
            var friend = new Friend 
            { 
                Id=Guid.NewGuid().ToString(),
                User1Id = userId,
                User2Id = friendId,
                Accepted = false,
            };
            dbContext.Friends.Add(friend);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
