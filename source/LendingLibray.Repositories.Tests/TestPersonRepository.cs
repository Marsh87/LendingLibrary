﻿using System;
using System.Linq;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Models;
using LendingLibrary.Domain.Tests;
using LendingLibray.Repositories.Tests;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.TestUtils.Generic;
using PeanutButter.Utils.Entity;

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
            var person1 = CreatePersonWithId(0);
            var person2 = CreatePersonWithId(1);
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
                Assert.AreEqual(person1.Mimetype,savedEntiy.Mimetype);
            }
        }
        [Test]
        public void Save_GivenExisitingPersonEntity_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var person1 = CreatePersonWithId(1);
            using (var context = GetContext())
            {
                context.People.Add(person1);
                context.SaveChanges();
                var existingperson = context.People.FirstOrDefault(x => x.PersonId == person1.PersonId);
                Assert.IsNotNull(existingperson);
            }
            using (var context = GetContext())
            { 
                var sut = Create(context);
                //---------------Assert Precondition---------------
                //---------------Execute Test ----------------------
                var expectedId=sut.Save(person1);
                //---------------Test Result -----------------------
                var existingEntiy = context.People.FirstOrDefault(x=>x.PersonId==expectedId);
                Assert.IsNotNull(existingEntiy);
                existingEntiy.Email = existingEntiy.Email+"1";
                existingEntiy.FirstName = existingEntiy.FirstName + "1";
                existingEntiy.Mimetype = existingEntiy.Mimetype + "1";
                existingEntiy.PhoneNumber = existingEntiy.PhoneNumber.Substring(1);
                existingEntiy.Photo=new byte[] {1};
                var newExpectedId = sut.Save(existingEntiy);
                Assert.AreEqual(expectedId,newExpectedId);
                var entity = context.People.FirstOrDefault(x => x.PersonId ==newExpectedId);
                Assert.IsNotNull(entity);
                Assert.AreEqual(existingEntiy,entity);
            }
        }

        [Test]
        public void GetPerson_GivenInValidId_ShouldNotReturnPerson()
        {
            //---------------Set up test pack-------------------
            var person=CreatePersonWithId(1);
            using (var context = GetContext())
            {
                context.People.Clear();
                context.People.Add(person);
                context.SaveChanges();
                var personRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var invalidId = person.PersonId+1;
                var result = personRepository.GetPerson(invalidId);
                //---------------Test Result -----------------------
                Assert.IsNull(result);
            }
        }

        [Test]
        public void GetPerson_GivenValidId_ShouldReturnThatPerson()
        {
            //---------------Set up test pack-------------------
            var person=CreatePersonWithId(1);
            using (var context = GetContext())
            {
                context.People.Clear();
                context.People.Add(person);
                context.SaveChanges();
                var personRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = personRepository.GetPerson(person.PersonId);
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(person.PersonId,result.PersonId);
                Assert.AreEqual(person.Email,result.Email);
                Assert.AreEqual(person.FirstName,result.FirstName);
                Assert.AreEqual(person.Mimetype,result.Mimetype);
                Assert.AreEqual(person.PhoneNumber,result.PhoneNumber);
                Assert.AreEqual(person.Photo,result.Photo);
                Assert.AreEqual(person.Surname,result.Surname);
            }
        }

        [Test]
        public void GetAllPersons_GivenZeroPerson_ShouldReturnEmtptyList()
        {
            //---------------Set up test pack-------------------
            using (var context = GetContext())
            {
                context.People.Clear();
                context.SaveChanges();
                var personRepository = Create(context);

                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = personRepository.GetAllPersons();
                //---------------Test Result -----------------------
                CollectionAssert.IsEmpty(result);
            }
        }

        [Test]
        public void GetAllPersons_GivenOnePerson_ShouldReturnThatPerson()
        {
            //---------------Set up test pack-------------------
            var person = CreatePersonWithId(1);
            using (var context = GetContext())
            {
                context.People.Add(person);
                context.SaveChangesWithErrorReporting();
                var personRepository = Create(context);

                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = personRepository.GetAllPersons();
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(1,result.Count());
                Assert.AreEqual(person.Email,result.FirstOrDefault().Email);
                Assert.AreEqual(person.FirstName,result.FirstOrDefault().FirstName);
                Assert.AreEqual(person.PhoneNumber,result.FirstOrDefault().PhoneNumber);
                Assert.AreEqual(person.Surname,result.FirstOrDefault().Surname);
                Assert.AreEqual(person.Photo,result.FirstOrDefault().Photo);
                Assert.AreEqual(person.Mimetype,result.FirstOrDefault().Mimetype);
            }
        }

        [Test]
        public void GetAllPersons_GivenTwoPersons_ShouldReturnTwoPersons()
        {
            //---------------Set up test pack-------------------
            var person1 = CreatePersonWithId(1);
            var person2= CreatePersonWithId(2);
            using (var context = GetContext())
            {
                context.People.AddRange(person1,person2);
                context.SaveChangesWithErrorReporting();
                var personRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = personRepository.GetAllPersons();
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(2,result.Count());
            }
        }

        private static Person CreatePersonWithId(int Id)
        {
            return new PersonBuilder().WithRandomProps()
                .WithId(Id)
                .WithFirstName()
                .WithSurname()
                .WithPhoneNumber()
                .Build();
        }

        private static PersonRepository Create(LendingLibraryContext context)
        {
            return new PersonRepository(context);
        }
    }
}
