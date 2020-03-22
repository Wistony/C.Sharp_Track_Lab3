using System;
using System.Text;

namespace Lab3
{
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Security.Authentication.ExtendedProtection;
    using System.Text;
    using Microsoft.VisualBasic;

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
        private void GenerateBasicField()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    Field[i, j] = (i*N + i/N + j) % (N*N) + 1;
                }
            }
        }

        private void Transposing()
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

        private void Swap_Rows()
        {
            var (randomLine1, randomLine2) = Randomizer.Generate_Random_Lines(N);
            for (var j = 0; j < N * N; j++)
            {
                var temp = Field[randomLine1, j];
                Field[randomLine1, j] = Field[randomLine2, j];
                Field[randomLine2, j] = temp;
            }
        }

        private void Swap_Columns()
        {
            var (randomLine1, randomLine2) = Randomizer.Generate_Random_Lines(N);
            for (var j = 0; j < N * N; j++)
            {
                var temp = Field[j, randomLine1];
                Field[j, randomLine1] = Field[j, randomLine2];
                Field[j, randomLine2] = temp;
            }
        }

        private void Swap_Rows_Area()
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
        private void Swap_Columns_Area()
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
        
        private delegate void MyDelegate();
        public void Create_Unique_Field()
        {
            GenerateBasicField();
            
            MyDelegate[] manipulationWithField = new MyDelegate[5];

            manipulationWithField[0] = Transposing;
            manipulationWithField[1] = Swap_Rows;
            manipulationWithField[2] = Swap_Columns;
            manipulationWithField[3] = Swap_Rows_Area;
            manipulationWithField[4] = Swap_Columns_Area;
            
            var rnd = new Random();
            
            for (var i = 0; i < 100; i++)
            {
                var randomAction = rnd.Next(0, 5);
                manipulationWithField[randomAction]();
            }
        }

        public StringBuilder ConvertToString()
        {
            var str = new StringBuilder(N * N * N * N);
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    str.Append(Field[i, j] == 0 ? "." : Field[i, j].ToString());
                }
            }

            return str;
        }

        public void CreatePuzzle()
        {
            int[,] look = new int[N * N, N * N];
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    look[i, j] = 0;
                }
            }

            var iterator = 0;

            while (iterator < 40)
            {
                var rnd = new Random();
                var row = rnd.Next(0, N * N);
                var column = rnd.Next(0, N * N);
                
                if (look[row, column] == 0)
                {
                    look[row, column] = 1;
                    iterator += 1;
                    
                    var temp = Field[row, column];
                    Field[row, column] = 0;
                }
            }
            Console.WriteLine(ConvertToString());
        }
    }
}