using System;

namespace Root.Core.Factories.Tools
{
    public class Pool<T> where T : class, IObjectPool
    {
        public bool CanUseCreate { get; private set; }

        public T Request()
        {
            return default(T);  
        }

        public void Return()
        {

        }

        public void Register(T ball)
        {
            throw new NotImplementedException();
        }
    }
}
