using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using LendingLibrary.DB;
using LendingLibrary.Web.Config;
using LendingLibrary.Web.Utils;

namespace LendingLibrary.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private  IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           _container= DependencyInjectionUtility.Bootstrap();
           var connectionString = GetConnectionString();
            MigrateDatabaseWith(connectionString);

        }
        private string GetConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (setting == null)
                throw new Exception("Please define the DefaultConnection connection string in Web.Config");
            return setting.ConnectionString;
        }

        private string GetConnectionString1()
        {
            var connectionStringConfig = _container.Resolve<IConnectionStringConfig>();
            return connectionStringConfig.DefaultConnection;
        }

        private void MigrateDatabaseWith(string connectionString)
        {
            var runner = new MigrationsRunner(connectionString);
            runner.MigrateToLatest();
        }
    }
}
