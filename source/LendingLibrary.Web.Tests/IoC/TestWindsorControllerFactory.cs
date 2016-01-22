using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.Windsor;
using LendingLibrary.Web.Controllers;
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
            Assert.DoesNotThrow(() => Create());
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

        [Test]
        public void ReleaseComponent_GivenController_ShouldReturnController()
        {
            //---------------Set up test pack-------------------
            var controller = Substitute.For<IController>();
            var kernel = Substitute.For<IKernel>();
            var windsorControllerFactory = CreateWindsorControllerFactory(kernel);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            windsorControllerFactory.ReleaseController(controller);
            //---------------Test Result -----------------------
            kernel.Received(1).ReleaseComponent(controller);
        }


        [Test]
        public void GetControllerInstance_GivenRequestContextAndControllerType_ShouldResolveControllerType()
        {
            //---------------Set up test pack-------------------
            var kernel = Substitute.For<IKernel>();
            var windsorControllerFactory = new WindsorControllerFactory_EXPOSES_GetControllerInstance(kernel);
            var requestContext = new RequestContext();
            var controllerType = typeof (int);
            var expected = new FooController();
            kernel.Resolve(controllerType).Returns(expected);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = windsorControllerFactory.BaseGetControllerInstance(requestContext, controllerType);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetControllerInstance_GivenRequestContextAndNullControllerType_ShouldThrowHttpException()
        {
            //---------------Set up test pack-------------------
            var kernel = Substitute.For<IKernel>();
            var windsorControllerFactory = new WindsorControllerFactory_EXPOSES_GetControllerInstance(kernel);
            var requestContext = new RequestContext();

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex =
                Assert.Throws<HttpException>(
                    () => windsorControllerFactory.BaseGetControllerInstance(requestContext, null));
            //---------------Test Result -----------------------
            Assert.AreEqual(404, ex.GetHttpCode());
        }

        private class FooController : IController
        {
            public void Execute(RequestContext requestContext)
            {
                throw new NotImplementedException();
            }
        }

        // ReSharper disable once InconsistentNaming
        private class WindsorControllerFactory_EXPOSES_GetControllerInstance
            : WindsorControllerFactory
        {
            public WindsorControllerFactory_EXPOSES_GetControllerInstance(IKernel kernel)
                : base(kernel)
            {
            }

            public IController BaseGetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }

        private static WindsorControllerFactory CreateWindsorControllerFactory(IKernel kernel)
        {
            var windsorControllerFactory = new WindsorControllerFactory(kernel);
            return windsorControllerFactory;
        }

        private static WindsorControllerFactory Create(IKernel kernel = null)
        {
            return new WindsorControllerFactory(kernel ?? Substitute.For<IKernel>());
        }

    }
}