using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public struct GCFreeEnumerator<T>
    {
        private List<T>.Enumerator enumerator;

        public T Current
        {
            get { return enumerator.Current; }
        }

        public GCFreeEnumerator(List<T> collection)
        {
            enumerator = collection.GetEnumerator();
        }

        public GCFreeEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }
    }

    public static GCFreeEnumerator<T> ForEachEx<T>(this List<T> collection)
    {
        return new GCFreeEnumerator<T>(collection);
    }

    public static int Count<T>(this List<T> collection, System.Predicate<T> predicate)
    {
        if (predicate == null)
            return 0;

        int count = 0;
        for (int i = 0; i < collection.Count; i++)
        {
            if (predicate(collection[i]))
                count++;
        }

        return count;
    }

    public static bool All<T>(this List<T> collection, System.Predicate<T> predicate)
    {
        if (predicate == null)
            return false;

        for (int i = 0; i < collection.Count; i++)
        {
            if (!predicate(collection[i]))
                return false;
        }

        return true;
    }

    public static bool Any<T>(this List<T> collection, System.Predicate<T> predicate)
    {
        if (predicate == null)
            return false;

        for (int i = 0; i < collection.Count; i++)
        {
            if (predicate(collection[i]))
                return true;
        }

        return false;
    }

    public static T FirstOrDefault<T>(this List<T> collection)
    {
        return collection.Count > 0 ? collection[0] : default(T);
    }

    public static T FirstOrDefault<T>(this List<T> collection, System.Predicate<T> predicate)
    {
        for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
        {
            if (predicate(enumerator.Current))
                return enumerator.Current;
        }

        return default(T);
    }

    public static bool IsNullOrEmpty<T>(this T[] data)
    {
        return ((data == null) || (data.Length == 0));
    }

    public static bool IsNullOrEmpty<T>(this List<T> data)
    {
        return ((data == null) || (data.Count == 0));
    }

    public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> data)
    {
        return ((data == null) || (data.Count == 0));
    }

    public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
    {
        var dict = new Dictionary<int, T>();

        foreach (var item in list)
        {
            if (!dict.ContainsKey(Predicate(item)))
            {
                dict.Add(Predicate(item), item);
            }
        }

        return dict.Values.AsEnumerable();
    }

    public static T DequeueOrNull<T>(this Queue<T> q)
    {
        try
        {
            return (q.Count > 0) ? q.Dequeue() : default(T);
        }
        catch (Exception)
        {
            return default(T);
        }
    }

}
