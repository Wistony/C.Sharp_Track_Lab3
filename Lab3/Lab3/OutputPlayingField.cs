using System;

namespace Lab3
{
    public class OutputPlayingField
    {
        private const string Gap = "   -----------------------------------";
        private const string Numeration = "     1  2  3     4  5  6     7  8  9";
        
        public void ConsoleOutput(int [,] array)
        {
            Console.WriteLine(Numeration);
            
            for (var i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    Console.WriteLine(Gap);
                }

                Console.Write($"{i + 1} ");
                
                for (var j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                    {
                        Console.Write("|  ");
                    }
                    
                    Console.Write(array[i, j]);
                    Console.Write("  ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(Gap);
        }
    }
}