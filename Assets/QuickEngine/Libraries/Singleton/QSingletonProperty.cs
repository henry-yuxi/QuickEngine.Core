namespace QuickEngine.Libraries
{
    public class QSingletonProperty<T> where T : class, ISingleton
    {
        protected static T mInstance = null;
        private static object mLock = new object();

        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = QSingletonCreator.CreateSingleton<T>();
                    }
                }

                return mInstance;
            }
        }

        public static void Dispose()
        {
            mInstance = null;
        }
    }
}