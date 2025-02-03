using System;
using System.Data;
using System.Threading.Channels;

namespace Test20250203
{
    //internal class Program
    //{
    //    //전역함수
    //    static int[,] data = new int[10, 10];
    //    static void Initialze() //PascalCase
    //    {
    //        int number = 1;

    //        for (int i = 0; i < 10; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                data[i, j] = number++;
    //            }

    //        }
    //    }

    //    static void Print()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                Console.Write(data[i, j].ToString() + "\t");
    //            }
    //            Console.WriteLine();

    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        int d; 지역변수 여기에서만 씀
    //        string s = "Hello World";
    //        Console.WriteLine(s);

    //        int[,] data = new int[10, 10];
    //        초기화한다
    //        int number = 1;
    //        for (int i = 0; i < 10; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                data[i, j] = number++;
    //            }
    //        }
    //        Initialze();
    //        출력한다
    //        for (int i = 0; i < 10; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                Console.Write(data[i, j].ToString() + "\t");
    //            }
    //            Console.WriteLine();
    //        }
    //        Print();


    //        별찍기
    //        int size = 5;
    //        for (int j = 0; j <= size; j++)
    //        {
    //            for (int i = 0; i < size - j; i++)
    //            {
    //                Console.Write(' ');
    //            }
    //            for (int i = 0; i < j; i++)
    //            {
    //                Console.Write('*');
    //            }
    //            Console.WriteLine();
    //        }
    //    }
    //}

    internal class program01
    {
        //static 반환형 함수명 (자료형 인자1, 자료형 인자2, ...)
        //{
        //   return 자료 반환;
        //}

        //static int Plus3(int number, int number2)
        //{
        //    return number + number2;
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(Plus3(2, 3));
        //}        

        //static string Eat(string fruit)
        //{
        //    return fruit + "을/를 먹다";
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(Eat("사과"));
        //    Console.WriteLine(Eat("오렌지"));
        //}

        //함수로 게임 만들기
        static char wall = '*';
        static char floor = ' ';
        static int playerX = 1;
        static int playerY = 1;

        static int[, ] map =
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 4, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },         
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }

        };
        static ConsoleKeyInfo keyInfo;
        static bool IsRunning = true;       
        static void Main(string[] args)
        {
            while (IsRunning)
            {               
                Input();
                Update();
                Render();
            }
            Console.Clear();
            Console.WriteLine("Game over");
        }
        private static void Render()
        {
            Console.Clear();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write('P');
                    }
                    else if (map[y, x] == 1)
                    {
                        Console.Write(wall);
                    }
                    else if (map[y, x] == 0)
                    {
                        Console.Write(floor);
                    }
                    else if (map[y, x] == 4)
                    {
                        Console.Write('M');
                    }
                }
                Console.Write('\n');
            }
        }
        private static void Update()
        {
            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                playerY--;
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                playerY++;
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                playerX--;
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                playerX++;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                IsRunning = false;
            }
        }
        private static void Input()
        {
            keyInfo = Console.ReadKey();
        }
    }
}
