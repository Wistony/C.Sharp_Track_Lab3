using System;


namespace Lab3
{
    public class Sudoku
    {
        public PlayingField SudokuPuzzle { get; }
        
        public Sudoku(int n)
        {
            SudokuPuzzle = new PlayingField(n);
        }

        //create unique puzzle
        public void CreatePuzzle()
        {
            SudokuPuzzle.GenerateBasicField();
            SudokuPuzzle.Create_Unique_Field();
            SudokuPuzzle.GeneratePuzzle();
        }
        
        public SudokuMemento SaveState(int row, int column, int value)
        {
            return new SudokuMemento(row, column, value);
        }

        public void RestoreState(SudokuMemento state)
        {
            var i = state.Row;
            var j = state.Column;
            
            SudokuPuzzle.AddCellValue(i,j,0);
        }
    }
}