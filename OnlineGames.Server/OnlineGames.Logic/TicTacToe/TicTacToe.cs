using OnlineGames.Logic.TicTacToe.Helper;

namespace OnlineGames.Logic.TicTacToe
{
    public  class TicTacToe:ITicTacToe
    {
        int currentPlayer = 1;
        int otherPlayer = 2;
        public Dictionary<string, CellCoordinates> Solver { get; set; } = new Dictionary<string, CellCoordinates>();

        public TicTacToe()
        {
            FillSolver(new Board(3, 3));
            SwapPlayers();
            FillSolver(new Board(3, 3));
        }

        public CellCoordinates GetMove(string boardString) 
            => Solver[boardString];
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
            var boardString = board.ToString();
            if (Solver.ContainsKey(boardString))
            {
                return Solver[board.ToString()];
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
            Solver[boardString] = new CellCoordinates();
            Solver[boardString].GameState = GameState.StillPlaing;
            int wins = 0;
            int loses = 0;
            int nextWins = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var nextBoard = board.ReturnCopy();
                    if (nextBoard.MakeAMove(currentPlayer, i, j))
                    {
                        SwapPlayers();
                        var outcome = TransformState(FillSolver(nextBoard));
                        SwapPlayers();
                        var currentOutcome = Solver[boardString].GameState;
                        if ((int)outcome.GameState > (int)currentOutcome)
                        {
                            Solver[boardString].GameState = outcome.GameState;
                            Solver[boardString].X = i;
                            Solver[boardString].Y = j;
                            nextWins = outcome.Wins;

                        }
                        else if ((int)outcome.GameState == (int)currentOutcome && outcome.Wins > nextWins)
                        {
                            Solver[boardString].GameState = outcome.GameState;
                            Solver[boardString].X = i;
                            Solver[boardString].Y = j;
                            nextWins = outcome.Wins;
                        }
                        wins += outcome.Wins;
                        loses += outcome.Loses;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return Solver[board.ToString()];
        }
    }
}
