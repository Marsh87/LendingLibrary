using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.TestUtils.Generic;
using PeanutButter.Utils;

namespace LendingLibrary.Domain.Tests
{
    [TestFixture]
    public class TestLendingLibraryContext
    {
        [Test]
        public void Type_ShouldInheritFrom_DbContext()
        {
            //---------------Set up test pack-------------------
            var sut = typeof (LendingLibraryContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldInheritFrom<DbContext>();
            //---------------Test Result -----------------------
        }
        [Test]
        public void Type_ShouldImplementFrom_ILendingLibraryContext()
        {
            //---------------Set up test pack-------------------
            var sut = typeof (LendingLibraryContext);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldImplement<ILendingLibraryContext>();
            //---------------Test Result -----------------------
        }

        [Test]
        public void StaticConstructor_ShouldNotAllowEntityMigrations()
        {
            //---------------Set up test pack-------------------
            using (var tempDb = new TempDBLocalDb())
            using (var dbConnection = tempDb.CreateConnection())
            using (var context = new LendingLibraryContext(dbConnection))
            {
                try
                {
                    // force Entity to do whatever it does when it
                    // spins up but don't care if the query onto Persons
                    //  fails (which it will at time of writing because
                    //  we haven't written any migrations to create the
                    // table)
                    context.People.ToArray();
                }
                catch
                {
                }
                //---------------Assert Precondition----------------
                //---------------Execute Test ----------------------
                using (var cmd = dbConnection.CreateCommand())
                {
                    cmd.CommandText =
                        @"select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = '__MigrationHistory';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        //---------------Test Result -----------------------
                        Assert.IsFalse(reader.Read());
                    }
                }

            }
        }
    }
}
