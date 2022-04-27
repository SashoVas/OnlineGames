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
        public async Task<string> CreateTicTacToeRoom(string username)
        {
            var room = new TicTacToeRoom
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName=username
            };
            await dbContext.TicTacToeRooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task RemoveRoom(string userId)
        {
            var user =await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms.Include(r=>r.Users).FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            user.TicTacToeRoom = null;
            user.TicTacToeRoomId = null;
            if (room.FirstPlayerName==user.UserName)
            {
                //The player that leves the room is first, so we set it to null
                room.FirstPlayerName = null;
            }
            if (room.Users.Count()==1)
            {
                //The room is empty so we remove it
                dbContext.TicTacToeRooms.Remove(room);
            }
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task SetRoomToUser(string userId, string roomId)
        {
            var user =await userManager.FindByIdAsync(userId);
            var room=await dbContext.TicTacToeRooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room==null||room.Users.Count>=2)
            {
                //Room is full or does not exist
                throw new ArgumentException();
            }
            user.TicTacToeRoom = room;
            if (room.FirstPlayerName==null)
            {
                //Here if the new join user is first
                room.FirstPlayerName = user.UserName;
            }
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBoardTicTacToe(string userId,int row, int col)
        {
            var user = await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms.Include(r=>r.Users).FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            if (room.BoardString[((3 * row) + col)]!='0')
            {
                //The position is alredy taken
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn && room.FirstPlayerName==user.UserName)
            {
                //First player move
                room.BoardString = room.BoardString[0..((3 * row) + col)]+"1"+ room.BoardString[((3 * row) + col+1) .. ^0];
            }
            else if (!room.FirstPlayerTurn && room.FirstPlayerName !=user.UserName)
            {
                //Second player move
                room.BoardString = room.BoardString[0..((3 * row) + col)] + "2" + room.BoardString[((3 * row) + col + 1)..^0];
            }
            else
            {
                throw new ArgumentException();
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }
        public async Task ClearBoard(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms
                .Include(r=>r.Users)
                .FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            if (room==null)
            {
                throw new ArgumentException();
            }
            //Swap first turns
            if (room.Users.Count()<=1)
            {
                //Here if oponent is ai or the room is not full
                room.FirstPlayerName=room.FirstPlayerName!=null?null:user.UserName;
            }
            else
            {
                room.FirstPlayerName = room.Users.FirstOrDefault(r => r.UserName != room.FirstPlayerName).UserName;
            }
            room.FirstPlayerTurn=true;
            room.BoardString =room.BoardString.Length==9?"000000000":new string('0',6*7);
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserBoard(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user==null)
            {
                throw new ArgumentException();
            }
            var room = await dbContext.TicTacToeRooms.FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            if (room==null)
            {
                throw new ArgumentException();
            }
            return room.BoardString;
        }

        public async Task<int> GetTurn(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException();
            }
            var room = await dbContext.TicTacToeRooms.FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            if (room == null)
            {
                throw new ArgumentException();
            }
            return room.FirstPlayerTurn?1:-1;
        }

        public async Task UpdateBoardAI(string userId, int row, int col)
        {
            var user = await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms.FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
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
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable< RoomsServiceModel>> GetAvailableRooms()
        {
            return await dbContext.TicTacToeRooms
                .Where(r => r.Users.Count() < 2)
                .Take(10)
                .Select(r=> new RoomsServiceModel
                {
                    Capacity=2,
                    Players=1,
                    GameName="TicTacToe",
                    UserName=r.Users.First().UserName,
                    RoomId=r.Id,
                    First=r.FirstPlayerName==null
                }).ToListAsync();
        }

        public async Task<string> CreateConnect4Room(string username)
        {
            var room = new TicTacToeRoom
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName = username,
                BoardString=new string('0',6*7)
            };
            await dbContext.TicTacToeRooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<string> GetRoomId(string userId)
        {
            return (await userManager.FindByIdAsync(userId)).TicTacToeRoomId;
        }

        public async Task UpdateBoardConnect4(string userId, int col)
        {
            var user = await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms.Include(r => r.Users).FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            int row = -1;
            for (int i = 5; i >= 0; i--)
            {
                if (room.BoardString[(i*6)+col]=='0')
                {
                    row = i;
                    break;
                }
            }
            if (row==-1)
            {
                throw new ArgumentException();
            }

            if (room.FirstPlayerTurn && room.FirstPlayerName == user.UserName)
            {
                //First player move
                room.BoardString = room.BoardString[0..((6 * row) + col)] + "1" + room.BoardString[((6 * row) + col + 1)..^0];
            }
            else if (!room.FirstPlayerTurn && room.FirstPlayerName != user.UserName)
            {
                //Second player move
                room.BoardString = room.BoardString[0..((6 * row) + col)] + "2" + room.BoardString[((6 * row) + col + 1)..^0];
            }
            else
            {
                throw new ArgumentException();
            }
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }
    }
}
