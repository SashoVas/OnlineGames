using OnlineGames.Logic.Connect4.Helper;

namespace OnlineGames.Logic.Connect4
{
    public class Connect4:IConnect4
    {
        public int Player1 { get; set; } = 1;
        public int Player2 { get; set; } = -1;
        public int Depth { get; set; } = 6;
        public int[] TurnsOrder { get; set; } = new int[] { 3, 2, 4, 1, 5, 0, 6 };
        public Dictionary<string, CellCoordinates> Solver { get; set; }= new Dictionary<string, CellCoordinates>();
        public int GetMove(string boardString,int player,int difficulty)
        {
            Depth = difficulty;
            var board = new Board(boardString, 6, 7);
            Solver=new Dictionary<string, CellCoordinates>();
            boardString = board.ToString();
            var bestMove = FillSolver(board,player, 0, -999999999, 999999999, 0, -1, -1);
            return Solver[boardString].Col;
        }
        private int FillSolver(Board board, int player, int turnCount, int alpha, int beta, int oldScore, int oldRow, int oldCol)
        {
            var boardString = board.ToString();
            if (Solver.ContainsKey(boardString))
            {
                return Solver[boardString].Score;
            }
            var score = oldScore + board.EvaluateBoard(oldRow, oldCol);
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
            Solver[boardString] = new CellCoordinates
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
                    if (i == 3)
                    {
                        //Scoring center more
                        outcome += player == 1 ? 6 : -6;
                    }
                    if (player == 1)
                    {
                        //First player
                        if (outcome > Solver[boardString].Score)
                        {
                            Solver[boardString].Col = i;
                            Solver[boardString].Score = outcome;
                        }
                        alpha = Math.Max(alpha, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i);
                            Solver.Remove(boardString);
                            return 1000;
                        }
                    }
                    else
                    {
                        //Second player
                        if (outcome < Solver[boardString].Score)
                        {
                            Solver[boardString].Col = i;
                            Solver[boardString].Score = outcome;
                        }
                        beta = Math.Min(beta, outcome);
                        if (alpha >= beta)
                        {
                            //Pruning
                            board.UndoMove(newY, i);
                            Solver.Remove(boardString);
                            return -1000;
                        }
                    }
                    board.UndoMove(newY, i);
                }
            }
            return Solver[boardString].Score;
        }
    }
}