using OnlineGames.Logic.TicTacToe;
using OnlineGames.Services.Contracts;
using OnlineGames.Services.Models.TicTacToe;

namespace OnlineGames.Services
{
    public class TicTacToeService : ITicTacToeService
    {
        private readonly IRoomService roomService;
        private readonly ITicTacToe ticTacToe;
        public TicTacToeService(IRoomService roomService, ITicTacToe ticTacToe)
        {
            this.roomService = roomService;
            this.ticTacToe = ticTacToe;
        }

        public BoardCoordinates MakeMove(string boardSring)
        {
            var output=ticTacToe.GetMove(boardSring);
            return new BoardCoordinates 
            {
                Row= output.X,
                Col= output.Y,
            };
        }

        public async Task<string> UpdateBoard(string userId, int row, int col, string username)
        {
            var room = await roomService.GetRoomByUserId(userId);
            if (room.BoardString[((3 * row) + col)] != '0')
            {
                throw new ArgumentException("The position is alredy taken");
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
                throw new ArgumentException("Its not user turn");
            }
            await roomService.UpdateBoard(room);
            return room.BoardString;
        }

        public async Task UpdateBoardAI(string userId, int row, int col)
        {
            var room = await roomService.GetRoomByUserId(userId);
            if (room.BoardString[(3 * row) + col] != '0')
            {
                throw new ArgumentException("The position is alredy taken");
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
