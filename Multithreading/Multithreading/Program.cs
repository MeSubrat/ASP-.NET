using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithreading
{
    internal class Program
    {
        public static void test1()
        {
            Console.WriteLine("Test 1 Started!");
            for(int i=1;i<=100;i++)
            {
                Console.WriteLine("Test 1: "+i);
            }
            Console.WriteLine("Test 1 Exited!");
        }
        public static void test2()
        {
            Console.WriteLine("Test 2 Started!");
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine("Test 2: " + i);
            }
            Console.WriteLine("Test 2 Exited!");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread Started!");
            Thread t1 = new Thread(test1);
            Thread t2 = new Thread(test2);
            t1.Start();
            t2.Start();
            t1.Join();t2.Join();
            Console.WriteLine("Main thread Exited!");
        }
    }
}
