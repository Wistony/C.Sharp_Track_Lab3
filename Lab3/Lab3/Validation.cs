using System;

namespace Lab3
{
    public static class Validation
    {
        //method for input value for make move
        public static int InputValue(string message)
        {
            var flag = true;
            var value = 0;
            while (flag)
            {
                Console.WriteLine(message);
                var str = Console.ReadLine();
                if (NumValidation(str) && str != null)
                {
                    value = int.Parse(str);
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            return value;
        }

        //check number for validy(number must be from 1 to 9)
        private static bool NumValidation(string str)
        {
            var value = int.Parse(str);
            return value > 0 && value < 10;
        }
    }
}