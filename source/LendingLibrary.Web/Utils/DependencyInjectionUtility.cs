using System.Web.Mvc;
using Castle.Windsor;
using LendingLibrary.Web.IoC;

namespace LendingLibrary.Web.Utils
{
    public class DependencyInjectionUtility
    {
        public static IWindsorContainer Bootstrap()
        {
            var bootstrapper = new WindsorBootstrapper();
            var container = bootstrapper.Bootstrap();
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            return container;
        }
    }
}