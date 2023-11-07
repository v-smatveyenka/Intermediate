﻿/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private const int itemsCount = 10;
        private static List<int> sharedList = new List<int>();
        private static EventWaitHandle writerEventWaitHandler = new EventWaitHandle(true, EventResetMode.AutoReset);
        private static EventWaitHandle readerEventWaitHandler = new EventWaitHandle(false, EventResetMode.AutoReset);

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            var writer = Task.Factory.StartNew(Write);
            var reader = Task.Factory.StartNew(() => Read(writer));

            try
            {
                Task.WaitAll(writer, reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.InnerException}");
            }

            Console.ReadLine();
        }
        private static void Write()
        {
            for (int i = 0; i < itemsCount; i++)
            {
                readerEventWaitHandler.WaitOne();

                sharedList.Add(i);

                writerEventWaitHandler.Set();
            }
        }

        private static void Read(Task writer)
        {
            while (!writer.IsCompleted)
            {
                writerEventWaitHandler.WaitOne();

                foreach (var item in sharedList)
                {
                    Console.Write($"{item} ");
                }

                Console.WriteLine("\n--------------------");

                readerEventWaitHandler.Set();
            }
        }


    }
}
