using OnlineGames.Logic.TicTacToe.Helper;

namespace OnlineGames.Logic.TicTacToe
{
    public  class TicTacToe:ITicTacToe
    {
        int currentPlayer = 1;
        int otherPlayer = 2;
        private Dictionary<long, CellCoordinates> Solver { get; set; } = new Dictionary<long, CellCoordinates>();
        private Board Board { get; set; }
        public TicTacToe()
        {
            Board = new Board(3, 3);
            FillSolver(Board);
            SwapPlayers();
            Board.Hash = 0;
            FillSolver(Board);
        }

        public CellCoordinates GetMove(string boardString) 
            => Solver[Board.GetBoardHash(boardString)];
        private void SwapPlayers()
        {
            int temp = currentPlayer;
            currentPlayer = otherPlayer;
            otherPlayer = temp;
        }

        private CellCoordinates TransformState(CellCoordinates cellCoordinates)
        {
            var newCellCoordinates = new CellCoordinates();
            if (cellCoordinates.GameState == GameState.Win)
            {
                newCellCoordinates.GameState = GameState.Lose;
            }
            else if (cellCoordinates.GameState == GameState.Lose)
            {
                newCellCoordinates.GameState = GameState.Win;
            }
            else
            {
                newCellCoordinates.GameState = GameState.Draw;
            }

            newCellCoordinates.Wins = cellCoordinates.Loses;
            newCellCoordinates.Loses = cellCoordinates.Wins;
            return newCellCoordinates;
        }
        private CellCoordinates FillSolver(Board board)
        {
            var boardId = board.Hash;
            if (Solver.ContainsKey(boardId))
            {
                return Solver[boardId];
            }
            var currentPosition = board.CurrentGameState(currentPlayer, otherPlayer);
            if (currentPosition != GameState.StillPlaing)
            {
                if (currentPosition == GameState.Lose)
                {
                    return new CellCoordinates
                    {
                        GameState = currentPosition,
                        Loses = 1
                    };
                }
                else
                {
                    return new CellCoordinates
                    {
                        GameState = currentPosition,
                    };
                }

            }
            Solver[boardId] = new CellCoordinates();
            Solver[boardId].GameState = GameState.StillPlaing;
            int wins = 0;
            int loses = 0;
            int nextWins = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board.MakeAMove(currentPlayer, i, j))
                    {
                        SwapPlayers();
                        var outcome = TransformState(FillSolver(board));
                        SwapPlayers();
                        var currentOutcome = Solver[boardId].GameState;
                        if ((int)outcome.GameState > (int)currentOutcome)
                        {
                            Solver[boardId].GameState = outcome.GameState;
                            Solver[boardId].X = i;
                            Solver[boardId].Y = j;
                            nextWins = outcome.Wins;

                        }
                        else if ((int)outcome.GameState == (int)currentOutcome && outcome.Wins > nextWins)
                        {
                            Solver[boardId].GameState = outcome.GameState;
                            Solver[boardId].X = i;
                            Solver[boardId].Y = j;
                            nextWins = outcome.Wins;
                        }
                        wins += outcome.Wins;
                        loses += outcome.Loses;
                        board.UndoMove(i,j, currentPlayer);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Solver[boardId];
        }
    }
}
