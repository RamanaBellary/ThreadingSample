using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingSample
{
    public delegate void PrintSumOfNumbersDelegate(int sum);

    public class Numbers
    {
        int _number;

        PrintSumOfNumbersDelegate _callback;
        public Numbers(int n, PrintSumOfNumbersDelegate callback)
        {
            Thread t = new Thread(Sum);
            this._number = n;
            this._callback = callback;
        }

        public void Sum()
        {
            int sum = 0;
            for (int i = 1; i <= _number; i++)
            {
                sum += i;
                Thread.Sleep(1000);
            }
            _callback?.Invoke(sum);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region AutoResetEvent
            //for (int i = 1; i <= 3; i++)
            //{
            //    Thread t = new Thread(AutoResetEventMethod);
            //    t.Name = "t" + i;
            //    t.Start();
            //}
            //are.Set();
            //are.Set();
            //are.Set();
            #endregion

            #region MaualResetEvent
            //for (int i = 1; i <= 3; i++)
            //{
            //    Thread t = new Thread(ManualResetEventMethod);
            //    t.Name = "t" + i;
            //    t.Start();
            //}
            //mre.Set(); 
            #endregion

            #region Using Callback in a Thread
            //PrintSumOfNumbersDelegate sumDel = PrintSum;
            //Numbers num = new Numbers(Convert.ToInt32( Console.ReadLine()), sumDel);
            //Thread t = new Thread(num.Sum);
            //t.Start(); 
            #endregion

            #region Mutex
            ////Example 1
            //MutexEx.Do();

            ////Example 2
            //using (Mutex m1 = new Mutex(false, "GlobalThreadingSample"))
            //{
            //    if (!m1.WaitOne(TimeSpan.FromSeconds(3), false))
            //    {
            //        Console.WriteLine("Another instance is already running..");
            //        Console.ReadLine();
            //        return;
            //    }

            //    Console.WriteLine("Press on any key to exit.");
            //    Console.ReadLine();
            //}
            #endregion

            #region Semaphore
            //SemaphoreExample.Do();
            #endregion

            #region SpinLock
            //for (int i = 1; i <= 10; i++)
            //{
            //    Thread t = new Thread(SpinLockEx.Do);
            //    t.Name = "t" + i;
            //    t.Start();
            //    Thread.Sleep(1000);
            //}
            #endregion

            #region SpinWait
            //for (int i = 1; i <= 10; i++)
            //{
            //    Thread t = new Thread(SpinWaitEx.Do);
            //    t.Name = "t" + i;
            //    t.Start();
            //}
            #endregion

            //DisplayLongRunningAsyncTaskResult();

            //var t = Run<int>(() =>{ Thread.Sleep(5000);return 100; });
            //Console.WriteLine("afsa");
            //Console.WriteLine(t.Result);

            //Task<int> t = Task.Run(() => { Console.WriteLine("Running task t..."); Thread.Sleep(5000); return 100; });
            //var awaiter = t.GetAwaiter();
            //awaiter.OnCompleted(() => { Console.WriteLine("Result from task t:" + awaiter.GetResult()); });

            //Task<string> t1 = Task<string>.Factory.StartNew(() =>
            //{
            //    string res = GetContent2();
            //    return res;
            //});
            //Task<string> t2 = Task<string>.Factory.StartNew(() =>
            //{
            //    string res = GetContent();
            //    return res;
            //});

            //Task<string[]> getTask = Task.WhenAll(t1, t2);
            //getTask.ContinueWith(t =>
            //    {
            //        if (t.IsFaulted)
            //            Console.WriteLine(t.Exception);
            //        else
            //            Console.WriteLine(t.Result);
            //    });
            //p1
            //Program p = new Program();
            //Timer timer = new Timer(p.TimerMethod, null, 1000, 5000);
            //Console.WriteLine("Press the Enter key to end the program.");
            //Console.ReadLine();
            //p1

            //p2
            //Console.WriteLine("Press Enter to create three threads and start them.\r\n" +
            //              "The threads wait on AutoResetEvent #1, which was created\r\n" +
            //              "in the signaled state, so the first thread is released.\r\n" +
            //              "This puts AutoResetEvent #1 into the unsignaled state.");
            //Console.ReadLine();

            //for (int i = 1; i < 4; i++)
            //{
            //    Thread t = new Thread(ThreadProc);
            //    t.Name = "Thread_" + i;
            //    t.Start();
            //}
            //Thread.Sleep(250);

            //for (int i = 0; i < 2; i++)
            //{
            //    Console.WriteLine("Press Enter to release another thread.");
            //    Console.ReadLine();
            //    are1.Set();
            //    Thread.Sleep(250);
            //}

            //Console.WriteLine("\r\nAll threads are now waiting on AutoResetEvent #2.");
            //for (int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine("Press Enter to release a thread.");
            //    Console.ReadLine();
            //    are2.Set();
            //    Thread.Sleep(250);
            //}

            //Console.ReadLine();
            //p2

            //p3
            //Test(null);//new string[] { "adf", "fsdf" }
            //Program p = new Program();
            //Thread tt = new Thread(() => { p.Go("hello"); p.Go("asf"); });
            //p.Go();
            //tt.Start();
            //p3

            //p4
            //Program p = new Program();
            //new Thread(Go).Start();
            //Go();
            //p4

            Console.WriteLine("Press on any key to exit.");
            Console.ReadLine();
        }

        static AutoResetEvent are1 = new AutoResetEvent(true);
        static AutoResetEvent are2 = new AutoResetEvent(false);

        static Task<TResult> Run<TResult>(Func<TResult> fun)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(fun());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            }).Start();
            return tcs.Task;
        }

        static Task<int> LongRunningAsyncTask()
        {
            return Task.Run(() => { Thread.Sleep(5000); return 100; });
        }

        static async void DisplayLongRunningAsyncTaskResult()
        {
            var result = await LongRunningAsyncTask();
            Console.WriteLine("Result:" + result);
        }

        static void PrintSum(int sum)
        {
            Console.WriteLine("Sum :" + sum);
        }

        private const int REPETITIONS = 5;

        private static void DoWork(int i)
        {
            //int j = (int)i;
            for (int j = 0; j < REPETITIONS; j++)
                Console.Write("*");
        }

        static string GetContent()
        {
            try
            {

                Console.WriteLine("Started GetContent Method.");
                //Thread.Sleep(1000);
                //string s =null;
                //s.Substring(0);
                return "Completed executing the GetContent";
            }
            catch (Exception exe)
            {
                throw new Exception("exception..");
            }
        }

        static string GetContent2()
        {
            try
            {

                Console.WriteLine("Started GetContent Method.");
                return "Completed executing the GetContent2";
            }
            catch (Exception exe)
            {
                throw new Exception("exception..");
            }
        }
        static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("{0} waits on AutoResetEvent #1.", name);
            are1.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent #1.", name);

            Console.WriteLine("{0} waits on AutoResetEvent #2.", name);
            are2.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent #2.", name);

            Console.WriteLine("{0} ends.", name);
        }

        public void TimerMethod(object state)
        {
            Console.Write(".");
        }

        private static void Test(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());
            worker.Name = "ZTestThread";
            if (args != null && args.Length > 0) worker.IsBackground = true;
            worker.Start();
        }

        static bool done;
        static object lockerObj = new object();
        static void Go()
        {
            //lock(lockerObj)
            if (!done) { Console.WriteLine("Done"); done = true; }
        }

        void Go(string s)
        {
            Console.WriteLine(s);
        }

        static ManualResetEvent mre = new ManualResetEvent(false);
        static AutoResetEvent are = new AutoResetEvent(false);
        private static void AutoResetEventMethod()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} waiting for the signal");
            are.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} got the signal");
        }
        private static void ManualResetEventMethod()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} waiting for the signal");
            mre.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.Name} got the signal");
        }
    }
}
