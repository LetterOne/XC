using System;
using System.Collections.Generic;
using System.Threading;

namespace XC.ConsoleApp
{
    class Program
    {
        static Thread M = null;
        static Thread SubA = null;
        static Thread SubB = null;
        static List<int> CacheA = new List<int>();
        static List<int> CacheB = new List<int>();
        static void Main(string[] args)
        {
            M = new Thread(new ThreadStart(Thread_Main));
            SubA = new Thread(new ThreadStart(Thread_SubA));
            SubB = new Thread(new ThreadStart(Thread_SubB));
            M.Start();
            SubA.Start();
            SubB.Start();
            Console.Read();
        }
        static void Thread_Main()
        {
            Random R = new Random();
            for (int i = 0; i < 100; i++)
            {
                int X = R.Next(10000);
                R = new Random(X);
                if (X % 2 == 1)
                {
                    while (!Monitor.TryEnter(CacheA))
                        ;
                    CacheA.Add(X);
                    Monitor.Exit(CacheA);
                }
                else
                {
                    while (!Monitor.TryEnter(CacheB))
                        ;
                    CacheB.Add(X);
                    Monitor.Exit(CacheB);
                }
            }
        }

        static void Thread_SubA()
        {
            while (true)
            {
                while (!Monitor.TryEnter(CacheA))
                    ;
                if (CacheA.Count > 0)
                {
                    Console.WriteLine("奇数： " + CacheA[0] + " 平方根： " + Math.Sqrt(CacheA[0]));
                    CacheA.RemoveAt(0);
                }
                Monitor.Exit(CacheA);
            }
        }
        static void Thread_SubB()
        {
            while (true)
            {
                while (!Monitor.TryEnter(CacheB))
                    ;
                if (CacheB.Count > 0)
                {
                    Console.WriteLine("偶数： " + CacheB[0] + " 平方根： " + Math.Sqrt(CacheB[0]));
                    CacheB.RemoveAt(0);
                }
                Monitor.Exit(CacheB);
            }
        }
    }


}
