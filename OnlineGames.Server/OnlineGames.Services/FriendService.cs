using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Friend;

namespace OnlineGames.Services
{
    public class FriendService : IFriendService
    {
        private readonly IRepository<Friend> repoFriend;
        private readonly IRepository<User> repoUser;
        public FriendService(IRepository<Friend> repoFriend, IRepository<User> repoUser)
        {
            this.repoFriend = repoFriend;
            this.repoUser = repoUser;
        }

        public async Task<bool> AcceptFriendRequest(string userId, string friendId)
        {
            var friend =await repoFriend.GetAll()
                .Where(f => f.User1Id==friendId && f.User2Id == userId)
                .FirstOrDefaultAsync();

            if (friend == null)
                return false;

            friend.Accepted = true;
            repoFriend.Update(friend);
            await repoFriend.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFriend(string userId, string friendId)
        {
            var friend= await repoFriend.GetAll()
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
            repoFriend.Remove(friend);
            await repoFriend.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FriendExist(string userId, string friendUserName)
        => await repoFriend.GetAll()
            .AnyAsync(f => (f.User1Id == userId && f.User2.UserName == friendUserName)
            || (f.User2Id == userId && f.User1.UserName == friendUserName));

        public async Task<string> GetFriendId(string userId, string friendId)
        => await repoFriend.GetAll()
            .Where(f => (f.User1Id == userId && f.User2Id==friendId)
            || (f.User2Id == userId && f.User1Id==friendId))
            .Select(f => f.Id)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<FriendServiceModel>> GetFriends(string userId) 
            => await repoFriend.GetAll()
                .Where(f => (f.User1Id == userId && f.Accepted) || f.User2Id == userId)
                .OrderByDescending(f=>f.Accepted)
                .Select(f => new FriendServiceModel
                {
                    Accepted = f.Accepted,
                    UserName = f.User1Id == userId ? f.User2.UserName : f.User1.UserName,
                    Id= f.User1Id == userId ? f.User2Id : f.User1Id,
                })
                .ToListAsync();

        public async Task<string> SendFriendRequest(string userId,string friendUserName)
        {
            var friendId=await repoUser.GetAll()
                .Where(u=>u.UserName== friendUserName)
                .Select(u=>u.Id)
                .FirstOrDefaultAsync();
            if (friendId == null)
                throw new ArgumentException();
            
            var friend = new Friend 
            { 
                Id=Guid.NewGuid().ToString(),
                User1Id = userId,
                User2Id = friendId,
                Accepted = false,
            };
            await repoFriend.AddAsync(friend);
            await repoFriend.SaveChangesAsync();
            return friend.Id;
        }
    }
}
