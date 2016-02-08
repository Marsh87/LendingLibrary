using System;
using System.Linq;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Tests;
using LendingLibrary.Domain.Tests.Builders;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PeanutButter.TestUtils.Entity;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Repositories.Tests
{
    [TestFixture]
    public class TestPersonRepository: LendingLibraryContextPerisitenceTestFixtureBase
    {
   
        [Test]
        public void Construct_GivenNullContext_ShouldThrowANE()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.Throws<ArgumentNullException>(() => new PersonRepository(null));
        }

        [Test]
        public void Construct_GivenConext_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
                
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(()=>new PersonRepository(
                Substitute.For<ILendingLibraryContext>()));
        }

        [Test]
        public void Type_ShouldInheritFromIPersonRepository()
        {
            //---------------Set up test pack-------------------
            var sut = typeof (PersonRepository);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            sut.ShouldImplement<IPersonRepository>();
        }

        [Ignore("Fluent Migrator Version Mismatch")]
        [Test]
        public void Save_GivenPersonEntity_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var person = PersonBuilder.BuildRandom();
            using (var context = GetContext())
            {
                var sut=Create(context);
                //---------------Assert Precondition----------------
                CollectionAssert.IsEmpty(context.People);
                //---------------Execute Test ----------------------
                sut.Save(person);
                //---------------Test Result -----------------------
                var savedEntiy = context.People.FirstOrDefault();
                Assert.IsNotNull(savedEntiy);
                Assert.AreEqual(person.Email,savedEntiy.Email);
                Assert.AreEqual(person.FirstName,savedEntiy.FirstName);
                Assert.AreEqual(person.PhoneNumber,savedEntiy.PhoneNumber);
                Assert.AreEqual(person.Photo,savedEntiy.Photo);
                Assert.AreEqual(person.Surname,savedEntiy.Surname);
            }
        }

        private static PersonRepository Create(LendingLibraryContext context)
        {
            return new PersonRepository(context);
        }
    }
}
