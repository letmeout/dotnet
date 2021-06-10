using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //DoMoreThings().Wait();

            var times = Enumerable.Range(1, 10);

            foreach (var time in times)
            {
                var list = Enumerable.Range(1, 10).Select(i => new Thing() { WhereCalledFrom = i.ToString() });

                foreach (var item in list)
                {
                    item.DoAction(item.WhereCalledFrom);
                    //if (item.WhereCalledFrom == "5")
                    //{
                    //    Console.WriteLine($"Starting DoSomething{item.WhereCalledFrom} task");
                    //    var task = item.DoSomething(item.WhereCalledFrom);

                    //    Task.Run(() => task);
                    //}

                    Console.WriteLine($"Starting {item.WhereCalledFrom}");
                }

                Thread.Sleep(1000);
            }

            

            //var sw = Stopwatch.StartNew();

            //// 2 tasks will be going simultaneously
            //// asynchronous concurrency - this is appropriate when you have I/O bound code eg http
            //Console.WriteLine("Starting DoSomethingA task");
            //Task<string> doSomethingTaskA = DoSomething("A");

            //Console.WriteLine("Starting DoSomethingB task");
            //var doSomethingTaskB = DoSomething("B");

            //// can do work that doesn't rely on doSomething results
            //Console.WriteLine("doing other stuff");

            //// await suspends the function Main, and can't continue until doSomethingTask completes
            //string resultA = doSomethingTaskA.Result;
            //Console.WriteLine($"resultA is {resultA}");

            //string resultB = doSomethingTaskB.Result;
            //Console.WriteLine($"resultb is {resultB}");

            //Console.WriteLine($"finished in {sw.ElapsedMilliseconds}");
        }

        static async Task DoMoreThings()
        {
            var sw = Stopwatch.StartNew();
            var parameters = new string[] { "A", "B" };
            var things = parameters.Select(i => new Thing() { WhereCalledFrom = i });
            var tasks = new List<Task<string>>();
            var list = new List<string>();

            foreach (var item in things)
            {
                Console.WriteLine($"Starting DoSomething{item.WhereCalledFrom} task");

                tasks.Add(item.DoSomething(item.WhereCalledFrom));
                //list.Add(await DoSomething(item));
            }

            Console.WriteLine("doing other stuff");

            //await Task.WhenAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                var item = await task;
                list.Add(item);
                Console.WriteLine($"result is {item}");
            }

            Console.WriteLine($"finished in {sw.ElapsedMilliseconds}");
        }

        
    }

    public class Thing
    {
        public string WhereCalledFrom { get; set; }
        private static DateTime _lastRunTime = DateTime.MinValue;

        public async Task<string> DoSomething(string whereCalledFrom)
        {
            Console.WriteLine($"inside DoSomething called from {whereCalledFrom}");

            if (whereCalledFrom == "A")
                await Task.Delay(8000);
            await Task.Delay(5000);

            Console.WriteLine($"during {whereCalledFrom}");

            if (whereCalledFrom == "B")
                await Task.Delay(10000);
            await Task.Delay(5000);

            Console.WriteLine($"done {whereCalledFrom}");
            return $"result {whereCalledFrom}";
        }

        public void DoAction(string whereCalledFrom)
        {
            if (WhereCalledFrom == "5")
            {
                if (DateTime.Now >= _lastRunTime.AddDays(1))
                {
                    _lastRunTime = DateTime.Today;
                    Console.WriteLine($"Starting DoSomething{WhereCalledFrom} task");
                    var task = DoSomething(WhereCalledFrom);

                    Task.Run(() => task);
                }
            }
        }
    }
}
