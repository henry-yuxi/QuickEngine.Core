namespace QuickEngine.Extensions
{
    using System;

    public static partial class CSharpExtensions
    {
        #region Action Extensions

        public static void TryInvoke(this Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        public static void TryInvoke<T>(this Action<T> action, T arg)
        {
            if (action != null)
            {
                action(arg);
            }
        }

        public static void TryInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action != null)
            {
                action(arg1, arg2);
            }
        }

        public static void TryInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action != null)
            {
                action(arg1, arg2, arg3);
            }
        }

        public static void TryInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (action != null)
            {
                action(arg1, arg2, arg3, arg4);
            }
        }

        #endregion Action Extensions

        #region Func Extensions

        public static TResult TryInvoke<TResult>(this Func<TResult> func)
        {
            if (func != null)
            {
                return func();
            }

            return default(TResult);
        }

        public static TResult TryInvoke<T1, TResult>(this Func<T1, TResult> func, T1 arg1)
        {
            if (func != null)
            {
                return func(arg1);
            }

            return default(TResult);
        }

        public static TResult TryInvoke<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            if (func != null)
            {
                return func(arg1, arg2);
            }

            return default(TResult);
        }

        public static TResult TryInvoke<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            if (func != null)
            {
                return func(arg1, arg2, arg3);
            }

            return default(TResult);
        }

        public static TResult TryInvoke<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (func != null)
            {
                return func(arg1, arg2, arg3, arg4);
            }

            return default(TResult);
        }

        #endregion Func Extensions
    }
}