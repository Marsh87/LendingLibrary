using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Web.Controllers;
using NUnit.Framework;

namespace LendingLibrary.Web.Tests
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

        private static HomeController Create()
        {
            return new HomeController();
        }
    }
}
