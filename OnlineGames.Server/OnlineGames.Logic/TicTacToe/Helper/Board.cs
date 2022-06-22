namespace OnlineGames.Logic.TicTacToe.Helper
{
    public class Board
    {
        public int[,] Matrix { get; set; }
        public Board(int x, int y) => this.Matrix = new int[x, y];
        public Board(int[,] matrix) => this.Matrix = matrix.Clone() as int[,];
        public Board ReturnCopy() => new Board(this.Matrix);
        public bool MakeAMove(int player, int x, int y)
        {
            if (this.Matrix[x, y] != 0)
            {
                return false;
            }
            this.Matrix[x, y] = player;
            return true;
        }
        public GameState CurrentGameState(int player,int otherPlayer)
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
        public override string ToString() =>
            String.Join("", Array.ConvertAll(this.Matrix.Cast<int>().ToArray(), el => el.ToString()));
    }
}
