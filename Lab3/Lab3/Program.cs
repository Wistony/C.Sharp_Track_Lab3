using System;

namespace Lab3
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var game = new Sudoku(3);
            var gameHistory = new GameHistory();
            
            while (game.SudokuPuzzle.NotFull())
            {
                ConsoleOutput.Print(game.SudokuPuzzle.ConvertToString().ToString());
                
                var i = Validation.InputValue("Enter row number: ") - 1;
                var j = Validation.InputValue("Enter column number: ") - 1;
                var value = Validation.InputValue("Enter cell value: ");

                if(game.SudokuPuzzle.CellIsEmpty(i, j))
                { 
                    game.SudokuPuzzle.AddCellValue(i, j, value);
                    gameHistory.History.Push(game.SaveState(i,j,value));
                }
                else
                {
                    Console.WriteLine("This cell already has a value");
                }
            }
            
            Console.WriteLine(game.SudokuPuzzle.IsValidSolution()
                ? "Congratulations! You solve this sudoku!"
                : "This solution isn`t valid");
        }
    }
}