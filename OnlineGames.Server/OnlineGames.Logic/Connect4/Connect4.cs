using OnlineGames.Logic.Connect4.Helper;

namespace OnlineGames.Logic.Connect4
{
    public class Connect4:IConnect4
    {
        private int Depth { get; set; } = 6;
        private int[] TurnsOrder { get; set; } = new int[] { 3, 2, 4, 1, 5, 0, 6 };
        private Dictionary<long, CellCoordinates> Solver { get; set; }= new Dictionary<long, CellCoordinates>();
        public int GetMove(string boardString,int player,int difficulty)
        {
            Depth = difficulty;
            var board = new Board(boardString, 6, 7);
            var bestMove = FillSolver(board,player, 0, -999999999, 999999999, 0, -1, -1);
            return Solver[board.Hash].Col;
        }
        private int FillSolver(Board board, int player, int turnCount, int alpha, int beta, int oldScore, int oldRow, int oldCol)
        {
            var boardId = board.Hash;
            if (Solver.ContainsKey(boardId))
            {
                return Solver[boardId].Score;
            }
            var score = oldScore + board.EvaluateBoard(oldRow, oldCol) + (oldCol == 3 ? player * 6 : 0);
            if (turnCount >= Depth)
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
            Solver[boardId] = new CellCoordinates
            {
                Score = player == 1 ? -999999999 : 999999999
            };

            foreach (var i in TurnsOrder)
            {

                int newY = board.MakeAMove(player, i);
                if (newY != -1)
                {
                    //The move is posible
                    var outcome = FillSolver(board, -player, turnCount + 1, alpha, beta, score, newY, i);
                    if (player == 1)
                    {
                        //First player
                        if (outcome > Solver[boardId].Score)
                        {
                            Solver[boardId].Col = i;
                            Solver[boardId].Score = outcome;
                        }
                        alpha = Math.Max(alpha, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i,player);
                            Solver.Remove(boardId);
                            return 1000;
                        }
                    }
                    else
                    {
                        //Second player
                        if (outcome < Solver[boardId].Score)
                        {
                            Solver[boardId].Col = i;
                            Solver[boardId].Score = outcome;
                        }
                        beta = Math.Min(beta, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i,player);
                            Solver.Remove(boardId);
                            return -1000;
                        }
                    }
                    board.UndoMove(newY, i,player);
                }
            }
            return Solver[boardId].Score;
        }
    }
}