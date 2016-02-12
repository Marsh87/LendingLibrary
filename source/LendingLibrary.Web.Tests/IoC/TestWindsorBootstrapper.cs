using Castle.Windsor;
using LendingLibrary.Web.Controllers;
using LendingLibrary.Web.IoC;
using NUnit.Framework;

namespace LendingLibrary.Web.Tests.IoC
{
    [TestFixture]
    public class TestWindsorBootstrapper
    {
        [Test]
        public void Construct_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => Create());
            //---------------Test Result -----------------------
        }
        [Test]
        public void Bootstrap_ShouldReturnContainer()
        {
            //---------------Set up test pack-------------------
            var sut = Create();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Bootstrap();
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IWindsorContainer>(result);
        }

        [Test]
        public void Bootstrap_ShouldReturnHomeController()
        {
            //---------------Set up test pack-------------------
            var sut = Create();
            //---------------Assert Precondition----------------
           
            //---------------Execute Test ----------------------
            var container = sut.Bootstrap();
            //---------------Test Result -----------------------
            Assert.IsNotNull(container);
            var homeController=container.Resolve<HomeController>();
            Assert.IsNotNull(homeController);
        }

        private static WindsorBootstrapper Create()
        {
            return new WindsorBootstrapper(WindsorLifestyles.Transient);
        }
    }
}
