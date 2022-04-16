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
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn && room.FirstPlayerName==user.UserName)
            {
                room.BoardString = room.BoardString[0..((3 * row) + col)]+"1"+ room.BoardString[((3 * row) + col+1) .. ^0];
            }
            else if (!room.FirstPlayerTurn && room.FirstPlayerName !=user.UserName)
            {
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
            var room = await dbContext.TicTacToeRooms.FirstOrDefaultAsync(t => t.Id == user.TicTacToeRoomId);
            if (room==null)
            {
                throw new ArgumentException();
            }
            room.BoardString = "000000000";
            dbContext.TicTacToeRooms.Update(room);
            await dbContext.SaveChangesAsync();
        }
    }
}
