namespace OnlineGames.Logic.TicTacToe.Helper
{
    public class Board
    {
        public int[,] Matrix { get; set; }
        public List<long> PlayerOneNums { get; set; } = new List<long>();
        public List<long> PlayerTwoNums { get; set; } = new List<long>();
        public long Hash { get; set; }
        public Board(int x, int y)
        {
            this.Matrix = new int[x, y];
            GenerateHashValues();
        }
        public long GetBoardHash(string boardString)
        {
            long hash = 0;
            for (int i = 0; i < boardString.Length; i++)
            {
                if (boardString[i] == '0')
                {
                    continue;
                }
                if (boardString[i] == '1')
                {
                    hash = hash ^ PlayerOneNums[i];
                }
                else
                {
                    hash = hash ^ PlayerTwoNums[i];
                }
            }
            return hash;
        }
        private void GenerateHashValues()
        {
            var r = new Random();
            for (int i = 0; i < 3 * 3; i++)
            {
                PlayerOneNums.Add(r.NextInt64());
                PlayerTwoNums.Add(r.NextInt64());
            }
        }
        public bool MakeAMove(int player, int x, int y)
        {
            if (this.Matrix[x, y] != 0)
            {
                return false;
            }
            this.Matrix[x, y] = player;
            if (player == 1)
            {
                Hash = Hash ^ PlayerOneNums[(x * 3) + y];
            }
            else
            {
                Hash = Hash ^ PlayerTwoNums[(x * 3) + y];
            }
            return true;
        }
        public void UndoMove(int x, int y, int player)
        {
            this.Matrix[x, y] = 0;
            if (player == 1)
            {
                Hash = Hash ^ PlayerOneNums[(x * 3) + y];
            }
            else
            {
                Hash = Hash ^ PlayerTwoNums[(x * 3) + y];
            }
        }
        public GameState CurrentGameState(int player, int otherPlayer)
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

                if (this.Matrix[i, 0] == otherPlayer && this.Matrix[i, 1] == otherPlayer && this.Matrix[i, 2] == otherPlayer)
                {
                    return GameState.Lose;
                }
                else if (this.Matrix[0, i] == otherPlayer && this.Matrix[1, i] == otherPlayer && this.Matrix[2, i] == otherPlayer)
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

            if (this.Matrix[0, 0] == otherPlayer && this.Matrix[1, 1] == otherPlayer && this.Matrix[2, 2] == otherPlayer)
            {
                return GameState.Lose;
            }
            else if (this.Matrix[2, 0] == otherPlayer && this.Matrix[1, 1] == otherPlayer && this.Matrix[0, 2] == otherPlayer)
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
    }
}
