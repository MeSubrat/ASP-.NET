using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal class ThreadLocking
    {
        void Display()
        {
            lock (this)     //By lock this much of code, we are able to execute one thread at a time.
            {
                Console.Write("[C# is an ");
                Thread.Sleep(3000);
                Console.WriteLine("Object Oriented Language.]");
            }
        }
        public static void Main()
        {
            ThreadLocking obj = new ThreadLocking();
            Thread t1 = new Thread(obj.Display);
            Thread t2 = new Thread(obj.Display);
            t1.Start();
            t2.Start();

            Console.ReadLine();
        }
    }
}
