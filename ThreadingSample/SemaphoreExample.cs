using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingSample
{
    public class SemaphoreExample
    {
        public static void Do()
        {
            ////Example1
            //Invoke1();


            ////Example2
            //Invoke2();
        }

        #region Example1
        static Semaphore _semaphore = new Semaphore(2, 2);
        private static void Invoke1()
        {
            for(int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(sharedMethod);
                t.Name = "Thread" + i;
                t.Start();
            }
        }

        private static void sharedMethod()
        {
            try
            {
                _semaphore.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " has aquired the Semaphore");
                Thread.Sleep(10000);                
            }
            finally
            {
                Console.WriteLine(Thread.CurrentThread.Name + " has released the Semaphore");
                _semaphore.Release();
            }

        }
        #endregion

        #region Example2
        private static void Invoke2()
        {
            var lstTasks = new List<Task>();
            Task t1 = new Task(InvokeSempahore1);
            lstTasks.Add(t1);
            Task t2 = new Task(InvokeSempahore2);
            lstTasks.Add(t2);
            t1.Start();
            Thread.Sleep(1000);
            t2.Start();
            Task.WaitAll(lstTasks.ToArray());
        }

        private static void InvokeSempahore1()
        {
            bool isSemaphoreCreate;
            Semaphore s1 = new Semaphore(1, 1, "MySemaphore", out isSemaphoreCreate);

            if (isSemaphoreCreate)
                Console.WriteLine("MySemaphore created in InvokeSempahore1..");

            s1.WaitOne();
            Console.WriteLine("Press any key to release the MySemaphore created in InvokeSempahore1..");
            Console.ReadLine();

            Console.WriteLine("MySemaphore is released in InvokeSempahore1..");
            s1.Release();
            
        }

        private static void InvokeSempahore2()
        {
            bool isSemaphoreCreate;
            Semaphore s1 = new Semaphore(1, 1, "MySemaphore", out isSemaphoreCreate);

            if (isSemaphoreCreate)
                Console.WriteLine("MySemaphore created in InvokeSempahore2..");

            s1.WaitOne();
            Console.WriteLine("MySemaphore accessed in InvokeSempahore2..");

            Console.WriteLine("Press any key to release the MySemaphore created in InvokeSempahore2..");
            Console.ReadLine();

            Console.WriteLine("MySemaphore is released in InvokeSempahore2..");
            s1.Release();
            

        } 
        #endregion
    }
}
