using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.TicTacToe.Helper
{
    public class CellCoordinates
    {
        public CellCoordinates()
        {

        }
        public CellCoordinates(int x, int y, GameState gameState)
        {
            X = x;
            Y = y;
            GameState = gameState;
        }
        public GameState GameState { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
    }
}
