/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            // The value overflow protection wasn't mentioned in the task description. So, no protection was implemented.
            var arraySize = 10;
            var random = new Random();
            var arrayTask = Task.Run(() =>
            {
                var array = new int[arraySize];
                Console.Write("{0,17}", "Original array:");
                for (int i = 0; i < arraySize; i++)
                {
                    array[i] = random.Next(100);
                    Console.Write("{0,12}", array[i] + " ");
                }
                Console.WriteLine();
                return array;
            });
            var multiplyTask = arrayTask.ContinueWith(arrayTaskResult =>
            {
                var array = arrayTaskResult.Result;
                var multiplier = random.Next(100);
                Console.Write("{0,17}", "Multiplied array:");

                for (int i = 0; i < arraySize; i++)
                {
                    array[i] *= multiplier;
                    Console.Write("{0,12}", array[i] + " ");
                }
                Console.WriteLine();

                return array;
            });
            var sortingTask = multiplyTask.ContinueWith(multiplyTaskResult =>
            {
                var array = multiplyTaskResult.Result;
                Array.Sort(array);
                Console.Write("{0,17}", "Sorted array:");

                for (int i = 0; i < arraySize; i++)
                {
                    Console.Write("{0,12}", array[i] + " ");
                }
                Console.WriteLine();
                return array;
            });
            var averageTask = sortingTask.ContinueWith(sortingTaskResult =>
            {
                Console.WriteLine($"Average value: {sortingTaskResult.Result.Average()}");
            });
            averageTask.Wait();

            Console.ReadLine();
        }
    }
}
