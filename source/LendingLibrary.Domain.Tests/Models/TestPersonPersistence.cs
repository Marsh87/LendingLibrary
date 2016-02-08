using System;
using System.Linq;
using LendingLibrary.DB;
using LendingLibrary.Domain.Models;
using LendingLibrary.Domain.Tests.Builders;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.Utils.Entity;

namespace LendingLibrary.Domain.Tests.Models
{
    [TestFixture]
    public class TestPersonPersistence
    {
        [Test]
        public void ShouldBeAbleToPersistAndRecall()
        {
            using (var db = new TempDBLocalDb())
            {
                var runner = new MigrationsRunner(db.ConnectionString, Console.WriteLine);
                runner.MigrateToLatest();

                //---------------Set up test pack-------------------

                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                // first, put your left leg in
                var person = CreatePerson();
                
                using (var context = new LendingLibraryContext(db.CreateConnection()))
                {
                    context.People.Add(person);
                    context.SaveChangesWithErrorReporting();
                }
                // shake it all about
                using (var context = new LendingLibraryContext(db.CreateConnection()))
                {
                    var persisted = context.People.FirstOrDefault(o => o.PersonId == person.PersonId);
                    Assert.IsNotNull(persisted);
                    Assert.AreEqual(person.Email, persisted.Email);
                    Assert.AreEqual(person.FirstName,persisted.FirstName);
                    Assert.AreEqual(person.PersonId,persisted.PersonId);
                    Assert.AreEqual(person.Surname,persisted.Surname);
                    Assert.AreEqual(person.PhoneNumber,persisted.PhoneNumber);
                    Assert.AreEqual(person.Photo,persisted.Photo);
                }

                //---------------Test Result -----------------------
            }
        }

        private static Person CreatePerson()
        {
            var person = new PersonBuilder()
                .WithRandomProps()
                .Build();
            return person;
        }
    }
}
