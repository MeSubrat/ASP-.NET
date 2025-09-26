using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class HashCollection
    {
        static void Main()
        {
            Hashtable ht = new Hashtable();
            ht.Add("name", "Subrat");
            ht.Add("age", 22);
            ht.Add("email", "subrat@gmail.com");
            //Console.WriteLine(ht["name"]);
            foreach(object key in ht.Keys)
            {
                Console.WriteLine(key +" : " + ht[key]);
            }
        }
    }
}
