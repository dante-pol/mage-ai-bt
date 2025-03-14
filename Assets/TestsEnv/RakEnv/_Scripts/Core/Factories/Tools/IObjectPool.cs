using System;

namespace Root.Core.Factories.Tools
{
    public interface IObjectPool
    {
        event Action<IObjectPool> ReturnToPoolEvent;

        void ResetForPool();
    }
}
