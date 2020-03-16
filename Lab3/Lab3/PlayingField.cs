namespace Lab3
{
    public class PlayingField
    {
        int N = 3;
        public int[,] Field = new int[9, 9];

        public PlayingField()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    Field[i, j] = 0;
                }
            }
        }

        public void GenerateBasicField()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    Field[i, j] = (i*N + i/N + j) % (N*N) + 1;
                }
            }
        }

        public void Transposing()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = i; j < N * N; j++)
                {
                    var temp = Field[i, j];
                    Field[i, j] = Field[j, i];
                    Field[j, i] = temp;
                }
            }
        }

        public void Swap_Rows()
        {
            
        }

    }
}