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

            int totalJobMiliseconds = Jobs.MilisecondsCounter;
            int totalExecutionMiliseconds = (int)Math.Round(ts.TotalMilliseconds);
            int totalSavingsPercent = (int)Math.Round((decimal)100*(totalJobMiliseconds - totalExecutionMiliseconds) / totalJobMiliseconds);

            Console.WriteLine($"Total job time: {totalJobMiliseconds} ms. Execution time: {totalExecutionMiliseconds} ms. Savings: {totalSavingsPercent} %.");

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
