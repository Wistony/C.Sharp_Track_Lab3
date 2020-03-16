using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var field = new PlayingField();
            field.GenerateBasicField();
            field.Transposing();
            
            OutputPlayingField console = new OutputPlayingField();
            console.ConsoleOutput(field.Field);
        }
    }
}