using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Room;

namespace OnlineGames.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> repoRoom;
        private readonly IRepository<User> repoUser;
        public RoomService(IRepository<Room> repoRoom, IRepository<User> repoUser)
        {
            this.repoRoom = repoRoom;
            this.repoUser = repoUser;
        }

        public async Task<Room>GetRoomByUserId(string userId)
        {
            var result = await repoUser.GetAll()
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
            await repoRoom.AddAsync(room);
            await repoRoom.SaveChangesAsync();
            return room.Id;
        }

        public async Task RemoveRoom(string userId)
        {

            var result = await repoUser.GetAll()
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
                repoRoom.Remove(result.Room);
            }
            repoUser.Update(result.User);
            await repoRoom.SaveChangesAsync();
        }

        public async Task SetRoomToUser(string userId, string roomId)
        {
            var user = await repoUser.GetAll().FirstOrDefaultAsync(u=>u.Id==userId);
            var room = await repoRoom.GetAll().Include(r=>r.Users).FirstOrDefaultAsync(r => r.Id == roomId);

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
            repoUser.Update(user);
            await repoRoom.SaveChangesAsync();
        }

        public async Task ClearBoard(string userId,string username)
        {
            //var room = await GetRoomWithUsers(await GetRoomId(userId));
            var room = await repoUser.GetAll()
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
            repoRoom.Update(room);
            await repoRoom.SaveChangesAsync();
        }

        public async Task<string> GetUserBoard(string userId) 
            => await repoUser.GetAll()
            .Where(u=>u.Id==userId)
            .Select(u=>u.Room.BoardString)
            .FirstOrDefaultAsync();

        public async Task<int> GetTurn(string userId) 
            =>await repoUser.GetAll()
            .Where(u=>u.Id==userId)
            .Select(u=>u.Room.FirstPlayerTurn)
            .FirstOrDefaultAsync() ? 1 : -1;

        public async Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms(string game, int count, int page) 
            => await repoRoom.GetAll()
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
            =>await repoUser.GetAll()
            .Where(u=>u.Id==userId)
            .Select(u=>u.RoomId)
            .FirstOrDefaultAsync();

        public async Task UpdateBoard(Room room)
        {
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            repoRoom.Update(room);
            await repoRoom.SaveChangesAsync();
        }
    }
}
