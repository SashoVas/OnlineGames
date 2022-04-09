using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.TicTacToe.Helper
{
    public class Board
    {
        public int[,] Matrix { get; set; }
        public int DimesionX { get => Matrix.GetLength(0); }
        public int DimesionY { get => Matrix.GetLength(1); }
        public Board(string boardString,int x,int y)
        {
            this.Matrix = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (boardString[(i * 3) + j]=='0')
                    {
                        continue;
                    }
                    this.Matrix[i, j] = boardString[(i * 3) + j]=='1'?1:-1;
                }
            }
        }
        public Board(int x, int y)
        {
            this.Matrix = new int[x, y];
        }
        public Board(int[,] matrix)
        {
            this.Matrix = matrix.Clone() as int[,];
        }
        public Board ReturnCopy()
        {
            return new Board(this.Matrix);
        }
        public bool MakeAMove(int player, int x, int y)
        {
            if (this.Matrix[x, y] != 0)
            {
                return false;
            }
            this.Matrix[x, y] = player;
            return true;
        }
        public void UndoMove(int x, int y)
        {
            this.Matrix[x, y] = 0;
        }
        public GameState CurrentGameState(int player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (this.Matrix[i, 0] == player && this.Matrix[i, 1] == player && this.Matrix[i, 2] == player)
                {
                    return GameState.Win;
                }
                else if (this.Matrix[0, i] == player && this.Matrix[1, i] == player && this.Matrix[2, i] == player)
                {
                    return GameState.Win;
                }

                if (this.Matrix[i, 0] == -player && this.Matrix[i, 1] == -player && this.Matrix[i, 2] == -player)
                {
                    return GameState.Lose;
                }
                else if (this.Matrix[0, i] == -player && this.Matrix[1, i] == -player && this.Matrix[2, i] == -player)
                {
                    return GameState.Lose;
                }
            }

            if (this.Matrix[0, 0] == player && this.Matrix[1, 1] == player && this.Matrix[2, 2] == player)
            {
                return GameState.Win;
            }
            else if (this.Matrix[2, 0] == player && this.Matrix[1, 1] == player && this.Matrix[0, 2] == player)
            {
                return GameState.Win;
            }

            if (this.Matrix[0, 0] == -player && this.Matrix[1, 1] == -player && this.Matrix[2, 2] == -player)
            {
                return GameState.Lose;
            }
            else if (this.Matrix[2, 0] == -player && this.Matrix[1, 1] == -player && this.Matrix[0, 2] == -player)
            {
                return GameState.Lose;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.Matrix[i, j] == 0)
                    {
                        return GameState.StillPlaing;
                    }
                }
            }
            return GameState.Draw;
        }


        public override string ToString()
        {
            return String.Join("", Array.ConvertAll(this.Matrix.Cast<int>().ToArray(), el => el.ToString()));
        }
    }
}
