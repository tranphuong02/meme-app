using System;

namespace Core.DI
{
    public interface IEngine 
    {
        ContainerManager ContainerManager { get; set; }

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>(string key = "");

        bool TryResolve(Type serviceType, out object instance);

        bool IsRegistered(Type serviceType);
    }

}
