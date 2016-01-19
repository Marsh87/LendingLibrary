using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

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
        private WindsorLifestyles _defaultLifeStyle;

        public WindsorBootstrapper(WindsorLifestyles defaultLifestyle = WindsorLifestyles.PerWebRequest)
        {
            _defaultLifeStyle = defaultLifestyle;
        }
        public IWindsorContainer Bootstrap()
        {
            var container = new WindsorContainer();
            container.Register(Classes.FromAssembly(GetType().Assembly)
                .BasedOn<IController>()
                .WithLifestyle(_defaultLifeStyle));
            return container;
        }
    }

    public static class BasedOnExtensions
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
    }
}