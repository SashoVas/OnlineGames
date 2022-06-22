namespace OnlineGames.Services.Contracts
{
    public interface IConnect4Service
    {
        Task<int> MakeMove(string boardSring, int currentPlayer,int difficulty);
        Task<string> UpdateBoard(string userId, int col, string username);
        Task UpdateBoardAI(string userId, int col);
    }
}
