using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core.Lifetime;

namespace Core.DI
{
    public class ContainerManager
    {
        public IContainer Container { get; set; }

        public T Resolve<T>() where T : class
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).Resolve(type);
        }

        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).Resolve<IEnumerable<T>>().ToArray();
            }
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public bool TryResolve(Type serviceType, out object instance)
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).TryResolve(serviceType, out instance);
        }

        public bool IsRegistered(Type serviceType)
        {
            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag).IsRegistered(serviceType);
        }
    }
}