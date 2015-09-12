using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.Properties;
using STS.Framework.DI;

namespace Core.DI
{
    public class DependencyRegister
    {
        /// <summary>
        /// The new type need to register
        /// </summary>
        [UsedImplicitly]
        private static IList<Type> _registerTypes = new List<Type>();

        /// <summary>
        /// Build the container
        /// </summary>
        /// <param name="assemblies">The list of assemblies need to apply DI</param>
        /// <returns></returns>
        public static IContainer BuildContainer(List<string> assemblies)
        {
            var containerBuilder = new ContainerBuilder();

            // Register AutofacWeb
            containerBuilder.RegisterModule(new AutofacWebTypesModule());

            // Register Controller
            containerBuilder.RegisterControllers(AppDomain.CurrentDomain.GetAssemblies());

            // Register API Controller
            containerBuilder.RegisterApiControllers(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var assembly in assemblies)
            {
                AddAssembly(assembly, containerBuilder);
            }

            EngineContext.Current.ContainerManager.Container = containerBuilder.Build();
            return EngineContext.Current.ContainerManager.Container;
        }

        /// <summary>
        /// Add Assembly need to apply DI
        /// </summary>
        /// <param name="assembly">Current Assembly</param>
        /// <param name="containerBuilder">Container Builder ( from Component Registrations )</param>
        public static void AddAssembly(string assembly, ContainerBuilder containerBuilder)
        {
            var currentAssembly = Assembly.Load(assembly);

            // Register 
            RegisterDependencies(currentAssembly, containerBuilder);

            // Register Module
            RegisterModule(currentAssembly, containerBuilder);

            // Register Component
            RegisterComponent(currentAssembly, containerBuilder);
        }

        /// <summary>
        /// Add the type into current DependencyRegister
        /// </summary>
        public static void RegisterType<T>()
        {
            _registerTypes.Add(typeof(T));
        }

        /// <summary>
        /// Register Dependency
        /// </summary>
        /// <param name="assembly">Current assembly</param>
        /// <param name="containerBuilder">Container Builder ( from Component Registrations )</param>
        private static void RegisterDependencies(Assembly assembly, ContainerBuilder containerBuilder)
        {
            // Get interface type of IDependency
            var types = assembly.DefinedTypes.Where(type => typeof(IDependency).IsAssignableFrom(type) || typeof(IPerRequestDependency).IsAssignableFrom(type) || typeof(ISingletonDependency).IsAssignableFrom(type));
            foreach (var type in types.Distinct())
            {
                if (type.IsInterface || type.IsAbstract) continue;
                var interfaces = type.GetInterfaces();

                if (type.IsGenericType)
                {
                    var genericRegistration = containerBuilder.RegisterGeneric(type);
                    foreach (var @interface in interfaces)
                    {
                        if (!@interface.IsGenericType) continue;

                        genericRegistration = genericRegistration.As(@interface);
                        if (typeof(ISingletonDependency).IsAssignableFrom(@interface))
                        {
                            // If @interface is ISingletonDependency type
                            // Register as SingleInstance
                            genericRegistration = genericRegistration.SingleInstance();
                        }
                        else if (typeof(IPerRequestDependency).IsAssignableFrom(@interface))
                        {
                            // If @interface is IPerRequestDependency type
                            // Register as InstancePerRequest
                            genericRegistration = genericRegistration.InstancePerRequest();
                        }
                        else
                        {
                            // Register as InstancePerDependency
                            genericRegistration = genericRegistration.InstancePerDependency();
                        }
                    }
                }
                else
                {
                    var registration = containerBuilder.RegisterType(type);

                    foreach (var @interface in interfaces)
                    {
                        registration = registration.As(@interface);

                        if (typeof(ISingletonDependency).IsAssignableFrom(@interface))
                        {
                            // If @interface is ISingletonDependency type
                            // Register as SingleInstance
                            registration = registration.SingleInstance();
                        }
                        else if (typeof(IPerRequestDependency).IsAssignableFrom(@interface))
                        {
                            // If @interface is IPerRequestDependency type
                            // Register as InstancePerRequest
                            registration = registration.InstancePerRequest();
                        }
                        else
                        {
                            // Register as InstancePerDependency
                            registration = registration.InstancePerDependency();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Register Module
        /// </summary>
        /// <param name="assembly">Current assembly</param>
        /// <param name="containerBuilder">Container Builder ( from Component Registrations )</param>
        private static void RegisterModule(Assembly assembly, ContainerBuilder containerBuilder)
        {
            var types = assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && typeof(IModule).IsAssignableFrom(type));
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type) as IModule;
                if (instance != null)
                    containerBuilder.RegisterModule(instance);
            }
        }

        /// <summary>
        /// Register the new Component (Example: the new Third-Party) 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="containerBuilder"></param>
        private static void RegisterComponent(Assembly assembly, ContainerBuilder containerBuilder)
        {
            foreach (var iType in _registerTypes)
            {
                // Get interface type of IDependency
                var types = assembly.DefinedTypes.Where(iType.IsAssignableFrom);
                foreach (var type in types.Distinct())
                {
                    if (type.IsInterface || type.IsAbstract) continue;

                    var registration = containerBuilder.RegisterType(type);
                    var interfaces = type.GetInterfaces();
                    foreach (var @interface in interfaces)
                    {
                        registration = registration.As(@interface);
                        registration.InstancePerDependency();
                    }
                }
            }
        }

    }
}