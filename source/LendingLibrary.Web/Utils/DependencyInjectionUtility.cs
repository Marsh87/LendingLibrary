using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LendingLibrary.Web.IoC;

namespace LendingLibrary.Web.Utils
{
    public class DependencyInjectionUtility
    {
        public static void Bootstrap()
        {
            var bootstrapper = new WindsorBootstrapper();
            var container = bootstrapper.Bootstrap();
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

        }
    }
}