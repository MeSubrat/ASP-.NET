using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal class ThreadPriority
    {
        static long count1, count2;
        void IncrementCount1()
        {
            while (true)
                count1 += 1;
        }
        void IncrementCount2()
        {
            while (true)
                count2 += 2;
        }
        public static void Main()
        {
            ThreadPriority obj = new ThreadPriority();
            Thread t1 = new Thread(obj.IncrementCount1);
            Thread t2 = new Thread(obj.IncrementCount2);

            t1.Priority = System.Threading.ThreadPriority.Lowest;
            t2.Priority = System.Threading.ThreadPriority.Highest;

            t1.Start();t2.Start();

            Console.WriteLine("Main thread going to sleep");
            Thread.Sleep(5000);
            Console.WriteLine("Main Thread Woke Up");

            Console.WriteLine("Count1 : "+count1);
            Console.WriteLine("Count2 : " + count2);
            t1.Abort();
            t2.Abort();
        }
    }
}
