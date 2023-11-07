/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static Semaphore _semaphore = new Semaphore(1,10);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            var maxThreads = 10;

            RecursiveDecrementingWithThreadAndJoin(maxThreads);
            RecursiveDecrementingWithPrintingPoolAndSemaphore(maxThreads);

            Console.ReadLine();
        }
        private static void RecursiveDecrementingWithThreadAndJoin(int countOfThreads)
        {
            if (countOfThreads > 0)
            {
                DecrementCount(ref countOfThreads);

                var thread = new Thread(() => RecursiveDecrementingWithThreadAndJoin(countOfThreads));
                thread.Start();
                thread.Join();
            }
        }

        private static void RecursiveDecrementingWithPrintingPoolAndSemaphore(object countOfThreads)
        {
            if (countOfThreads is int count && count > 0)
            {
                _semaphore.WaitOne();

                DecrementCount(ref count);

                ThreadPool.QueueUserWorkItem(new WaitCallback(RecursiveDecrementingWithPrintingPoolAndSemaphore), count);

                _semaphore.Release();
            }
        }

        private static void DecrementCount(ref int count)
        {
            count--;
            Console.WriteLine($"Current count is {count}");
        }
    }
}
