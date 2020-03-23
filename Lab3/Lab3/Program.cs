using System;

namespace Lab3
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var game = new Sudoku(3);
            var gameHistory = new GameHistory();
            game.CreatePuzzle();
            
            var flag = true;
            while (flag)
            {
                ConsoleOutput.Print(game.SudokuPuzzle.ConvertToString().ToString());

                Console.WriteLine(" 1)Continue game");
                Console.WriteLine(" 2)Undo");
                Console.WriteLine(" 3)Exit");
                var str = Console.ReadLine();
                var choice = int.Parse(str);
                switch (choice)
                {
                    case 1:
                    {
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
                        flag = game.SudokuPuzzle.NotFull();
                        break;
                    }
                    case 2:
                    {
                        if (!gameHistory.IsEmpty())
                        {
                            game.RestoreState(gameHistory.Undo());
                            Console.WriteLine("One move canceled");
                        }
                        else
                        {
                            Console.WriteLine("Game history is empty");
                        }
                        flag = game.SudokuPuzzle.NotFull();
                        break;
                    }
                    case 3:
                    {
                        flag = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Enter number from 1 to 3");
                        flag = game.SudokuPuzzle.NotFull();
                        break;
                    }
                }
            }
            
            Console.WriteLine(game.SudokuPuzzle.IsValidSolution()
                ? "Congratulations! You solve this sudoku!"
                : "This solution isn`t valid");
        }
    }
}