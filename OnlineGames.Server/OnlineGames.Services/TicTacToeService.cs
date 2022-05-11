using Microsoft.AspNetCore.Identity;
using OnlineGames.Data;
using OnlineGames.Data.Models;
using OnlineGames.Logic.TicTacToe;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Services
{
    public class TicTacToeService : ITicTacToeService
    {
        private readonly IRoomService roomService;

        public TicTacToeService(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        public async Task<BoardCoordinates> MakeMove(string boardSring,int currentPlayer)
        {
            var output=TicTacToe.CreateSolver(boardSring,currentPlayer);
            return new BoardCoordinates 
            {
                Row= output.X,
                Col= output.Y,
            };
        }

        public async Task UpdateBoard(string userId, int row, int col, string username)
        {
            var room = await roomService.GetRoomByUserId(userId);
            if (room.BoardString[((3 * row) + col)] != '0')
            {
                //The position is alredy taken
                throw new ArgumentException();
            }
            if (room.FirstPlayerTurn && room.FirstPlayerName == username)
            {
                //First player move
                room.BoardString = room.BoardString[0..((3 * row) + col)] + "1" + room.BoardString[((3 * row) + col + 1)..^0];
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
            await roomService.UpdateBoard(room);
        }

        public async Task UpdateBoardAI(string userId, int row, int col)
        {
            var room = await roomService.GetRoomByUserId(userId);
            if (room.BoardString[(3 * row) + col] != '0')
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
            await roomService.UpdateBoard(room);
        }
    }
}
