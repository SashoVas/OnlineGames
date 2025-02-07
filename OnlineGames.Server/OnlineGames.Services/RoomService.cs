﻿using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.Room;

namespace OnlineGames.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> repo;
        public RoomService(IRepository<Room> repo)
            => this.repo = repo;

        public async Task<Room> GetRoomByUserId(string userId)
        {
            var result = await repo.GetAll()
                .Where(r => r.Player1Id == userId || r.Player2Id == userId)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                throw new ArgumentException();
            }
            return result;
        }
        public async Task<string> CreateRoom(string username, bool isPrivate, string gameName, int board)
        {
            var room = new Room
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayerName = username,
                BoardString = new string('0', board),
                Private = isPrivate,
                GameName = gameName
            };
            await repo.AddAsync(room);
            await repo.SaveChangesAsync();
            return room.Id;
        }

        public async Task RemoveRoom(string userId)
        {

            var result = await repo.GetAll()
                .Where(r => r.Player1Id == userId || r.Player2Id == userId)
                .Select(r => new
                {
                    UserName = userId == r.Player1Id ? r.Player1.UserName : r.Player2.UserName,
                    Room = r
                }).FirstOrDefaultAsync();
            //Remove user from room
            if (result.Room.Player1Id == userId)
            {
                result.Room.Player1Id = null;
            }
            else
            {
                result.Room.Player2Id = null;
            }
            if (result.Room.FirstPlayerName == result.UserName)
            {
                //The player that leves the room is first, so we set it to null
                result.Room.FirstPlayerName = null;
            }
            if (result.Room.Player1Id == null && result.Room.Player2Id == null)
            {
                //The room is empty so we remove it
                repo.Remove(result.Room);
            }
            else
            {
                //Else we update it
                repo.Update(result.Room);
            }
            await repo.SaveChangesAsync();
        }

        public async Task SetRoomToUser(string userId, string roomId, string username)
        {
            var room = await repo.GetAll()
                .Where(r => r.Id == roomId)
                .FirstOrDefaultAsync();
            if (room == null)
            {
                throw new ArgumentException("Room doesn't exist");
            }
            if (room.Player1Id == null)
            {
                room.Player1Id = userId;
            }
            else if (room.Player2Id == null)
            {
                room.Player2Id = userId;
            }
            else
            {
                throw new ArgumentException("Room is full");
            }
            if (room.FirstPlayerName == null)
            {
                //Here if the new join user is first
                room.FirstPlayerName = username;
            }
            repo.Update(room);
            await repo.SaveChangesAsync();
        }

        public async Task ClearBoard(string userId, string username)
        {
            var room = await repo.GetAll()
                .Where(r => r.Player1Id == userId || r.Player2Id == userId)
                .Include(r => r.Player1)
                .Include(r => r.Player2)
                .FirstOrDefaultAsync();
            //Swap first turns
            if (room.Player1Id == null || room.Player2Id == null)
            {
                //Here if oponent is ai or the room is not full
                room.FirstPlayerName = room.FirstPlayerName != null ? null : username;
            }
            else
            {
                room.FirstPlayerName = room.Player1.UserName == room.FirstPlayerName ? room.Player2.UserName : room.Player1.UserName;
            }
            room.FirstPlayerTurn = true;
            room.BoardString = new string('0', room.BoardString.Length);
            repo.Update(room);
            await repo.SaveChangesAsync();
        }

        public Task<string> GetUserBoard(string userId)
            => repo.GetAll()
            .Where(r => r.Player1Id == userId || r.Player2Id == userId)
            .Select(r => r.BoardString)
            .FirstOrDefaultAsync();

        public async Task<int> GetTurn(string userId)
            => await repo.GetAll()
            .Where(r => r.Player1Id == userId || r.Player2Id == userId)
            .Select(r => r.FirstPlayerTurn)
            .FirstOrDefaultAsync() ? 1 : -1;

        public async Task<IEnumerable<RoomsServiceModel>> GetAvailableRooms(string game, int count, int page)
            => await repo.GetAll()
                .Include(r => r.Player1)
                .Include(r => r.Player2)
                .Where(r => !r.Private && (game != null ? game == r.GameName : true) && (r.Player1Id == null || r.Player2Id == null))//&& (r.Player1Id==null || r.Player2Id == null)
                .Skip(count * page)
                .Take(count)
                .Select(r => new RoomsServiceModel
                {
                    Capacity = 2,
                    Players = 1,
                    GameName = r.GameName,
                    UserName = r.Player1 == null ? r.Player2.UserName : r.Player1.UserName,
                    RoomId = r.Id,
                    First = r.FirstPlayerName == null
                }).ToListAsync();

        public Task<string> GetRoomId(string userId)
            => repo.GetAll()
            .Where(r => r.Player1Id == userId || r.Player2Id == userId)
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        public async Task UpdateBoard(Room room)
        {
            room.FirstPlayerTurn = !room.FirstPlayerTurn;
            repo.Update(room);
            await repo.SaveChangesAsync();
        }
    }
}
