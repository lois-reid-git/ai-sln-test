using System;
using System.Threading;

namespace AiCustomTaskScheduler.Worker
{
    /// <summary>
    /// A simple program that simulates work by sleeping for 30 seconds.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working - starting to sleep for 30s...");
            Thread.Sleep(30000);
            Console.WriteLine("Finished sleeping!");
        }
    }
}
