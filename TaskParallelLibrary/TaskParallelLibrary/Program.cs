using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Task parallel Library helps to improve application performance by using the undergoing resources at their best
            //Usually a particular task can be done by a thread, but if we have a bunch of tasks which are independent to eachother and not need to be order then Parallelism helps a lot to complete that bunch of tasks within less time
            //Lets see the working functionality of a static method Paralle.For by comparing with for loop

            //ParallelForLoop.WorkingNature();
            //ParallelForLoop.DegreeOfParallelism();

            //Lets see the working functionality of a static method Parallel.Foreach by comparing with foreach loop
            //ParallelForEach.WorkingNature();

            //If we have independent methods to be done parallely then we can use Parallel.Invoke method, lets see its functionality            
            //ParallelInvoke.WorkingNature();

            //Parallel LINQ can be used to do actions on collections in parallel. Lets see how PLINQ can be more faster than normal LINQ
            ParallelLinq.WorkingNature();
        }

        private class ParallelForLoop
        {
            internal static void DegreeOfParallelism()
            {
                //Here we will see how we can control the number of threads can be used for a Parallel.For method.
                Console.WriteLine("ParallelFor with Options");
                var options = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
                Parallel.For(1, 10, options, (number) =>
                {
                    var result = DoSomeBigWork();
                    Console.WriteLine("ParallelFor loop in {0}th iteration and Thread is {1}",number, Thread.CurrentThread.ManagedThreadId);
                });
                Console.WriteLine("ParallelFor completed");
                //Here you can notice that this loop is restricted to use two threads only
            }

            internal static void WorkingNature()
            {
                //Lets start with For loop
                Console.WriteLine("For Loop Execution started");
                for(int i=1;i<=10;i++)
                {
                    int result = DoSomeBigWork();
                    Console.WriteLine("The iteration numer is {0} and value is {1} and Thread used is {2}", i, result, Thread.CurrentThread.ManagedThreadId);
                }
                Console.WriteLine("For Loop execution completed");

                //Now lets the functionality of Parallel.For loop
                Console.WriteLine();
                Console.WriteLine("ParallelFor loop execution started");
                Parallel.For(1, 10, (number) =>
                {
                    int result = DoSomeBigWork();
                    Console.WriteLine("The iteration numer is {0} and value is {1} and Thread used is {2}", number, result, Thread.CurrentThread.ManagedThreadId);
                });
                Console.WriteLine("ParallelFor loop execution is completed");
                //If you can notice in the result that ParallelFor loop uses different threads but normal for loop uses one thread only, this makes parallel thread to be faster

                //Now lets check the time taken by for and parallelfor loops
                Stopwatch stopwatch = Stopwatch.StartNew();
                Console.WriteLine() ;
                Console.WriteLine("For loop started");
                for(int i = 1; i <= 10; i++) { 
                    int result = DoSomeBigWork();
                }
                stopwatch.Stop();
                Console.WriteLine("For loop is completed and time taken is {0}", stopwatch.ElapsedMilliseconds);

                Console.WriteLine();
                stopwatch = Stopwatch.StartNew();
                Console.WriteLine();
                Console.WriteLine("ParallelFor loop started");
                Parallel.For(1, 10, (number) =>
                {
                    int result = DoSomeBigWork();
                });
                stopwatch.Stop();
                Console.WriteLine("ParallelFor loop is completed and time taken is {0}", stopwatch.ElapsedMilliseconds);

                //here by using multiple threads PallelFor loop takes much lesser time than for loop
            }

            private static int DoSomeBigWork()
            {
                int result = 0;
                for(int i = 0; i < 100000000; i++)
                {
                    result++;
                }
                return result;
            }
        }

        private class ParallelForEach
        {
            internal static void WorkingNature()
            {
                //Lets check the working nature of Parallel.ForEach loop by comparing with foreach loop
                Stopwatch stopwatch = Stopwatch.StartNew();
                var items = Enumerable.Range(0, 10).ToList();
                //Foreach loop is starting
                Console.WriteLine("Normal foreach loop execution is starting");
                foreach(var item in items)
                {
                    var result = DoSomeTimeTakingWork();
                    Console.WriteLine("{0} - {1} with Thread {2}", item, result, Thread.CurrentThread.ManagedThreadId);
                }
                stopwatch.Stop();
                Console.WriteLine("Normal foreach loop execution completed and time taken is {0}", stopwatch.ElapsedMilliseconds);

                Console.WriteLine();
                stopwatch = Stopwatch.StartNew();                
                //Foreach loop is starting
                Console.WriteLine("Parallel foreach loop execution is starting");
                Parallel.ForEach(items, (number) =>
                {
                    var result = DoSomeTimeTakingWork();
                    Console.WriteLine("{0} - {1} with Thread {2}", number, result, Thread.CurrentThread.ManagedThreadId);
                });
                stopwatch.Stop();
                Console.WriteLine("Parallel foreach loop execution completed and time taken is {0}", stopwatch.ElapsedMilliseconds);

                //Here you can notice that Parallel.ForEach loop takes less time than norma foreach loop
            }

            private static int DoSomeTimeTakingWork()
            {
                var result = 0;
                for(int i = 0; i < 1000000000; i++)
                {
                    result++;
                }
                return result;
            }
        }

        private class ParallelInvoke
        {
            internal static void WorkingNature()
            {
                //To learn about this method we should have few methods in hand, the following Method1, Method2 and Method3 are the functions which are independent in this case
                //Lets executive these methods without parallelism mesn executing in sequential order
                
                Stopwatch stopwatch = Stopwatch.StartNew();
                Console.WriteLine("Testing for the time taken for Methods are started");
                Method1();
                Method2();
                Method3();
                stopwatch.Stop();
                Console.WriteLine("The time taken to complete all these three methods is {0}",stopwatch.ElapsedMilliseconds);

                //Lets start executing three methods in parallelly with Parallel.Invoke
                Console.WriteLine("Testing for the time taken for Methods in parallel are started");
                Console.WriteLine();
                stopwatch = new Stopwatch();
                stopwatch.Start();
                Parallel.Invoke(
                    Method1, Method2, Method3
                    );
                stopwatch.Stop();
                Console.WriteLine("The time taken to complete all these three methods in parallel is {0}", stopwatch.ElapsedMilliseconds);
            }

            private static void Method3()
            {
                Thread.Sleep(1000);
                Console.WriteLine("Execution of Method3 is Completed and Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            }

            private static void Method2()
            {
                Thread.Sleep(500);
                Console.WriteLine("Execution of Method2 is Completed and Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            }

            private static void Method1()
            {
                Thread.Sleep(600);
                Console.WriteLine("Execution of Method1 is Completed and Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            }
        }

        private class ParallelLinq
        {
            internal static void WorkingNature()
            {
                //Lets query a list with LINQ
                var numbers = Enumerable.Range(1, 20).ToList();
                Console.WriteLine("Querying the list of numbers with normal LINQ");
                var EvenNumbers = numbers.Where(x => x%2 == 0).ToList();

                foreach (var number in EvenNumbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine("Normal LINQ execution is completed");

                //Lets see the working of PLINQ in the same situation
                Console.WriteLine();
                Console.WriteLine("PLinq working is started");
                var EvenNumbersWithPLinq = numbers.AsParallel().Where(x => x%2 ==0).ToList();
                foreach (var number in EvenNumbersWithPLinq)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine("Normal LINQ execution is completed");

                //Here you can notice that the result is not in sequence as we are doing Parallel Linq
                //As the above list is smaller we cant notice the difference, lets take a bigger list to see the difference b/w LINQ and PLINQ
                var random = new Random();
                var listOfNumbers = Enumerable.Range(1, 99999999).Select(x => random.Next(1,1000)).ToList();
                Console.WriteLine("Normal LINQ working is started");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var linqMax = listOfNumbers.Max();
                var linqMin = listOfNumbers.Min();
                var linqAverage = listOfNumbers.Average();
                stopwatch.Stop();
                var linqTimeMS = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"\nMax : {linqMax}, \nMin : {linqMin}, \nAverage = {linqAverage}, \nTimeTaken is {linqTimeMS}");

                Console.WriteLine();
                Console.WriteLine("PLINQ working is started");                
                stopwatch.Restart();
                var pLinqMax = listOfNumbers.AsParallel().Max();
                var pLinqMin = listOfNumbers.AsParallel().Min();
                var pLinqAverage = listOfNumbers.AsParallel().Average();
                stopwatch.Stop();
                var pLinqTimeMS = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"\nMax : {pLinqMax}, \nMin : {pLinqMin}, \nAverage = {pLinqAverage}, \nTimeTaken is {pLinqTimeMS}");

                //Here you can clearly observe that PLINQ is working faster than normal LINQ
            }
        }
    }
}