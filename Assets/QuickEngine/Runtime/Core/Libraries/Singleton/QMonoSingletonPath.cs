namespace QuickEngine.Libraries
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class GMonoSingletonPath : Attribute
    {
        private string mPathInHierarchy;

        public GMonoSingletonPath(string pathInHierarchy)
        {
            mPathInHierarchy = pathInHierarchy;
        }

        public string PathInHierarchy
        {
            get { return mPathInHierarchy; }
        }
    }
}