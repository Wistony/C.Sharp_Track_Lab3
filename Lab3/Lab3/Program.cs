using System;
using System.Linq;

namespace Lab3
{
    using System.Reflection.Metadata.Ecma335;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var field = new PlayingField(3);
            field.Create_Unique_Field();
            field.CreatePuzzle();
            Console.WriteLine(field.IsValidSolution());
            while (field.NotFull())
            {
                ConsoleOutput.Print(field.ConvertToString().ToString());
                var i = Validation.InputValue("Enter row number: ");
                var j = Validation.InputValue("Enter column number: ");
                var value = Validation.InputValue("Enter cell value: ");
                if (field.CellIsEmpty(i, j))
                {
                    field.AddCellValue(i,j,value);
                } 
            }

            if (field.IsValidSolution())
            {
                Console.WriteLine("Congratulations! You solve this sudoku!");
            }
            else
            {
                Console.WriteLine("This solution isn`t valid");
            }
        }
    }
}