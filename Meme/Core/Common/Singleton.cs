using System;

namespace Core.Common
{

    public class Singleton<T>
    {
        private static T _instance;
        private static readonly object SyncRoot = new Object();

        private Singleton() { }

        public static T Instance
        {
            get
            {
                // If ready have an instance => return
                if (_instance != null) return _instance;

                // Multithread
                lock (SyncRoot)
                {
                    // Create an instance 
                    if (_instance == null)
                    {
                        _instance = CreateInstance();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Create an Instance
        /// </summary>
        /// <returns></returns>
        private static T CreateInstance()
        {
            var instance = default(T);
            var type = typeof(T);

            if (!type.IsInterface)
            {
                instance = (T)Activator.CreateInstance(type, true);
                
            }
            else
            {
                // TODO: singleton Interface
            }

            return instance;
        }
    }
}
