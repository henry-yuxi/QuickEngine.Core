using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
    public struct GCFreeEnumerator<K, V>
    {
        private Dictionary<K, V>.Enumerator enumerator;

        public KeyValuePair<K, V> Current
        {
            get { return enumerator.Current; }
        }

        public GCFreeEnumerator(Dictionary<K, V> collection)
        {
            enumerator = collection.GetEnumerator();
        }

        public GCFreeEnumerator<K, V> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }
    }

    public static GCFreeEnumerator<K, V> ForEachEx<K, V>(this Dictionary<K, V> collection)
    {
        return new GCFreeEnumerator<K, V>(collection);
    }

    public static int Count<K, V>(this Dictionary<K, V> collection, System.Predicate<KeyValuePair<K, V>> predicate)
    {
        if (predicate == null)
            return 0;

        int count = 0;
        for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
        {
            if (predicate(enumerator.Current))
                count++;
        }

        return count;
    }

    public static bool All<K, V>(this Dictionary<K, V> collection, System.Predicate<KeyValuePair<K, V>> predicate)
    {
        if (predicate == null)
            return false;

        for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
        {
            if (!predicate(enumerator.Current))
                return false;
        }

        return true;
    }

    public static bool Any<K, V>(this Dictionary<K, V> collection, System.Predicate<KeyValuePair<K, V>> predicate)
    {
        if (predicate == null)
            return false;

        for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
        {
            if (predicate(enumerator.Current))
                return true;
        }

        return false;
    }

    public static KeyValuePair<K, V> FirstOrDefault<K, V>(this Dictionary<K, V> collection)
    {
        var enumerator = collection.GetEnumerator();
        if (enumerator.MoveNext())
            return enumerator.Current;

        return default(KeyValuePair<K, V>);
    }

    public static KeyValuePair<K, V> FirstOrDefault<K, V>(this Dictionary<K, V> collection, System.Predicate<KeyValuePair<K, V>> predicate)
    {
        for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
        {
            if (predicate(enumerator.Current))
                return enumerator.Current;
        }

        return default(KeyValuePair<K, V>);
    }

    public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
            return false;

        dict.Add(key, value);
        return true;
    }

    public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
            dict[key] = value;
        else
            dict.Add(key, value);
    }

    public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<KeyValuePair<TKey, TValue>> kvpList)
    {
        foreach (var kvp in kvpList)
        {
            dict.Add(kvp.Key, kvp.Value);
        }
    }

    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
    {
        return groupings.ToDictionary(group => group.Key, group => group.ToList());
    }
}
