using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.Connect4.Helper
{
    public class CellCoordinates
    {
        public CellCoordinates()
        {

        }
        public CellCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }
    }
}
