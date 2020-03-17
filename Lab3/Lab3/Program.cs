using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = new PlayingField(3);
            field.GenerateBasicField();
            
            OutputPlayingField.ConsoleOutput(field.Field);

            field.Create_Unique_Field();
            OutputPlayingField.ConsoleOutput(field.Field);

            
        }
    }
}