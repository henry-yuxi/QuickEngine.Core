namespace QuickEngine.Libraries
{
    using System;
    using System.Reflection;
    using UnityEngine;

    public class QSingletonCreator
    {
        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            T retInstance = default(T);

            ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-public ctor() not found! in " + typeof(T));
            }

            retInstance = ctor.Invoke(null) as T;
            retInstance.Initialize();

            return retInstance;
        }

        public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton
        {
            T instance = null;

            if (instance == null && Application.isPlaying)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    MemberInfo info = typeof(T);
                    object[] attributes = info.GetCustomAttributes(true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        GMonoSingletonPath defineAttri = attributes[i] as GMonoSingletonPath;
                        if (defineAttri == null)
                        {
                            continue;
                        }
                        instance = CreateComponentOnGameObject<T>(defineAttri.PathInHierarchy, true);
                        break;
                    }

                    if (instance == null)
                    {
                        GameObject obj = new GameObject("Singleton of " + typeof(T).Name);
                        UnityEngine.Object.DontDestroyOnLoad(obj);
                        instance = obj.AddComponent<T>();
                    }

                    //instance.OnInitialize();
                }
            }

            return instance;
        }

        protected static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : MonoBehaviour
        {
            GameObject obj = FindGameObject(null, path, true, dontDestroy);
            if (obj == null)
            {
                obj = new GameObject("Singleton of " + typeof(T).Name);
                if (dontDestroy)
                {
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                }
            }

            return obj.AddComponent<T>();
        }

        private static GameObject FindGameObject(GameObject root, string path, bool build, bool dontDestroy)
        {
            if (path == null || path.Length == 0)
            {
                return null;
            }

            string[] subPath = path.Split('/');
            if (subPath == null || subPath.Length == 0)
            {
                return null;
            }

            return FindGameObject(null, subPath, 0, build, dontDestroy);
        }

        private static GameObject FindGameObject(GameObject root, string[] subPath, int index, bool build, bool dontDestroy)
        {
            GameObject client = null;

            if (root == null)
            {
                client = GameObject.Find(subPath[index]);
            }
            else
            {
                var child = root.transform.Find(subPath[index]);
                if (child != null)
                {
                    client = child.gameObject;
                }
            }

            if (client == null)
            {
                if (build)
                {
                    client = new GameObject(subPath[index]);
                    if (root != null)
                    {
                        client.transform.SetParent(root.transform);
                    }
                    if (dontDestroy && index == 0)
                    {
                        GameObject.DontDestroyOnLoad(client);
                    }
                }
            }

            if (client == null)
            {
                return null;
            }

            if (++index == subPath.Length)
            {
                return client;
            }

            return FindGameObject(client, subPath, index, build, dontDestroy);
        }
    }
}