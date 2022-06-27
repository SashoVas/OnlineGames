using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Friend;

namespace OnlineGames.Services
{
    public class FriendService : IFriendService
    {
        private readonly IRepository<Friend> repo;
        private readonly IUserService userService;
        public FriendService(IRepository<Friend> repo, IUserService userService)
        {
            this.repo = repo;
            this.userService = userService;
        }

        public async Task<bool> AcceptFriendRequest(string userId, string friendId)
        {
            var friend =await repo.GetAll()
                .Where(f => f.User1Id==friendId && f.User2Id == userId)
                .FirstOrDefaultAsync();

            if (friend == null)
                return false;

            friend.Accepted = true;
            repo.Update(friend);
            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFriend(string userId, string friendId)
        {
            var friend= await repo.GetAll()
                .Where(f => (f.User1Id == userId && f.User2Id == friendId)
                || (f.User2Id == userId && f.User1Id == friendId))
                .Select(f => new Friend
                {
                    Id = f.Id
                }).FirstOrDefaultAsync();
            if (friend==null)
            {
                return false;
            }
            repo.Remove(friend);
            await repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FriendExist(string userId, string friendUserName)
        => await repo.GetAll()
            .AnyAsync(f => (f.User1Id == userId && f.User2.UserName == friendUserName)
            || (f.User2Id == userId && f.User1.UserName == friendUserName));

        public async Task<string> GetFriendId(string userId, string friendId)
        => await repo.GetAll()
            .Where(f => (f.User1Id == userId && f.User2Id==friendId)
            || (f.User2Id == userId && f.User1Id==friendId))
            .Select(f => f.Id)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<FriendServiceModel>> GetFriends(string userId) 
            => await repo.GetAll()
                .Where(f => (f.User1Id == userId && f.Accepted) || f.User2Id == userId)
                .OrderByDescending(f=>f.Accepted)
                .Select(f => new FriendServiceModel
                {
                    Accepted = f.Accepted,
                    UserName = f.User1Id == userId ? f.User2.UserName : f.User1.UserName,
                    Id= f.User1Id == userId ? f.User2Id : f.User1Id,
                })
                .ToListAsync();

        public async Task<IEnumerable<FriendServiceModel>> GetRequests(string userId) 
            => await repo.GetAll()
                .Where(f => !f.Accepted && (f.User1Id == userId || f.User2Id == userId))
                .Select(f => new FriendServiceModel
                {
                    Accepted = false,
                    Id = f.User1Id == userId ? f.User2Id : f.User1Id,
                    UserName = f.User1Id == userId ? f.User2.UserName : f.User1.UserName
                }).ToListAsync();

        public async Task<string> SendFriendRequest(string userId,string friendUserName)
        {
            var friendId=await userService.GetUserIdFromName(friendUserName);
            if (friendId == null)
                throw new ArgumentException("No such user");
            
            var friend = new Friend 
            { 
                Id=Guid.NewGuid().ToString(),
                User1Id = userId,
                User2Id = friendId,
                Accepted = false,
            };
            await repo.AddAsync(friend);
            await repo.SaveChangesAsync();
            return friend.Id;
        }
    }
}
