using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = new PlayingField(3);
            field.GenerateBasicField();
            
            var console = new OutputPlayingField();
            console.ConsoleOutput(field.Field);
            
            MyDelegate[] func = new MyDelegate[5];

            func[0] = field.Transposing;
            func[1] = field.Swap_Rows;
            func[2] = field.Swap_Columns;
            func[3] = field.Swap_Rows_Area;
            func[4] = field.Swap_Columns_Area;
            
            console.ConsoleOutput(field.Field);
        }
    }
    
    delegate void MyDelegate();

}