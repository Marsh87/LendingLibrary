using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.MicroKernel;
using LendingLibrary.Web.IoC;
using NSubstitute;
using NUnit.Framework;

namespace LendingLibrary.Web.Tests.IoC
{

    [TestFixture]
    public class TestWindsorControllerFactory
    {
        [Test]
        public void Construct_GivenAllParameters_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.DoesNotThrow(()=>Create());
        }
        [Test]
        public void Construct_GivenNullKernel_ShouldThrowArgumentNullException()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.Throws<ArgumentNullException>(() => new WindsorControllerFactory(null));
            //---------------Test Result -----------------------
        }
        [Ignore("WIP")]
        [Test]
        public void ReleaseComponent_GivenController_ShouldReturnController()
        {
            //---------------Set up test pack-------------------
            var controller = Substitute.For<IController>();
            var kernel = Substitute.For<IKernel>();
            var windsorControllerFactory=new WindsorControllerFactory(kernel);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            windsorControllerFactory.ReleaseController(controller);
            //---------------Test Result -----------------------
        }

        private static WindsorControllerFactory Create(
            IKernel kernel = null)
        {
            return new WindsorControllerFactory(kernel ?? Substitute.For<IKernel>());
        }
    }
}
