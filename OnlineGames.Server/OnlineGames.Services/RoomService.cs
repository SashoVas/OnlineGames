using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class RoomService : IRoomService
    {
        private readonly OnlineGamesDbContext dbContext;
        public RoomService(OnlineGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Room>GetRoomByUserId(string userId)
        {
            var result = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .Select(u => u.Room)
                .FirstOrDefaultAsync();
            if (result==null)
            {
                throw new ArgumentException();
            }
            return result;
        }
        public async Task<string> CreateRoom(string username, bool isPrivate,string gameName,int board)
        {
            var room = new Room
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName = username,
                BoardString = new string('0', board),
                Private = isPrivate,
                GameName = gameName
            };
            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task RemoveRoom(string userId)
        {

            var result = await dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    User = u,
                    Room = u.Room
                }).FirstOrDefaultAsync();

            result.User.Room = null;
            result.User.RoomId = null;
            if (result.Room.FirstPlayerName== result.User.UserName)
            {
                //The player that leves the room is first, so we set it to null
                result.Room.FirstPlayerName = null;
            }
            if (result.Room.Users.Count()==1)
            {
                //The room is empty so we remove it
                dbContext.Rooms.Remove(result.Room);
            }
            dbContext.Users.Update(result.User);
            await dbContext.SaveChangesAsync();
        }

        public async Task SetRoomToUser(string userId, string roomId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u=>u.Id==userId);
            var room = await dbContext.Rooms.Include(r=>r.Users).FirstOrDefaultAsync(r => r.Id == roomId);

            if (room.Users.Count>=2)
            {
                //Room is full or does not exist
                throw new ArgumentException();
            }
            user.Room = room;
            if (room.FirstPlayerName==null)
            {
                //Here if the new join user is first
                room.FirstPlayerName = user.UserName;
            }
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task ClearBoard(string userId,string username)
        {
            //var room = await GetRoomWithUsers(await GetRoomId(userId));
            var room = await dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u=>u.Room)
                .ThenInclude(r=>r.Users)
                .Select(u =>u.Room)
                .FirstOrDefaultAsync();
            //Swap first turns
            if (room.Users.Count()<=1)
            {
                //Here if oponent is ai or the room is not full
                room.FirstPlayerName=room.FirstPlayerName!=null?null:username;
            }
            else
            {
                room.FirstPlayerName = room.Users.FirstOrDefault(r => r.UserName != room.FirstPlayerName).UserName;
            }
            room.FirstPlayerTurn=true;
            room.BoardString =room.BoardString.Length==9?"000000000":new string('0',6*7);
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserBoard(string userId) 
            => await dbContext.Users
            .Where(u=>u.Id==userId)
            .Select(u=>u.Room.BoardString)
            .FirstOrDefaultAsync();

        public async Task<int> GetTurn(string userId) 
            =>await dbContext.Users
            .Where(u=>u.Id==userId)
            .Select(u=>u.Room.FirstPlayerTurn)
            .FirstOrDefaultAsync() ? 1 : -1;

        public async Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms(string game, int count, int page) 
            => await dbContext.Rooms
                .Where(r => r.Users.Count() < 2 && !r.Private && game != null ? game == r.GameName : true)
                .Skip(count * page)
                .Take(count)
                .Select(r => new RoomsServiceModel
                {
                    Capacity = 2,
                    Players = 1,
                    GameName = r.GameName,
                    UserName = r.Users.First().UserName,
                    RoomId = r.Id,
                    First = r.FirstPlayerName == null
                }).ToListAsync();

        public async Task<string> GetRoomId(string userId) 
            =>await dbContext.Users
            .Where(u=>u.Id==userId)
            .Select(u=>u.RoomId)
            .FirstOrDefaultAsync();

        public async Task UpdateBoard(Room room)
        {
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }
    }
}
