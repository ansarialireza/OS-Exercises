using System;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex();
    static object monitorLock = new object();
    static object lockObject = new object();
    static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Mutex Example");
            Console.WriteLine("2. Monitor Example");
            Console.WriteLine("3. Lock Example");
            Console.WriteLine("4. AutoResetEvent Example");
            Console.WriteLine("5. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    MutexExample();
                    break;
                case "2":
                    MonitorExample();
                    break;
                case "3":
                    LockExample();
                    break;
                case "4":
                    AutoResetEventExample();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid selection, please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void MutexExample()
    {
        Thread t1 = new Thread(MutexWorker);
        Thread t2 = new Thread(MutexWorker);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

    static void MutexWorker()
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting for the mutex");
        mutex.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} has entered the critical section");
        Thread.Sleep(1000);
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is leaving the critical section");
        mutex.ReleaseMutex();
    }

    static void MonitorExample()
    {
        Thread t1 = new Thread(MonitorWorker);
        Thread t2 = new Thread(MonitorWorker);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

    static void MonitorWorker()
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting for the monitor");
        Monitor.Enter(monitorLock);
        try
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} has entered the critical section");
            Thread.Sleep(1000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is leaving the critical section");
        }
        finally
        {
            Monitor.Exit(monitorLock);
        }
    }

    static void LockExample()
    {
        Thread t1 = new Thread(LockWorker);
        Thread t2 = new Thread(LockWorker);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

    static void LockWorker()
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting for the lock");
        lock (lockObject)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} has entered the critical section");
            Thread.Sleep(1000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is leaving the critical section");
        }
    }

    static void AutoResetEventExample()
    {
        Thread t1 = new Thread(AutoResetEventWorker);
        Thread t2 = new Thread(AutoResetEventWorker);
        t1.Start();
        t2.Start();
        Thread.Sleep(500);
        Console.WriteLine("Signaling the AutoResetEvent");
        autoResetEvent.Set();
        Thread.Sleep(500);
        autoResetEvent.Set();
        t1.Join();
        t2.Join();
    }

    static void AutoResetEventWorker()
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is waiting for the AutoResetEvent");
        autoResetEvent.WaitOne();
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} has entered the critical section");
        Thread.Sleep(1000);
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is leaving the critical section");
    }
}
