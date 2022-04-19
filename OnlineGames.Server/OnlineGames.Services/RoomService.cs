using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
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

        public async Task RemoveTicTacToeRoom(string userId)
        {
            var user =await userManager.FindByIdAsync(userId);
            var room = await dbContext.TicTacToeRooms.Include(r=>r.Users).FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            user.TicTacToeRoom = null;
            user.TicTacToeRoomId = null;
            if (room.Users.Count()==1)
            {
                //The room is empty so we remove it
                dbContext.TicTacToeRooms.Remove(room);
            }
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task SetTicTacToeRoomToUser(User user,string roomId)
        {
            var room=await dbContext.TicTacToeRooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room==null||room.Users.Count>=2)
            {
                //Room is full or does not exist
                throw new ArgumentException();
            }
            user.TicTacToeRoom = room;
        }

        public async Task UpdateBoard(string userId,int row, int col)
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
            var room = await dbContext.TicTacToeRooms.Include(r=>r.Users).FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
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
            room.BoardString = "000000000";
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserRoom(string userId)
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
    }
}
