namespace QuickEngine.Unity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public static class Yielders
    {
        public static bool Enabled = true;

        public static int mInternalCounter = 0; // counts how many times the app yields

        private static WaitForEndOfFrame mWaitForEndOfFrame = new WaitForEndOfFrame();

        public static WaitForEndOfFrame EndOfFrame
        {
            get { mInternalCounter++; return Enabled ? mWaitForEndOfFrame : new WaitForEndOfFrame(); }
        }

        private static WaitForFixedUpdate mWaitForFixedUpdate = new WaitForFixedUpdate();

        public static WaitForFixedUpdate FixedUpdate
        {
            get { mInternalCounter++; return Enabled ? mWaitForFixedUpdate : new WaitForFixedUpdate(); }
        }

        public static WaitForSeconds Seconds(float seconds)
        {
            mInternalCounter++;

            if (!Enabled)
                return new WaitForSeconds(seconds);

            WaitForSeconds wfs;
            if (!mWaitForSecondsYielders.TryGetValue(seconds, out wfs))
                mWaitForSecondsYielders.Add(seconds, wfs = new WaitForSeconds(seconds));
            return wfs;
        }

        public static IEnumerator Seconds(float seconds, UnityAction action)
        {
            yield return Seconds(seconds);
            if (action != null)
            {
                action();
            }
        }

        public static void CleanUpSecondsCache()
        {
            mWaitForSecondsYielders.Clear();
        }

        private static Dictionary<float, WaitForSeconds> mWaitForSecondsYielders = new Dictionary<float, WaitForSeconds>(100, new FloatComparer());

        private class FloatComparer : IEqualityComparer<float>
        {
            public bool Equals(float x, float y)
            {
                return x == y;
            }

            public int GetHashCode(float obj)
            {
                return (int)obj;
            }
        }
    }
}
