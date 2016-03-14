using System;
using LendingLibrary.Domain.Models;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Domain.Tests.Models
{
    [TestFixture]
    public class TestItem
    {
        [Test]
        public void Construct_ShouldNotThow()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => Create());
            //---------------Test Result -----------------------
        }

        [TestCase("ItemId", typeof(int))]
        [TestCase("Title", typeof(string))]
        [TestCase("Description", typeof(string))]
        [TestCase("Photo", typeof(byte[]))]
        [TestCase("Mimetype", typeof(string))]  
        public void Type_ShouldHaveProperty_(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Item);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }

        private static Item Create()
        {
            return new Item();
        }
    }
}
