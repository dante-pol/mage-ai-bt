using UnityEngine;

namespace Root.Core.Tools
{
    public static class AssetsProvider
    {
        public static T Load<T>(string path) where T : Object 
            => Resources.Load<T>(path);

        public static T[] LoadAll<T>(string path) where T : Object
            => Resources.LoadAll<T>(path);  
    }
}
