using System;
using System.Linq;

namespace Lab3
{ 
    class Program
    {
        static void Main(string[] args)
        {
            PlayingField field = new PlayingField(3);
            field.Create_Unique_Field();
            field.CreatePuzzle();
            Sudoku.Print(field.ConvertToString().ToString());
        }
    }
}