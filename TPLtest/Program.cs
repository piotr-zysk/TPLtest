using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TPLtest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            for (var i=1;i<10;i++)
            {
                DoJob(i);
            }
            */
            int NumberOfJobs = 100;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Task[] tasks = new Task[NumberOfJobs];

            for (var i = 1; i <= NumberOfJobs; i++)
            {
                var jobNr = i;
                tasks[i - 1] = Task.Factory.StartNew(() => new Jobs().DoJob(jobNr));
            }

            Task.WaitAll(tasks);

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            

            Console.WriteLine($"Total job time: {Jobs.MilisecondsCounter} ms. Execution time: {Math.Round(ts.TotalMilliseconds)} ms.");

        }

    }

    class Jobs
    { 
        public static int MilisecondsCounter=0;

        public void DoJob(int jobNr)
        {
            var rnd = new Random();
            var sleepTime = rnd.Next(100);
            Thread.Sleep(sleepTime);
            Console.WriteLine($"Job {jobNr} took {sleepTime} miliseconds;");
            MilisecondsCounter += sleepTime;
        }

    }


    
}
