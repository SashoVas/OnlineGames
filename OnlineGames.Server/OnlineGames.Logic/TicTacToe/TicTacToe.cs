using OnlineGames.Logic.TicTacToe.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGames.Logic.TicTacToe
{
    public static class TicTacToe
    {
        public static int Player1 { get; set; } = 1;
        public static int Player2 { get; set; } = -1;
        public static Dictionary<string, CellCoordinates> Solver { get; set; } = new Dictionary<string, CellCoordinates>();
        public static void PlayVsBot(Func<string> inputFunc, int player)
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
                    var solverOutput = Solver[board.ToString()];
                    Console.WriteLine(solverOutput.X + " " + solverOutput.Y);
                    board.MakeAMove(currentPlayer, solverOutput.X, solverOutput.Y);
                }
                Print(board.Matrix);
                gameState = board.CurrentGameState(currentPlayer);
            } while (gameState == GameState.StillPlaing);
            Console.WriteLine(board.CurrentGameState(currentPlayer));
        }
        public static void Print(int[,] board)
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
        public static CellCoordinates CreateSolver(string boardString,int currentPlayer)
        {
            var board = boardString == null ? new Board(3, 3) : new Board(boardString,3,3);
            return FillSolver(board, currentPlayer);
        }

        private static CellCoordinates TransformState(CellCoordinates cellCoordinates)
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
        private static CellCoordinates FillSolver(Board board, int player)
        {
            var boardString = board.ToString();
            if (Solver.ContainsKey(boardString))
            {
                return Solver[board.ToString()];
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
            Solver[boardString] = new CellCoordinates();
            Solver[boardString].GameState = GameState.StillPlaing;
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
