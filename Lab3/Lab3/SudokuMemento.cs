using System.Collections.Generic;


namespace Lab3
{
    using System;

    public class SudokuMemento
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Value { get; private set; }

        public SudokuMemento(int row, int column, int value)
        {
            Row = row;
            Column = column;
            Value = value;
        }
    }
    
    public class GameHistory
    {
        public Stack<SudokuMemento> History { get; private set; }
        public GameHistory()
        {
            History = new Stack<SudokuMemento>();
        }
        public SudokuMemento Undo()
        {
            return History.Pop();
        }
        public bool IsEmpty()
        {
            return History.Count == 0;
        }
        
    }
}