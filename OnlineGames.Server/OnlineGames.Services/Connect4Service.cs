using OnlineGames.Logic.Connect4;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Services
{
    public class Connect4Service : IConnect4Service
    {
        private readonly IRoomService roomService;
        public Connect4Service(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        public async Task<int> MakeMove(string boardSring, int currentPlayer, int difficulty) 
            => Connect4.GetBestMove(boardSring, currentPlayer, difficulty);

        public async Task UpdateBoard(string userId, int col, string username)
        {
            var room = await roomService.GetRoomByUserId(userId);
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
            await roomService.UpdateBoard(room);
        }

        public async Task UpdateBoardAI(string userId, int col)
        {
            var room = await roomService.GetRoomByUserId(userId);
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
            await roomService.UpdateBoard(room);
        }
    }
}
