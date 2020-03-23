using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace Lab3
{
    public class PlayingField
    {
        private int N { get; set; } //dimension (standart is 3)
        public int[,] Field { get; set; }
        private delegate void UniqueField();


        public PlayingField(int n)
        {
            N = n;
            Field = new int[N * N, N * N];

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
                    Field[i, j] = (i * N + i / N + j) % (N * N) + 1;
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
        public void Create_Unique_Field()
        {
            UniqueField[] manipulationWithField = new UniqueField[5];

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

        public void GeneratePuzzle()
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
            while (iterator < 35)
            {
                var rnd = new Random();
                var row = rnd.Next(0, N * N);
                var column = rnd.Next(0, N * N);
                if (look[row, column] == 0)
                {
                    look[row, column] = 1;
                    iterator += 1;

                    Field[row, column] = 0;
                }
            }
        }
        
        
        public bool NotFull()
        {
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    if (Field[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CellIsEmpty(int i, int j)
        {
            return Field[i, j] == 0;
        }

        public void AddCellValue(int i, int j, int value)
        {
            Field[i, j] = value;
        }

        public bool IsValidSolution()
        {
            return IsValidRow() && IsValidColumn() && IsValidBox();
        }

        private bool IsValidRow()
        {
            var sum = 0;
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    sum += Field[i, j];
                }
                if (sum != 45)
                {
                    return false;
                }
                sum = 0;
            }
            return true;
        }

        private bool IsValidColumn()
        {
            var sum = 0;
            for (var i = 0; i < N * N; i++)
            {
                for (var j = 0; j < N * N; j++)
                {
                    sum += Field[j, i];
                }
                if (sum != 45)
                {
                    return false;
                }
                sum = 0;
            }
            return true;
        }
        
        //Check all 9 square 3x3 for validy
        private bool IsValidBox() 
        {
            var sum = 0;
            for (var l = 0; l < N; l++)
            {
                for (var k = 0; k < N; k++)
                {
                    for (var i = 0; i < N; i++)
                    {
                        for (var j = 0; j < N; j++)
                        {
                            sum += Field[i + N * k, j + N * l];
                        }
                    }
                    if (sum != 45)
                    {
                        return false;
                    }
                    sum = 0;
                }
            }
            return true;
        }


    }
}