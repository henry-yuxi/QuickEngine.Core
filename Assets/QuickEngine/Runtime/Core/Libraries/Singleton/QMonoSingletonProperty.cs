namespace QuickEngine.Libraries
{
    using UnityEngine;

    public abstract class QMonoSingletonProperty<T> where T : MonoBehaviour, ISingleton
    {
        protected static T mInstance = null;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = QSingletonCreator.CreateMonoSingleton<T>();
                }

                return mInstance;
            }
        }

        public static void Dispose()
        {
            GameObject.Destroy(mInstance.gameObject);
            mInstance = null;
        }
    }
}