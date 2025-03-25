using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L20250324_Thread
{
    class Program
    {
        static Object _lock = new Object(); //동기화 객체
        static SpinLock spinLock = new SpinLock();

        //atomic, 공유영역 작업은 원자성, 중간 끊지 말라고
        //나 끝날 때까지 다 하지마
        volatile static int Money = 0; //volatile 최적화 하지 말라 순서 뒤집지 말라 c#에선 쓰지 말자... 멀티 쓰레드도
        
        static bool lockTaken = false;

        static void Add()
        {
            int Gold = 0;
            for (int i = 0; i < 100000; ++i)
            {
                //Interlocked.Increment(ref Money); //가벼워서 더 빠름
                //spinLock.Enter(ref lockTaken);
                lock (_lock) //UserMode, Kernel Mode                
                {
                    Money++;
                //    //int temp = Money;
                //    //temp = temp + 1;
                //    //Money = temp;
                }
                //spinLock.Exit();

                Gold++;
            }
        }
        static void Remove()
        {
            int Gold = 0;
            for (int i = 0; i < 100000; ++i)
            {
                //Interlocked.Decrement(ref Money);
                //spinLock.Enter(ref lockTaken);
                lock (_lock)                
                {
                    Money--;
                //    //int temp = Money;
                //    //temp = temp - 1;
                //    //Money = temp;
                }
                //spinLock.Exit();
                Gold--;
            }
        }

        static void B()
        {
            for (int i = 0; i < 100; ++i)
            {
                Console.WriteLine("B");
            }
        }

        //foreground, main thread 종료되면 나머지 쓰레드는 다 종료된다.
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));

            //B함수 따로 실행 시켜줘 (Thread) -> OS 부탁
            thread1.IsBackground = true;
            thread1.Start();

            thread2.IsBackground = true;
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine(Money);
            
        }
    }
}
