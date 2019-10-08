namespace QuickEngine.Libraries
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class QAndroidHelper
    {
#if UNITY_ANDROID
        private static Dictionary<string, AndroidJavaClass> mAndroidJavaClassDict = new Dictionary<string, AndroidJavaClass>();

        public static AndroidJavaObject CurrentActivity()
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                return jc.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public static AndroidJavaObject GetInstance(string className, string methodName)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass(className))
            {
                return jc.CallStatic<AndroidJavaObject>(methodName);
            }
        }

        public static void CallMethod(this AndroidJavaObject androidJavaObject, string methodName, params object[] args)
        {
            androidJavaObject.Call(methodName, args);
        }

        public static T CallMethod<T>(this AndroidJavaObject androidJavaObject, string methodName, params object[] args)
        {
            return androidJavaObject.Call<T>(methodName, args);
        }

#endif

        public static void CallMethod(string className, string methodName, params object[] args)
        {
#if UNITY_ANDROID
            getAndroidJavaClass(className).Call(methodName, args);
#endif
        }

        public static T CallMethod<T>(string className, string methodName, params object[] args)
        {
#if UNITY_ANDROID
            return getAndroidJavaClass(className).Call<T>(methodName, args);
#else
            return default(T);
#endif
        }

        public static void CallStaticMethod(string className, string methodName, params object[] args)
        {
#if UNITY_ANDROID
            getAndroidJavaClass(className).CallStatic(methodName, args);
#endif
        }

        public static T CallStaticMethod<T>(string className, string methodName, params object[] args)
        {
#if UNITY_ANDROID
            return getAndroidJavaClass(className).CallStatic<T>(methodName, args);
#else
            return default(T);
#endif
        }

        /// <summary>
        /// 访问当前应用的方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        public static void CallAndroidMethod(string methodName, params object[] args)
        {
#if UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call(methodName, args);
                }
            }
#endif
        }

        /// <summary>
        /// 访问当前应用的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CallAndroidMethod<T>(string methodName, params object[] args)
        {
#if UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    return jo.Call<T>(methodName, args);
                }
            }
#else
            return default(T);
#endif
        }

        /// <summary>
        /// 访问当前应用的静态方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        public static void CallStaticAndroidMethod(string methodName, params object[] args)
        {
#if UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.CallStatic(methodName, args);
                }
            }
#endif
        }

        /// <summary>
        /// 访问当前应用的静态方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CallStaticAndroidMethod<T>(string methodName, params object[] args)
        {
#if UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    return jo.CallStatic<T>(methodName, args);
                }
            }
#else
            return default(T);
#endif
        }

#if UNITY_ANDROID

        private static AndroidJavaClass getAndroidJavaClass(string className)
        {
            AndroidJavaClass jc = null;

            if (mAndroidJavaClassDict.ContainsKey(className))
            {
                jc = mAndroidJavaClassDict[className];
            }
            else
            {
                jc = new AndroidJavaClass(className);
                mAndroidJavaClassDict.Add(className, jc);
            }
            return jc;
        }

#endif
    }
}