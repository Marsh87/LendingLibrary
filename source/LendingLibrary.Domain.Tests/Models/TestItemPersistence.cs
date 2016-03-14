using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.DB;
using LendingLibrary.Domain.Tests.Builders;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PeanutButter.TempDb;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.Utils.Entity;

namespace LendingLibrary.Domain.Tests.Models
{
    [TestFixture]
    public class TestItemPersistence
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
                var item= ItemBuilder.BuildRandom();

                using (var context = new LendingLibraryContext(db.CreateConnection()))
                {
                    context.Items.Add(item);
                    context.SaveChangesWithErrorReporting();
                }
                // shake it all about
                using (var context = new LendingLibraryContext(db.CreateConnection()))
                {
                    var persisted = context.Items.FirstOrDefault(o => o.ItemId == item.ItemId);
                    Assert.IsNotNull(persisted);
                    Assert.AreEqual(item.ItemId, persisted.ItemId);
                    Assert.AreEqual(item.Description, persisted.Description);
                    Assert.AreEqual(item.Mimetype, persisted.Mimetype);
                    Assert.AreEqual(item.Photo, persisted.Photo);
                }
                //---------------Test Result -----------------------
            }
        }
    }
}
