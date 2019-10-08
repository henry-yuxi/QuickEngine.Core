namespace QuickEngine.Libraries
{
    public abstract class QSingleton<T> : ISingleton where T : class, new()
    {
        protected static T mInstance = null;
        protected static readonly object mLock = new object();

        protected QSingleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (null == QSingleton<T>.mInstance)
                {
                    lock (mLock)
                    {
                        if (null == QSingleton<T>.mInstance)
                        {
                            QSingleton<T>.mInstance = System.Activator.CreateInstance<T>();
                            (QSingleton<T>.mInstance as QSingleton<T>).Initialize();
                        }
                    }
                }
                return mInstance;
            }
        }

        public static bool HasInstance()
        {
            return QSingleton<T>.mInstance != null;
        }

        public virtual void Dispose()
        {
            if (QSingleton<T>.mInstance != null)
            {
                (QSingleton<T>.mInstance as QSingleton<T>).UnInitialize();
                QSingleton<T>.mInstance = (T)((object)null);
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual void UnInitialize()
        {
        }
    }
}