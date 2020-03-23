using System;


namespace Lab3
{
    public class Sudoku
    {
        private PlayingField SudokuPuzzle { get; set; }
        
        public Sudoku(int n)
        {
            SudokuPuzzle = new PlayingField(n);
        }

        public void CreatePuzzle()
        {
            SudokuPuzzle.GenerateBasicField();
            SudokuPuzzle.Create_Unique_Field();
            SudokuPuzzle.GeneratePuzzle();
        }

        public void StartGame()
        {
            while (SudokuPuzzle.NotFull())
            {
                ConsoleOutput.Print(SudokuPuzzle.ConvertToString().ToString());
                var i = Validation.InputValue("Enter row number: ");
                var j = Validation.InputValue("Enter column number: ");
                var value = Validation.InputValue("Enter cell value: ");
                if (SudokuPuzzle.CellIsEmpty(i, j))
                {
                    SudokuPuzzle.AddCellValue(i,j,value);
                } 
                
                Console.WriteLine(SudokuPuzzle.IsValidSolution()
                    ? "Congratulations! You solve this sudoku!"
                    : "This solution isn`t valid");
            }
        }

        public SudokuMemento SaveMove(int row, int column, int value)
        {
            return new SudokuMemento(row, column, value);
            
        }
    }
}