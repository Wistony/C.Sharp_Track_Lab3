using System;

namespace Lab3
{
    public class Validation
    {
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
        
        private static bool NumValidation(string str)
        {
            var value = int.Parse(str);
            return (value > 0 && value < 10) ? true : false;
        }
    }
}