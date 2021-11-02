using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threads_Practice
{
    partial class Program
    {
        //Shared sum to add and subtract from.
        static int sum = 0;
        //Shared lock object to ensure interlock.
        static object _lock = new object();
        static void Main()
        {
            Thread[] threads = new Thread[2];
            for (int i = 0; i < 100; i++)
            {
                //Makes a new thread, starts it and runs Add, Add is locked with Monitor. After it writes the sum in console.
                threads[0] = new Thread(Add);
                threads[0].Start();
                threads[0].Join();
                Console.WriteLine("Adding 2 equals = " + sum);

                //Makes a new thread, starts it and runs Remove, Remove is locked with Monitor. After it writes the sum in console.
                threads[1] = new Thread(Remove);
                threads[1].Start();
                threads[1].Join();
                Console.WriteLine("Subtracting 1 equals =" + sum);
            }
            Console.ReadLine();
        }
        static void Add()
        {
            Monitor.Enter(_lock);
            try
            {
                sum = sum + 2;
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
        static void Remove()
        {
            Monitor.Enter(_lock);
            try
            {
                sum = sum - 1;
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }
    }
}
