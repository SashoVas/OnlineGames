using OnlineGames.Logic.Connect4.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.Connect4
{
    public class Connect4
    {
        public int Player1 { get; set; } = 1;
        public int Player2 { get; set; } = -1;
        public int Depth { get; set; } = 6;
        public int[] TurnsOrder { get; set; } = new int[] { 3, 2, 4, 1, 5, 0, 6 };
        public Dictionary<string, CellCoordinates> Solver { get; set; }
        public Connect4()
        {
            this.Solver = new Dictionary<string, CellCoordinates>();
        }
        public int GetBestMove(string boardString,int player)
        {
            var board = new Board(boardString, 6, 7);
            boardString = board.ToString();
            var bestMove = FillSolver(board,player, 0, -999999999, 999999999, 0, -1, -1);
            return this.Solver[boardString].Col;
        }
        public void TestWithBoard(int[,] boardArr)
        {
            var board = new Board(boardArr);
            Print(board.Matrix, board.DimesionX, board.DimesionY);
            this.Solver = new Dictionary<string, CellCoordinates>();
            var score = FillSolver(board, 1, 0, -999999999, 999999999, 0, -1, -1);
            var solverOutput = this.Solver[board.ToString()];
            Console.WriteLine(score);
            board.MakeAMove(1, solverOutput.Col);
            Print(board.Matrix, board.DimesionX, board.DimesionY);
        }
        public void PlayVsBot(Func<int> inputFunc, int player)
        {
            var board = new Board(6, 7);
            int currentPlayer = player;
            Print(board.Matrix, board.DimesionX, board.DimesionY);
            do
            {
                currentPlayer = -currentPlayer;
                if (currentPlayer == 1)
                {
                    int position = inputFunc();
                    board.MakeAMove(currentPlayer, position);
                }
                else
                {
                    this.Solver = new Dictionary<string, CellCoordinates>();
                    var score = FillSolver(board, currentPlayer, 0, -999999999, 999999999, 0, -1, -1);
                    var solverOutput = this.Solver[board.ToString()].Col;
                    Console.WriteLine(this.Solver[board.ToString()].Score);
                    board.MakeAMove(currentPlayer, solverOutput);
                }
                Print(board.Matrix, board.DimesionX, board.DimesionY);
            } while (true);
        }
        public void Print(int[,] board, int boardX, int boardY)
        {
            var result1 = "|";
            for (int i = 1; i < 8; i++)
            {
                result1 += i + "|";
            }
            Console.WriteLine(result1);
            var result = "|";
            for (int i = 0; i <= boardY; i++)
            {
                for (int j = 0; j <= boardX; j++)
                {
                    if (board[i, j] == 0)
                    {
                        result += "-|";
                        continue;
                    }
                    result += (board[i, j] == 1 ? "O" : "X") + "|";
                }
                result += "\n|";
            }
            Console.WriteLine(result);
        }
        private int FillSolver(Board board, int player, int turnCount, int alpha, int beta, int oldScore, int oldRow, int oldCol)
        {
            var boardString = board.ToString();
            if (this.Solver.ContainsKey(boardString))
            {
                return this.Solver[boardString].Score;
            }
            var score = oldScore + board.EvaluateBoard(oldRow, oldCol);
            if (turnCount >= this.Depth)
            {
                board.Win = false;
                board.Lose = false;
                return score;
            }
            if (board.Win)
            {
                board.Win = false;
                board.Lose = false;
                return 100000;
            }
            if (board.Lose)
            {
                board.Win = false;
                board.Lose = false;
                return -100000;
            }
            this.Solver[boardString] = new CellCoordinates
            {
                Score = player == 1 ? -999999999 : 999999999
            };

            foreach (var i in this.TurnsOrder)
            {

                int newY = board.MakeAMove(player, i);
                if (newY != -1)
                {
                    //The move is posible
                    var outcome = FillSolver(board, -player, turnCount + 1, alpha, beta, score, newY, i);
                    if (i == 3)
                    {
                        //Scoring center more
                        outcome += player == 1 ? 6 : -6;
                    }
                    if (player == 1)
                    {
                        //First player
                        if (outcome > this.Solver[boardString].Score)
                        {
                            this.Solver[boardString].Col = i;
                            this.Solver[boardString].Score = outcome;
                        }
                        alpha = Math.Max(alpha, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i);
                            this.Solver.Remove(boardString);
                            return 1000;
                        }
                    }
                    else
                    {
                        //Second player
                        if (outcome < this.Solver[boardString].Score)
                        {
                            this.Solver[boardString].Col = i;
                            this.Solver[boardString].Score = outcome;
                        }
                        beta = Math.Min(beta, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i);
                            this.Solver.Remove(boardString);
                            return -1000;
                        }
                    }
                    board.UndoMove(newY, i);
                }
            }
            return this.Solver[boardString].Score;
        }

    }
}
