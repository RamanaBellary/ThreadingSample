using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingSample
{
    public class SpinWaitEx
    {
        static bool isFree = true;
        static int shareCount = 0;
        public static void Do()
        {
            SpinWait sw = new SpinWait();
            while (!isFree)
            {
                Thread.MemoryBarrier();
                sw.SpinOnce();
            }
            isFree = false;
            Console.WriteLine("Thread is holding the resource :" + Thread.CurrentThread.Name);            
            Thread.Sleep(2000);
            Console.WriteLine("Thread is released the resource :" + Thread.CurrentThread.Name);
            isFree = true;
        }
    }
}
