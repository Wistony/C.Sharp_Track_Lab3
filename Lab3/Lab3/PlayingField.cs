namespace Lab3
{
    using System;
    using System.Security.Authentication.ExtendedProtection;

    public class PlayingField
    {
        private int N { get; set; }  //dimension (standart is 3)
        public int[,] Field { get; set; }

        public PlayingField(int n)
        {
            this.N = n;
            Field = new int[n * n, n * n];
            
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
            var (randomLine1, randomLine2) = Randomizer.Generate_Random_Lines(N);
            for (var j = 0; j < N * N; j++)
            {
                var temp = Field[randomLine1, j];
                Field[randomLine1, j] = Field[randomLine2, j];
                Field[randomLine2, j] = temp;
            }
        }

        public void Swap_Columns()
        {
            var (randomLine1, randomLine2) = Randomizer.Generate_Random_Lines(N);
            for (var j = 0; j < N * N; j++)
            {
                var temp = Field[j, randomLine1];
                Field[j, randomLine1] = Field[j, randomLine2];
                Field[j, randomLine2] = temp;
            }
        }

        public void Swap_Rows_Area()
        {
            var (randomArea1, randomArea2) = Randomizer.Generate_Random_Areas(N);
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    var temp = Field[randomArea1 * N + i, j];
                    Field[randomArea1 * N + i, j] = Field[randomArea2 * N + i, j];
                    Field[randomArea2 * N + i, j] = temp;
                }
            }
        }
        
        public void Swap_Columns_Area()
        {
            var (randomArea1, randomArea2) = Randomizer.Generate_Random_Areas(N);
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    var temp = Field[j, randomArea1 * N + i];
                    Field[j, randomArea1 * N + i] = Field[j, randomArea2 * N + i];
                    Field[j, randomArea2 * N + i] = temp;
                }
            }
        }
    }
}