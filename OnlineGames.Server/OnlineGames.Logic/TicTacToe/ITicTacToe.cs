using OnlineGames.Logic.TicTacToe.Helper;

namespace OnlineGames.Logic.TicTacToe
{
    public interface ITicTacToe
    {
        CellCoordinates GetMove(string boardString);
    }
}
