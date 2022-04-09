using OnlineGames.Logic.TicTacToe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.TicTacToe
{
    public class TicTacToe
    {
        public int Player1 { get; set; } = 1;
        public int Player2 { get; set; } = -1;
        public Dictionary<string, CellCoordinates> Solver { get; set; }
        public TicTacToe()
        {
            this.Solver = new Dictionary<string, CellCoordinates>();
        }
        public void PlayVsBot(Func<string> inputFunc, int player)
        {
            var board = new Board(3, 3);
            int currentPlayer = -player;
            var gameState = GameState.StillPlaing;
            Print(board.Matrix);
            do
            {
                currentPlayer = -currentPlayer;
                if (currentPlayer == -1)
                {
                    var playerInput = inputFunc();
                    var positions = Array.ConvertAll(playerInput.Split(','), pos => int.Parse(pos));
                    board.MakeAMove(currentPlayer, positions[0], positions[1]);

                }
                else
                {
                    var solverOutput = this.Solver[board.ToString()];
                    Console.WriteLine(solverOutput.X + " " + solverOutput.Y);
                    board.MakeAMove(currentPlayer, solverOutput.X, solverOutput.Y);
                }
                Print(board.Matrix);
                gameState = board.CurrentGameState(currentPlayer);
            } while (gameState == GameState.StillPlaing);
            Console.WriteLine(board.CurrentGameState(currentPlayer));
        }
        public void Print(int[,] board)
        {
            var result = "|";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result += board[i, j].ToString() + "|";
                }
                result += "\n---\n|";
            }
            Console.WriteLine(result);
        }
        public CellCoordinates CreateSolver(string boardString,int currentPlayer)
        {
            var board = boardString == null ? new Board(3, 3) : new Board(boardString,3,3);
            return FillSolver(board, currentPlayer);
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
        private CellCoordinates FillSolver(Board board, int player)
        {
            var boardString = board.ToString();
            if (this.Solver.ContainsKey(boardString))
            {
                return this.Solver[board.ToString()];
            }
            var currentPosition = board.CurrentGameState(player);
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
            this.Solver[boardString] = new CellCoordinates();
            this.Solver[boardString].GameState = GameState.StillPlaing;
            int wins = 0;
            int loses = 0;
            int nextWins = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //TODO:break if win
                    var nextBoard = board.ReturnCopy();
                    if (nextBoard.MakeAMove(player, i, j))
                    {
                        var outcome = TransformState(FillSolver(nextBoard, -player));
                        var currentOutcome = this.Solver[boardString].GameState;
                        if ((int)outcome.GameState > (int)currentOutcome)
                        {
                            this.Solver[boardString].GameState = outcome.GameState;
                            this.Solver[boardString].X = i;
                            this.Solver[boardString].Y = j;
                            nextWins = outcome.Wins;

                        }
                        else if ((int)outcome.GameState == (int)currentOutcome && outcome.Wins > nextWins)
                        {
                            this.Solver[boardString].GameState = outcome.GameState;
                            this.Solver[boardString].X = i;
                            this.Solver[boardString].Y = j;
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


            return this.Solver[board.ToString()];
        }
    }
}
