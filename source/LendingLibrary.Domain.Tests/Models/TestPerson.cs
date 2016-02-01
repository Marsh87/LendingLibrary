using System;
using LendingLibrary.Domain.Models;
using NUnit.Framework;
using PeanutButter.TestUtils.Entity;
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
        [TestCase("FirstName", typeof(string))]
        [TestCase("Surname",typeof(string))]
        [TestCase("PhoneNumber",typeof(string))]
        [TestCase("Email",typeof(string))]
        [TestCase("Photo",typeof(byte[]))]
        public void Type_ShouldHaveProperty_(string propertyName,Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof (Person);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName,propertyType);
            //---------------Test Result -----------------------
        }

        [Test]
        public void FirstName_ShouldHaveMaxLength_512()
        {
            //---------------Set up test pack-------------------
            var sut=new Person();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveMaxLengthOf(512,o=>o.FirstName);
            //---------------Test Result -----------------------
        }

        [Test]
        public void Surname_ShouldHaveMaxLength_512()
        {
            //---------------Set up test pack-------------------
            var sut=new Person();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveMaxLengthOf(512,o=>o.Surname);
            //---------------Test Result -----------------------
        }

        [Test]
        public void Email_ShouldHaveMaxLength_512()
        {
            //---------------Set up test pack-------------------
            var sut=new Person();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveMaxLengthOf(512,o=>o.Email);
            //---------------Test Result -----------------------
        }

        [Test]
        public void PhoneNumber_ShouldHaveMaxLength_10()
        {
            //---------------Set up test pack-------------------
            var sut=new Person();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveMaxLengthOf(10,o=>o.PhoneNumber);
            //---------------Test Result -----------------------
        }

      
        private static void Create()
        {
            var person = new Person();
        }
    }
}
