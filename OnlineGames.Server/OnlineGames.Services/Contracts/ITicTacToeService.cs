using OnlineGames.Services.Models.TicTacToe;

namespace OnlineGames.Services.Contracts
{
    public interface ITicTacToeService
    {
        Task<BoardCoordinates> MakeMove(string boardSring,int currentPlayer);
        Task UpdateBoard(string userId, int row, int col, string username);
        Task UpdateBoardAI(string userId, int row, int col);

    }
}
