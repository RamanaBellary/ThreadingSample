using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingSample
{
    public class MutexEx
    {
        private static Mutex _mutex = new Mutex();
        public static void Do()
        {
            for(int i = 0; i < 5; i++)
            {
                Thread t = new Thread(aquireMutex);
                t.Name = "Thread" + (i + 1);
                t.Start();
            }
        }

        private static void aquireMutex()
        {
            try
            {
                _mutex.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " has aquired the Mutex");
                Thread.Sleep(2000);
                
            }
            finally
            {
                Console.WriteLine(Thread.CurrentThread.Name + " has released the Mutex");
                _mutex.ReleaseMutex();
            }
        }
    }
}
