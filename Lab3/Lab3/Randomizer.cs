using System;

namespace Lab3
{
    public static class Randomizer
    {
        //choose 2 random line
        public static (int randomLine1, int randomLine2) Generate_Random_Lines(int n)
        {
            var rnd = new Random();
            var area = rnd.Next(0, n);
            var line1 = rnd.Next(0, n);
            var randomLine1 = area * n + line1;

            var line2 = rnd.Next(0, n);
            while (line1 == line2)
            {
                line2 = rnd.Next(0, n);
            }
            var randomLine2 = area * n + line2;
            
            return (randomLine1, randomLine2);
        }

        //choose 2 random box(square 3x3) 
        public static (int area1, int area2) Generate_Random_Areas(int n)
        {
            var rnd = new Random();
            var area1 = rnd.Next(0, n);
            var area2 = rnd.Next(0, n);
            while (area1 == area2)
            {
                area2 = rnd.Next(0, n);
            }

            return (area1, area2);
        }
    }
}