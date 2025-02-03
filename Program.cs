using System;

namespace Test20250203
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] data = new int[5, 5];
            int size = 5;

            for(int j = 0; j <= size; j++)
                {

                for (int i = 0; i < size - j; i++)
                {
                    Console.Write(' ');
                }

                for (int i = 0; i < j; i++)
                {
                    Console.Write('*');                   
                }

                Console.WriteLine();
            }
        }
    }
}
