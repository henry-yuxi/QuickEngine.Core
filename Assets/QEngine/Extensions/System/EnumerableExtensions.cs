namespace QEngine.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static bool AllEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (!predicate(iter.Current))
                        return false;
                }
            }
            finally
            {
                iter.Dispose();
            }
            return true;
        }

        public static bool AnyEx<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var iter = source.GetEnumerator();
            try
            {
                if (iter.MoveNext())
                    return true;
            }
            finally
            {
                iter.Dispose();
            }
            return false;
        }

        public static bool AnyEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (predicate(iter.Current))
                        return true;
                }
            }
            finally
            {
                iter.Dispose();
            }
            return false;
        }

        public static bool ContainsEx<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
                comparer = EqualityComparer<TSource>.Default;
            if (source == null)
                throw new ArgumentNullException("source");

            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (comparer.Equals(iter.Current, value))
                        return true;
                }
            }
            finally
            {
                iter.Dispose();
            }
            return false;
        }

        public static int CountEx<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            ICollection<TSource> is2 = source as ICollection<TSource>;
            if (is2 != null)
                return is2.Count;

            int num = 0;
            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    ++num;
                }
            }
            finally
            {
                iter.Dispose();
            }
            return num;
        }

        public static int CountEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            int num = 0;
            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (predicate(iter.Current))
                        ++num;
                }
            }
            finally
            {
                iter.Dispose();
            }
            return num;
        }

        public static TSource FirstOrDefaultEx<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                    return list[0];
            }
            else
            {
                var iter = source.GetEnumerator();
                try
                {
                    if (iter.MoveNext())
                        return iter.Current;
                }
                finally
                {
                    iter.Dispose();
                }
            }
            return default(TSource);
        }

        public static TSource FirstOrDefaultEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (predicate(iter.Current))
                        return iter.Current;
                }
            }
            finally
            {
                iter.Dispose();
            }
            throw new InvalidOperationException("NoMatch");
        }

        public static TSource LastEx<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                int count = list.Count;
                if (count > 0)
                    return list[count - 1];
            }
            else
            {
                var iter = source.GetEnumerator();
                try
                {
                    if (iter.MoveNext())
                    {
                        TSource current;
                        do
                        {
                            current = iter.Current;
                        }
                        while (iter.MoveNext());
                        return current;
                    }
                }
                finally
                {
                    iter.Dispose();
                }
            }
            throw new InvalidOperationException("NoElements");
        }

        public static TSource LastEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            TSource local = default(TSource);
            bool flag = false;
            var iter = source.GetEnumerator();
            try
            {
                while (iter.MoveNext())
                {
                    if (predicate(iter.Current))
                    {
                        local = iter.Current;
                        flag = true;
                    }
                }
            }
            finally
            {
                iter.Dispose();
            }
            if (!flag)
            {
                throw new InvalidOperationException("NoMatch");
            }
            return local;
        }

        public static IEnumerable<TResult> SelectEx<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            return SelectIterator<TSource, TResult>(source, selector);
        }

        private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            int iteratorVariable0 = -1;
            var iter = source.GetEnumerator();
            while (iter.MoveNext())
            {
                iteratorVariable0++;
                yield return selector(iter.Current, iteratorVariable0);
            }
        }

        public static IEnumerable<TSource> WhereEx<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("source");
            return WhereIterator<TSource>(source, predicate);
        }

        private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int iteratorVariable0 = -1;
            var iter = source.GetEnumerator();
            while (iter.MoveNext())
            {
                iteratorVariable0++;
                if (predicate(iter.Current, iteratorVariable0))
                {
                    yield return iter.Current;
                }
            }
            iter.Dispose();
        }
    }
}
