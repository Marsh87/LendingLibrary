using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Domain.Tests.Models
{
    [TestFixture]
    public class TestPerson
    {
        [Test]
        public void Construct_ShouldNotThow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(()=>Create());
            //---------------Test Result -----------------------
        }

        [TestCase("PersonId",typeof(int))]
        [TestCase("FirstName",typeof(string))]
        [TestCase("PhoneNumber",typeof(string))]
        [TestCase("Email",typeof(string))]
        [TestCase("Photos",typeof(byte[]))]
        public void Type_ShouldHaveProperty_(string propertyName,Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof (Person);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName,propertyType);
            //---------------Test Result -----------------------
        }

        private static void Create()
        {
            var person = new Person();
        }
    }
}
