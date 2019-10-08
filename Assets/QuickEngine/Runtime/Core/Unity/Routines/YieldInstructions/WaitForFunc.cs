namespace QuickEngine.Unity
{
    using System;
    using UnityEngine;

    public class WaitForFunc : CustomYieldInstruction
    {
        private Func<bool> mKeepWaitingFunc;

        public WaitForFunc(Func<bool> keepWaitingFunc)
        {
            mKeepWaitingFunc = keepWaitingFunc;
        }

        public override bool keepWaiting
        {
            get
            {
                return mKeepWaitingFunc();
            }
        }
    }

    public class WaitForFunc<T> : CustomYieldInstruction
    {
        private Func<T, bool> mKeepWaitingFunc;
        private Func<T> mFunc;

        public WaitForFunc(Func<T, bool> keepWaitingFunc, Func<T> func)
        {
            mKeepWaitingFunc = keepWaitingFunc;
            mFunc = func;
        }

        public override bool keepWaiting
        {
            get
            {
                return mKeepWaitingFunc(mFunc());
            }
        }
    }

    public class WaitForFunc<T, U> : CustomYieldInstruction
    {
        private Func<T, U, bool> mKeepWaitingFunc;
        private Func<T> mTFunc;
        private Func<U> mUFunc;

        public WaitForFunc(Func<T, U, bool> keepWaitingFunc, Func<T> tFunc, Func<U> uFunc)
        {
            mKeepWaitingFunc = keepWaitingFunc;
            mTFunc = tFunc;
            mUFunc = uFunc;
        }

        public override bool keepWaiting
        {
            get
            {
                return mKeepWaitingFunc(mTFunc(), mUFunc());
            }
        }
    }

    public class WaitForFunc<T, U, V> : CustomYieldInstruction
    {
        private Func<T, U, V, bool> mKeepWaitingFunc;
        private Func<T> mTFunc;
        private Func<U> mUFunc;
        private Func<V> mVFunc;

        public WaitForFunc(Func<T, U, V, bool> keepWaitingFunc, Func<T> tFunc, Func<U> uFunc, Func<V> vFunc)
        {
            mKeepWaitingFunc = keepWaitingFunc;
            mTFunc = tFunc;
            mUFunc = uFunc;
            mVFunc = vFunc;
        }

        public override bool keepWaiting
        {
            get
            {
                return mKeepWaitingFunc(mTFunc(), mUFunc(), mVFunc());
            }
        }
    }
}
