using System;
using System.Threading;

namespace MemoryOptimizer
{
    internal class MOptimize
    {
        const int PERIOD_IN_MS = 100;
        static int counter;

        readonly AutoResetEvent event_ = new AutoResetEvent(false);
        Thread thread;

        /// <summary>
        /// Starts memory optimizing
        /// </summary>
        public void Start()
        {
            Stop();
            thread = new Thread(Run) { Name = string.Format("MemoryCleaner#{0}", Interlocked.Increment(ref counter)), IsBackground = true };
            // this makes thread to be stopped when Main thread is over
            event_.Reset();
            thread.Start();
        }

        /// <summary>
        /// Stops memory optimizing
        /// </summary>
        public void Stop()
        {
            if (thread != null)
            {
                event_.Set();
                thread.Join();
                thread = null;
            }
        }

        void Run()
        {
            while (!event_.WaitOne(PERIOD_IN_MS, false))
            {
                try
                {
                    long mem = (FormMain.TrackValue * (long)Math.Pow(1024, 2));
                    if (mem > int.MaxValue) mem = int.MaxValue;
                    GC.AddMemoryPressure(mem);
                }
                catch
                {
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}