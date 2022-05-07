using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
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
        private readonly UserManager<User> userManager;
        public RoomService(OnlineGamesDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        private async Task<User> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user==null)
            {
                throw new ArgumentException();
            }
            return user;
        }
        private async Task<Room>GetRoom(string roomId)
        {
            var room = await dbContext.Rooms.FirstOrDefaultAsync(t => t.Id ==roomId);
            if (room==null)
            {
                throw new ArgumentException();
            }
            return room;
        }
        private async Task<Room> GetRoomWithUsers(string roomId)
        {
            var room = await dbContext.Rooms
                .Include(r=>r.Users)
                .FirstOrDefaultAsync(t => t.Id == roomId);
            if (room == null)
            {
                throw new ArgumentException();
            }
            return room;
        }
        public async Task<string> CreateTicTacToeRoom(string username, bool isPrivate)
        {
            var room = new Room
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName=username,
                Private=isPrivate,
                GameName="TicTacToe"
            };
            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task RemoveRoom(string userId)
        {
            var user =await GetUser(userId);
            var room = await GetRoomWithUsers(user.RoomId);
            user.Room = null;
            user.RoomId = null;
            if (room.FirstPlayerName==user.UserName)
            {
                //The player that leves the room is first, so we set it to null
                room.FirstPlayerName = null;
            }
            if (room.Users.Count()==1)
            {
                //The room is empty so we remove it
                dbContext.Rooms.Remove(room);
            }
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task SetRoomToUser(string userId, string roomId)
        {
            var user =await GetUser(userId);
            var room = await GetRoom(roomId);
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

        public async Task UpdateBoardTicTacToe(string userId,int row, int col,string username)
        {
            var room = await GetRoomWithUsers(await GetRoomId(userId));
            if (room.BoardString[((3 * row) + col)]!='0')
            {
                //The position is alredy taken
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn && room.FirstPlayerName== username)
            {
                //First player move
                room.BoardString = room.BoardString[0..((3 * row) + col)]+"1"+ room.BoardString[((3 * row) + col+1) .. ^0];
            }
            else if (!room.FirstPlayerTurn && room.FirstPlayerName != username)
            {
                //Second player move
                room.BoardString = room.BoardString[0..((3 * row) + col)] + "2" + room.BoardString[((3 * row) + col + 1)..^0];
            }
            else
            {
                throw new ArgumentException();
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task ClearBoard(string userId,string username)
        {
            var room = await GetRoomWithUsers(await GetRoomId(userId));
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
            => (await GetRoom(await GetRoomId(userId))).BoardString;

        public async Task<int> GetTurn(string userId) 
            => (await GetRoom(await GetRoomId(userId))).FirstPlayerTurn ? 1 : -1;

        public async Task UpdateBoardAITicTacToe(string userId, int row, int col)
        {
            var room = await GetRoom(await GetRoomId(userId));
            if (room.BoardString[(3 * row) + col]!='0')
            {
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn)
            {
                room.BoardString = room.BoardString[0..((3 * row) + col)] + "1" + room.BoardString[((3 * row) + col + 1)..^0];
            }
            else
            {
                room.BoardString = room.BoardString[0..((3 * row) + col)] + "2" + room.BoardString[((3 * row) + col + 1)..^0];
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

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

        public async Task<string> CreateConnect4Room(string username, bool isPrivate)
        {
            var room = new Room
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName = username,
                BoardString=new string('0',6*7),
                Private=isPrivate,
                GameName="Connect4"
            };
            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<string> GetRoomId(string userId) 
            => (await GetUser(userId)).RoomId;

        public async Task UpdateBoardConnect4(string userId, int col,string username)
        {
            var room = await GetRoom(await GetRoomId(userId));
            int row = -1;
            for (int i = 5; i >= 0; i--)
            {
                if (room.BoardString[(i*7)+col]=='0')
                {
                    row = i;
                    break;
                }
            }
            if (row==-1)
            {
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn && room.FirstPlayerName == username)
            {
                //First player move
                room.BoardString = room.BoardString[0..((7 * row) + col)] + "1" + room.BoardString[((7 * row) + col + 1)..^0];
            }
            else if (!room.FirstPlayerTurn && room.FirstPlayerName != username)
            {
                //Second player move
                room.BoardString = room.BoardString[0..((7 * row) + col)] + "2" + room.BoardString[((7 * row) + col + 1)..^0];
            }
            else
            {
                throw new ArgumentException();
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBoardAIConnect4(string userId, int col)
        {
            var room = await GetRoom(await GetRoomId(userId));
            int row = -1;
            for (int i = 5; i >= 0; i--)
            {
                if (room.BoardString[(i * 7) + col] == '0')
                {
                    row = i;
                    break;
                }
            }
            if (row == -1)
            {
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn)
            {
                room.BoardString = room.BoardString[0..((7 * row) + col)] + "1" + room.BoardString[((7 * row) + col + 1)..^0];
            }
            else
            {
                room.BoardString = room.BoardString[0..((7 * row) + col)] + "2" + room.BoardString[((7 * row) + col + 1)..^0];
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }
    }
}
