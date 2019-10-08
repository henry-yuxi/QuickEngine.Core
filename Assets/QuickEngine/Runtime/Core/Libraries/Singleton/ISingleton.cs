namespace QuickEngine.Libraries
{
    using System;

    public interface ISingleton : IDisposable
    {
        void Initialize();

        void UnInitialize();
    }
}