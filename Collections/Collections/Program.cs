using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayList al = new ArrayList();
            al.Add(200);
            al.Add(300);

            foreach (object obj in al)
            {
                Console.Write(obj+" ");
            }
        }
    }
}
