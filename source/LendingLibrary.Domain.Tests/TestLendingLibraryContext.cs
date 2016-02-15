using System.Configuration;
using System.Data.Entity;
using System.Linq;
using NSubstitute.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.TestUtils.Generic;

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

        [Test]
        public void ParameterlessConstructor_ShouldUseConnectionStringCalled_DefaultConnection()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------
            using (var tempDb = new TempDBLocalDb())
            {
                //---------------Execute Test ----------------------
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString =
                    tempDb.ConnectionString;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                var sut = new LendingLibraryContext();
                //---------------Test Result -----------------------
                var conn = sut.Database.Connection;
                Assert.AreEqual(tempDb.ConnectionString, conn.ConnectionString);
            }
        }
    }
}
