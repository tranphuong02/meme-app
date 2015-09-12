using System;
using STS.Framework.DI;

namespace Core.DI
{
    public class Engine : IEngine
    {
        public Engine()
        {
            ContainerManager = new ContainerManager();
        }

        public ContainerManager ContainerManager { get; set; }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>(string key = "")
        {
            return ContainerManager.ResolveAll<T>(key);
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return ContainerManager.TryResolve(serviceType, out instance);
        }

        public bool IsRegistered(Type serviceType)
        {
            return ContainerManager.IsRegistered(serviceType);
        }
    }

}
