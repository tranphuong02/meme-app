using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace Core.DI
{
   public class ContainerManager
    {
        public IContainer Container { get; set; }

        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return Container.Resolve<IEnumerable<T>>().ToArray();
            }
            return Container.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.TryResolve(serviceType, out instance);
        }

        public bool IsRegistered(Type serviceType)
        {
            return Container.IsRegistered(serviceType);
        }
    }
}
