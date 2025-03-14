using System.Collections.Generic;

namespace Root.Core.Factories.Tools
{
    public class Pool
    {
        public bool CanUseCreate { get; private set; }

        private readonly List<IObjectPool> _allObjects;

        private readonly Stack<IObjectPool> _availableObjects;

        public Pool(bool canUseCreate= false)
        {
            CanUseCreate = canUseCreate;

            _availableObjects = new Stack<IObjectPool>();

            _allObjects = new List<IObjectPool>();
        }

        public IObjectPool Request()
        {
            if (_availableObjects.Count == 0) return null;

            return _availableObjects.Pop();
        }

        public void Return(IObjectPool obj) 
            => _availableObjects.Push(obj);


        public void RegisterObject(IObjectPool ball) 
            => _allObjects.Add(ball);

        public void Clear()
        {
            _availableObjects.Clear();

            _allObjects.Clear();    
        }
    }
}
