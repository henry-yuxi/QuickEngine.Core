namespace QuickEngine.Libraries
{
    using UnityEngine;

    public abstract class QMonoSingleton<T> : MonoBehaviour, ISingleton where T : QMonoSingleton<T>
    {
        protected static T mInstance = null;
        private static readonly object mThreadLock = new object();
        private static bool mIsQuitApplication = false;

        public static T Instance
        {
            get
            {
                if (mIsQuitApplication)
                {
                    Debug.LogError(string.Format("Try To Call [QMonoSingleton] Instance {0} When The Application Already Quit, return null inside", typeof(T)));
                    return null;
                }
                lock (mThreadLock)
                {
                    if (null == mInstance)
                    {
                        mInstance = QSingletonCreator.CreateMonoSingleton<T>();
                    }
                }
                return QMonoSingleton<T>.mInstance;
            }
        }

        private void Awake()
        {
            mIsQuitApplication = false;
            this.Initialize();
        }

        public virtual void Initialize()
        {
        }

        public virtual void UnInitialize()
        {
        }

        public static bool HasInstance()
        {
            return QMonoSingleton<T>.mInstance != null;
        }

        public virtual void Dispose()
        {
            UnInitialize();
            mIsQuitApplication = true;
            Debug.Log("[GEMonoSingleton] OnDestroy '" + typeof(T).FullName + "'");
            QMonoSingleton<T>.mInstance = null;
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (QMonoSingleton<T>.mInstance != null && QMonoSingleton<T>.mInstance.gameObject == base.gameObject)
            {
                QMonoSingleton<T>.mInstance = (T)((object)null);
            }
        }
    }
}