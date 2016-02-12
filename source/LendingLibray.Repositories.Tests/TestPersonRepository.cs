using System;
using System.Linq;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Tests;
using LendingLibrary.Domain.Tests.Builders;
using NSubstitute;
using NUnit.Framework;
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

        [Test]
        public void Save_GivenNewPersonEntity_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var person1 = new PersonBuilder()
                .WithRandomProps()
                .WithId(0)
                .Build();
            var person2 = new PersonBuilder()
                .WithRandomProps()
                .WithId(1)
                .Build();
            using (var context = GetContext())
            {
                context.People.Add(person2);
                context.SaveChanges();
                var existingperson = context.People.FirstOrDefault(x => x.PersonId == person1.PersonId);
                Assert.IsNull(existingperson);
            }
            using (var context = GetContext())
            { 
                var sut = Create(context);
                //---------------Assert Precondition---------------
                //---------------Execute Test ----------------------
                var expectedId=sut.Save(person1);
                //---------------Test Result -----------------------
                var savedEntiy = context.People.FirstOrDefault(x=>x.PersonId==expectedId);
                Assert.IsNotNull(savedEntiy);
                Assert.AreEqual(person1.Email,savedEntiy.Email);
                Assert.AreEqual(person1.FirstName,savedEntiy.FirstName);
                Assert.AreEqual(person1.PhoneNumber,savedEntiy.PhoneNumber);
                Assert.AreEqual(person1.Photo,savedEntiy.Photo);
                Assert.AreEqual(person1.Surname,savedEntiy.Surname);
            }
        }

        private static PersonRepository Create(LendingLibraryContext context)
        {
            return new PersonRepository(context);
        }
    }
}
