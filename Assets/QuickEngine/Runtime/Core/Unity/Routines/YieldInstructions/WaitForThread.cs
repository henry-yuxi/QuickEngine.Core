namespace QuickEngine.Unity
{
    using System.Threading;
    using UnityEngine;

    public class WaitForThread : CustomYieldInstruction
    {
        private Thread thread;

        public WaitForThread(Thread thread)
        {
            this.thread = thread;
        }

        public override bool keepWaiting
        {
            get
            {
                return thread.IsAlive;
            }
        }
    }
}
