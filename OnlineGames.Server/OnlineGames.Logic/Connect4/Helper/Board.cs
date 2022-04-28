using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.Connect4.Helper
{
    public class Board
    {
        public int[,] Matrix { get; set; }
        public int DimesionX { get => Matrix.GetLength(1) - 1; }
        public int DimesionY { get => Matrix.GetLength(0) - 1; }
        public bool Win { get; set; } = false;
        public bool Lose { get; set; } = false;
        public Board(string boardString, int x, int y)
        {
            this.Matrix = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (boardString[(i * 7) + j] == '0')
                    {
                        continue;
                    }
                    this.Matrix[i, j] = boardString[(i * 7) + j] == '1' ? 1 : -1;
                }
            }
        }
        public Board(int y, int x)
        {
            this.Matrix = new int[y, x];
        }
        public Board(int[,] matrix)
        {
            this.Matrix = matrix.Clone() as int[,];
        }
        public int MakeAMove(int player, int x)
        {
            for (int i = this.DimesionY; i >= 0; i--)
            {
                if (this.Matrix[i, x] == 0)
                {
                    this.Matrix[i, x] = player;
                    return i;
                }
            }
            return -1;

        }
        public void UndoMove(int y, int x)
        {
            this.Matrix[y, x] = 0;
        }
        private int EvaluetePosition(int[] arr)
        {
            int score = 0;
            //4
            if (arr.Count(x => x == 1) == 4)
            {
                Win = true;
                return 100000;
            }
            if (arr.Count(x => x == -1) == 4)
            {
                Lose = true;
                return -100000;
            }
            //3
            if (arr.Count(x => x == 1) == 3 && arr.Count(x => x == 0) == 1)
            {
                score += 10;
            }
            if (arr.Count(x => x == -1) == 3 && arr.Count(x => x == 0) == 1)
            {
                score -= 10;
            }
            //2
            if (arr.Count(x => x == 1) == 2 && arr.Count(x => x == 0) == 2)
            {
                score += 5;
            }
            if (arr.Count(x => x == -1) == 2 && arr.Count(x => x == 0) == 2)
            {
                score -= 5;
            }

            return score;
        }
        public int EvaluateBoard(int row, int col)
        {
            if (row == -1)
            {
                return 0;
            }
            int currentValue = 0;
            //horizontal
            for (int j = Math.Max(0, col - 3); j <= Math.Min(col, DimesionX - 3); j++)
            {
                currentValue += EvaluetePosition(new int[] { Matrix[row, j], Matrix[row, j + 1], Matrix[row, j + 2], Matrix[row, j + 3] });
            }
            //vertical
            for (int i = Math.Max(0, row - 3); i <= Math.Min(row, DimesionY - 3); i++)
            {
                currentValue += EvaluetePosition(new int[] { Matrix[i, col], Matrix[i + 1, col], Matrix[i + 2, col], Matrix[i + 3, col] });
            }
            //right diagonal
            for (int i = 3; i >= 0; i--)
            {
                if (row - i < 0 || col - i < 0)
                {
                    continue;
                }
                if (row + 3 - i > DimesionY || col + 3 - i > DimesionX)
                {
                    break;
                }
                currentValue += EvaluetePosition(new int[] { Matrix[row - i, col - i], Matrix[row + 1 - i, col + 1 - i], Matrix[row + 2 - i, col + 2 - i], Matrix[row + 3 - i, col + 3 - i] });

            }
            //left diagonal
            for (int i = 3; i >= 0; i--)
            {
                if (row - i < 0 || col + i > DimesionX)
                {
                    continue;
                }
                if (row + 3 - i > DimesionY || col - 3 + i < 0)
                {
                    break;
                }
                currentValue += EvaluetePosition(new int[] { Matrix[row - i, col + i], Matrix[row + 1 - i, col - 1 + i], Matrix[row + 2 - i, col - 2 + i], Matrix[row + 3 - i, col - 3 + i] });
            }

            return currentValue;
        }
        public override string ToString()
        {
            return String.Join("", Array.ConvertAll(this.Matrix.Cast<int>().ToArray(), el => el.ToString()));
        }
    }
}
