using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.ModelBuilder.Descriptors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using LendingLibrary.Domain;
using LendingLibrary.Repositories;
using PeanutButter.Utils;

namespace LendingLibrary.Web.IoC
{
    public enum WindsorLifestyles
    {
        Unknown,
        Transient,
        Singleton,
        PerWebRequest
    }

    public class WindsorBootstrapper
    {
        private readonly WindsorLifestyles _defaultLifeStyle;

        public WindsorBootstrapper(WindsorLifestyles defaultLifestyle = WindsorLifestyles.PerWebRequest)
        {
            _defaultLifeStyle = defaultLifestyle;
        }
        public IWindsorContainer Bootstrap()
        {
            var container = new WindsorContainer();
            RegisterAllControllersFromThisAssembly(container);
            container.RegisterWithLifestyle<ILendingLibraryContext, LendingLibraryContext>(_defaultLifeStyle);
            container.RegisterAllOneToOneResolutionsIn(typeof(ILendingLibraryContext).Assembly);
            container.RegisterAllOneToOneResolutionsIn(typeof(IPersonRepository).Assembly);
            return container;
        }

        private void RegisterAllControllersFromThisAssembly(WindsorContainer container)
        {
            container.Register(Classes.FromAssembly(GetType().Assembly)
                .BasedOn<IController>()
                .WithLifestyle(_defaultLifeStyle));
        }

    }

    public static class WindsorHelperExtensions
    {
        public static BasedOnDescriptor WithLifestyle(this BasedOnDescriptor input, WindsorLifestyles lifestyle)
        {
            switch (lifestyle)
            {
                case WindsorLifestyles.Transient:
                    return input.LifestyleTransient();
                case WindsorLifestyles.PerWebRequest:
                    return input.LifestylePerWebRequest();
                default:
                    throw new NotImplementedException("No strategy to deal with lifestyle: " + lifestyle);
            }
        }

        public static ComponentRegistration<T> WithLifestyle<T>(this ComponentRegistration<T> registration,
            WindsorLifestyles lifestyle) where T: class
        {
            switch (lifestyle)
            {
                case WindsorLifestyles.Transient:
                    return registration.LifestyleTransient();
                case WindsorLifestyles.PerWebRequest:
                    return registration.LifestylePerWebRequest();
                default:
                    throw new NotImplementedException("No strategy to deal with lifestyle: " + lifestyle);
            }

        }

        public static void RegisterWithLifestyle<TService, TImplementation>(
            this WindsorContainer container,
            WindsorLifestyles lifeStyle) where TService:class
                                         where TImplementation: TService
        {
            container.Register(Component.For<TService>()
                                .ImplementedBy<TImplementation>()
                                .WithLifestyle(lifeStyle));
        }

        public static void RegisterAllOneToOneResolutionsIn(this WindsorContainer container, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var interfaceTypes = types.Where(t => t.IsInterface);
            var concreteTypes = types.Where(t => t.IsPublic &&
                                                 !t.IsInterface &&
                                                 !t.IsAbstract &&
                                                 !t.IsGenericType);
            interfaceTypes.ForEach(iface =>
            {
                var implementingTypes = concreteTypes.Where(iface.IsAssignableFrom).ToArray();
                if (implementingTypes.Length != 1)
                    return;
                var implementingType = implementingTypes[0];
                if (container.Kernel.HasComponent(iface))
                    return;
                container.Register(Component.For(iface)
                    .ImplementedBy(implementingType)
                    .LifestyleTransient());
            });
        }

    }
}