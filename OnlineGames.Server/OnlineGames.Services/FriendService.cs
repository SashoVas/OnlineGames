using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Friend;

namespace OnlineGames.Services
{
    public class FriendService : IFriendService
    {
        private readonly OnlineGamesDbContext dbContext;
        public FriendService(OnlineGamesDbContext dbContext) 
            => this.dbContext = dbContext;
        public async Task<bool> AcceptFriendRequest(string userId, string friendId)
        {
            var friend =await dbContext
                .Friends
                .Where(f => f.User1Id==friendId && f.User2Id == userId)
                .FirstOrDefaultAsync();

            if (friend == null)
                return false;

            friend.Accepted = true;
            dbContext.Update(friend);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFriend(string userId, string friendId)
        {
            var friend= await dbContext.Friends
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
            dbContext.Friends.Remove(friend);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FriendExist(string userId, string friendUserName)
        => await dbContext.Friends
            .AnyAsync(f => (f.User1Id == userId && f.User2.UserName == friendUserName)
            || (f.User2Id == userId && f.User1.UserName == friendUserName));

        public async Task<string> GetFriendId(string userId, string friendId)
        => await dbContext.Friends
            .Where(f => (f.User1Id == userId && f.User2Id==friendId)
            || (f.User2Id == userId && f.User1Id==friendId))
            .Select(f => f.Id)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<FriendServiceModel>> GetFriends(string userId) 
            => await dbContext.Friends
                .Where(f => (f.User1Id == userId && f.Accepted) || f.User2Id == userId)
                .OrderByDescending(f=>f.Accepted)
                .Select(f => new FriendServiceModel
                {
                    Accepted = f.Accepted,
                    UserName = f.User1Id == userId ? f.User2.UserName : f.User1.UserName,
                    Id= f.User1Id == userId ? f.User2Id : f.User1Id,
                })
                .ToListAsync();
        
        public async Task<bool> IsUserInRoom(string userId, string roomId) 
            => await dbContext.Users
                .AnyAsync(u => u.Id == userId && u.RoomId == roomId);

        public async Task<bool> SendFriendRequest(string userId,string friendUserName)
        {
            var friendId=await dbContext.Users
                .Where(u=>u.UserName== friendUserName)
                .Select(u=>u.Id)
                .FirstOrDefaultAsync();
            if (friendId==null)
                return false;
            
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
