namespace QuickEngine.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public static partial class CSharpExtensions
    {
        /// <summary>
        /// Enum没有实现IEquatable接口。因此，当我们使用Enum类型作为key值时，Dictionary的内部操作就需要将Enum类型转换为System.Object，这就导致了Boxing的产生。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public struct KeyEnumComparer<T> : IEqualityComparer<T> where T : struct
        {
            public bool Equals(T first, T second)
            {
                var firstParam = Expression.Parameter(typeof(T), "first");
                var secondParam = Expression.Parameter(typeof(T), "second");
                var equalExpression = Expression.Equal(firstParam, secondParam);
                return Expression.Lambda<Func<T, T, bool>>(equalExpression, new[] { firstParam, secondParam }).Compile().Invoke(first, second);
            }

            public int GetHashCode(T instance)
            {
                var parameter = Expression.Parameter(typeof(T), "instance");
                var convertExpression = Expression.Convert(parameter, typeof(int));
                return Expression.Lambda<Func<T, int>>(convertExpression, new[] { parameter }).Compile().Invoke(instance);
            }
        }

        #region T Extensions

        public static bool IsNull<T>(this T t)
        {
            return (EqualityComparer<T>.Default.Equals(t, default(T)));
        }

        public static List<T> AsList<T>(this T t)
        {
            return new List<T> { t };
        }

        public static bool IsIn<T>(this T source, params T[] list) where T : class
        {
            return (source != null) && (!list.IsNullOrEmpty()) && (list.Contains(source));
        }

        public static bool IsIn<T>(this T source, params T?[] list) where T : struct
        {
            return (!list.IsNullOrEmpty()) && (list.Contains(source));
        }

        public static bool IsBetweenInclusive<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        }

        public static bool IsBetweenExclusive<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }

        /// <summary>
        /// 对象拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T item)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable. ", item.ToString());
            }
            if (Object.ReferenceEquals(item, null))
            {
                return default(T);
            }
            using (Stream stream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion T Extensions

        #region ICollection IEnumerable Extensions

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return (collection == null || !collection.Any());
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable == null || !enumerable.Any());
        }

        public static bool HasValue<T>(this ICollection<T> collection)
        {
            return !IsNullOrEmpty(collection);
        }

        public static T Result<T>(this IEnumerator target) where T : class
        {
            return target.Current as T;
        }

        public static T ResultValueType<T>(this IEnumerator target) where T : struct
        {
            return (T)target.Current;
        }

        public static bool IsCollectionsEqual<T>(this ICollection<T> collectionA, ICollection<T> collectionB)
        {
            return (!collectionA.Except(collectionB).Any() && !collectionB.Except(collectionA).Any());
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

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
            throw new InvalidOperationException("NoElements");
        }

        public static TSource FirstOrDefaultEx<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

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

        #endregion ICollection IEnumerable Extensions

        #region Array Extensions

        public static string ToBase64(this byte[] bytes)
        {
            if (bytes.IsNullOrEmpty()) { return null; } // TODO: raise exception or log error
            return Convert.ToBase64String(bytes);
        }

        public static void ForEachEx<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i]);
            }
        }

        public static T TryGetRandom<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
                return default(T);

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T[] SubArray<T>(this T[] array, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        #endregion Array Extensions

        #region Dictionary Extensions

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

        public static List<TKey> ToKeysList<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return Enumerable.ToList(dict.Keys);
        }

        public static List<TValue> ToValuesList<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return Enumerable.ToList(dict.Values);
        }

        public static List<KeyValuePair<TKey, TValue>> ToKeyValueList<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return Enumerable.ToList(dict);
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

        public static Tvalue TryGetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key, Tvalue defaultValue = default(Tvalue))
        {
            if (!typeof(Tkey).IsValueType && Equals(key, default(Tkey)))
            {
                return defaultValue;
            }
            if (dict.IsNullOrEmpty())
            {
                return defaultValue;
            }
            Tvalue value;
            if (dict.TryGetValue(key, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key)) dict.Add(key, value);
            return dict;
        }

        public static bool TryRemove<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
                return true;
            }
            return false;
        }

        public static IDictionary<TKey, TValue> AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("Dictionary Is Null");
            }
            if (!dict.ContainsKey(key)) { dict.Add(new KeyValuePair<TKey, TValue>(key, value)); }
            else { dict[key] = value; }
            return dict;
        }

        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }

        public static void Foreach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<KeyValuePair<TKey, TValue>> action, int maxCount = 1000)
        {
            if (action == null) return;
            var enumerator = dict.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current);
            }
        }

        public static void Foreach<TKey, TValue>(this IDictionary<TKey, TValue> dict, Action<TKey, TValue> action, int maxCount = 1000)
        {
            if (action == null) return;
            var enumerator = dict.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Key, enumerator.Current.Value);
            }
        }

        public static void ForeachKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, Action<TKey> action, int maxCount = 1000)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Key);
            }
        }

        public static void ForeachValue<TKey, TValue>(this IDictionary<TKey, TValue> dic, Action<TValue> action, int maxCount = 1000)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < maxCount)
            {
                action(enumerator.Current.Value);
            }
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("Dictionary Is Null");
            }
            return new SortedDictionary<TKey, TValue>(dict);
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dict, IComparer<TKey> comparer)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("Dictionary Is Null");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("IComparer  Is Null");
            }
            return new SortedDictionary<TKey, TValue>(dict, comparer);
        }

        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("Dictionary Is Null");
            }
            return dict.OrderBy(v => v.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, IComparer<TValue> comparer)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("Dictionary Is Null");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("IComparer  Is Null");
            }
            return dict.OrderBy(v => v.Value, comparer).ToDictionary(item => item.Key, item => item.Value);
        }

        #endregion Dictionary Extensions

        #region List Extensions

        public struct GCFreeEnumerator<TKey>
        {
            private List<TKey>.Enumerator enumerator;

            public TKey Current
            {
                get { return enumerator.Current; }
            }

            public GCFreeEnumerator(List<TKey> collection)
            {
                enumerator = collection.GetEnumerator();
            }

            public GCFreeEnumerator<TKey> GetEnumerator()
            {
                return this;
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }
        }

        public static bool AllEx<T>(this List<T> collection, System.Predicate<T> predicate)
        {
            if (predicate == null)
                return false;

            for (int i = 0, length = collection.Count; i < length; i++)
            {
                if (!predicate(collection[i]))
                    return false;
            }

            return true;
        }

        public static bool AnyEx<T>(this List<T> collection, System.Predicate<T> predicate)
        {
            if (predicate == null)
                return false;

            for (int i = 0, length = collection.Count; i < length; i++)
            {
                if (predicate(collection[i]))
                    return true;
            }

            return false;
        }

        public static bool IsNullOrEmpty<TKey>(this List<TKey> list)
        {
            return list == null || !list.Any();
        }

        public static void ForeachEx<TKey>(this IList<TKey> list, Action<TKey> action)
        {
            if (list.IsNullOrEmpty()) { return; }
            for (int i = 0, count = list.Count; i < count; i++)
            {
                action(list[i]);
            }
        }

        public static TKey FirstOrDefaultEx<TKey>(this List<TKey> collection)
        {
            return collection.Count > 0 ? collection[0] : default(TKey);
        }

        public static TKey FirstOrDefaultEx<TKey>(this List<TKey> collection, System.Predicate<TKey> predicate)
        {
            for (var enumerator = collection.GetEnumerator(); enumerator.MoveNext();)
            {
                if (predicate(enumerator.Current))
                    return enumerator.Current;
            }
            return default(TKey);
        }

        public static int CountEx<TKey>(this List<TKey> list)
        {
            return list == null ? 0 : list.Count;
        }

        public static int CountEx<TKey>(this List<TKey> list, Predicate<TKey> predicate)
        {
            if (predicate == null) { return 0; }
            if (list.IsNullOrEmpty()) { return 0; }
            int count = 0;
            for (int i = 0, length = list.Count; i < length; i++)
            {
                if (predicate(list[i]))
                    count++;
            }
            return count;
            //return list.FindAll(predicate).CountEx();
        }

        public static void TryRemove<TKey>(this IList<TKey> list, TKey item)
        {
            if (list == null) { return; }
            TKey temp = list.FirstOrDefaultEx(p => p.GetHashCode() == item.GetHashCode());
            if (temp != null)
            {
                list.Remove(temp);
            }
        }

        public static bool TryInsert<TKey>(this IList<TKey> list, int index, TKey insertItem)
        {
            if ((!typeof(TKey).IsValueType && Equals(insertItem, default(TKey)))) { return false; }
            if (list == null) { return false; }

            list.Insert(index, insertItem);
            return true;
        }

        public static bool TryReplace<TKey>(this IList<TKey> list, int position, TKey item)
        {
            if ((!typeof(TKey).IsValueType && Equals(item, default(TKey)))) { return false; }
            if (list == null) { return false; }
            if (position > list.Count - 1) { return false; }

            list.RemoveAt(position);
            list.Insert(position, item);
            return true;
        }

        public static TKey Front<TKey>(this List<TKey> list)
        {
            if (list == null || list.Count == 0)
                return default(TKey);
            return list[0];
        }

        public static TKey Back<TKey>(this List<TKey> list)
        {
            if (list == null || list.Count == 0)
                return default(TKey);

            return list[list.Count - 1];
        }

        public static TKey TryGetValue<TKey>(this IList<TKey> list, int index, TKey defaultValue) where TKey : struct
        {
            if (list.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return (index >= list.Count || index < 0) ? defaultValue : list[index];
        }

        public static TKey TryGetRandom<TKey>(this List<TKey> list)
        {
            if (list == null || list.Count == 0)
                return default(TKey);

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        #endregion List Extensions

        #region Queue Extensions

        public static T DequeueOrNull<T>(this Queue<T> queue)
        {
            try
            {
                return (queue.Count > 0) ? queue.Dequeue() : default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        #endregion Queue Extensions

        #region Stack Extensions

        public static bool Remove<T>(this Stack<T> stack, T item)
        {
            if (!stack.Contains(item))
                return false;

            Stack<T> popStack = new Stack<T>();
            T popItem = default(T);
            do
            {
                popItem = stack.Pop();
                popStack.Push(popItem);
            } while (!popItem.Equals(item));

            popStack.Pop();

            for (int i = 0; i < popStack.Count; i++)
            {
                stack.Push(popStack.Pop());
            }
            return true;
        }

        #endregion Stack Extensions
    }
}
