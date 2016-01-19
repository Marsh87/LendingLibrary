using LendingLibrary.Web.Controllers;
using NUnit.Framework;

namespace LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void Construct_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(()=>Create());
            //---------------Test Result -----------------------             
        }

        private HomeController Create()
        {
            return new HomeController();
        }
    }
}
