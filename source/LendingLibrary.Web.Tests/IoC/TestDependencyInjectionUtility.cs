using System.Web.Mvc;
using LendingLibrary.Web.IoC;
using LendingLibrary.Web.Utils;
using NUnit.Framework;

namespace LendingLibrary.Web.Tests.IoC
{
    [TestFixture]
    public class TestDependencyInjectionUtility
    {
        [Test]
        public void Application_Start_ShouldRegisterControllerFactory()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------
            var original = ControllerBuilder.Current.GetControllerFactory();
            Assert.IsNotInstanceOf<WindsorControllerFactory>(original);
            //---------------Execute Test ----------------------
            DependencyInjectionUtility.Bootstrap();
            var result = ControllerBuilder.Current.GetControllerFactory();
            //---------------Test Result -----------------------
            Assert.IsInstanceOf<WindsorControllerFactory>(result);
        }
    }
}