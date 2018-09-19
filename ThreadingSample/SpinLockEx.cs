using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingSample
{
    public class SpinLockEx
    {
        static SpinLock sl = new SpinLock(true);
        public static void Do()
        {
            try
            {
                bool isLockTaken = false;
                if (!sl.IsHeldByCurrentThread)
                    sl.Enter(ref isLockTaken);
                Console.WriteLine("The lock is currently taken by " + Thread.CurrentThread.Name);
                Thread.Sleep(2000);
            }
            finally
            {
                Console.WriteLine("The lock is released by " + Thread.CurrentThread.Name);
                sl.Exit();
            }
        }
    }
}
