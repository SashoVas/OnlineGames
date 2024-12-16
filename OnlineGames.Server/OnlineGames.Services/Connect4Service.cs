using OnlineGames.Logic.Connect4;
using OnlineGames.Services.Contracts;

namespace OnlineGames.Services
{
    public class Connect4Service : IConnect4Service
    {
        private readonly IRoomService roomService;
        private readonly IConnect4 connect4;
        public Connect4Service(IRoomService roomService, IConnect4 connect4)
        {
            this.roomService = roomService;
            this.connect4 = connect4;
        }

        public int MakeMove(string boardSring, int currentPlayer, int difficulty)
            => connect4.GetMove(boardSring, currentPlayer, difficulty);

        public async Task<string> UpdateBoard(string userId, int col, string username)
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
                throw new ArgumentException("The position is alredy taken");
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
                throw new ArgumentException("Its not user turn");
            }
            await roomService.UpdateBoard(room);
            return room.BoardString;
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
                throw new ArgumentException("The position is alredy taken");
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
