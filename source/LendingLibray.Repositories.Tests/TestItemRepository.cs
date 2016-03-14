using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Models;
using LendingLibrary.Domain.Tests;
using LendingLibrary.Domain.Tests.Builders;
using LendingLibrary.Repositories;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using PeanutButter.Utils.Entity;

namespace LendingLibray.Repositories.Tests
{
    [TestFixture]
    public class TestItemRepository: LendingLibraryContextPerisitenceTestFixtureBase
    {
        [Test]
        public void Construct_GivenNullContext_ShouldThrowANE()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.Throws<ArgumentNullException>(() => new ItemRepository(null));
        }

        [Test]
        public void Construct_GivenConext_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => new ItemRepository(
                Substitute.For<ILendingLibraryContext>()));
        }

        [Test]
        public void Type_ShouldInheritFromIItemRepository()
        {
            //---------------Set up test pack-------------------
            var sut = typeof(ItemRepository);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            sut.ShouldImplement<IItemRepository>();
        }

        [Test]
        public void Save_GivenNewItemEntity_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var Item1 = CreateItemWithId(0);
            var Item2 = CreateItemWithId(1);
            using (var context = GetContext())
            {
                context.Items.Add(Item2);
                context.SaveChanges();
                var existingItem = context.Items.FirstOrDefault(x => x.ItemId == Item1.ItemId);
                Assert.IsNull(existingItem);
            }
            using (var context = GetContext())
            {
                var sut = Create(context);
                //---------------Assert Precondition---------------
                //---------------Execute Test ----------------------
                var expectedId = sut.Save(Item1);
                //---------------Test Result -----------------------
                var savedEntiy = context.Items.FirstOrDefault(x => x.ItemId == expectedId);
                Assert.IsNotNull(savedEntiy);
                Assert.AreEqual(Item1.Description, savedEntiy.Description);
                Assert.AreEqual(Item1.Title, savedEntiy.Title);
                Assert.AreEqual(Item1.Photo, savedEntiy.Photo);
                Assert.AreEqual(Item1.Mimetype, savedEntiy.Mimetype);
            }
        }
        [Test]
        public void Save_GivenExisitingItemEntity_ShouldSave()
        {
            //---------------Set up test pack-------------------
            var Item1 = CreateItemWithId(1);
            using (var context = GetContext())
            {
                context.Items.Add(Item1);
                context.SaveChanges();
                var existingItem = context.Items.FirstOrDefault(x => x.ItemId == Item1.ItemId);
                Assert.IsNotNull(existingItem);
            }
            using (var context = GetContext())
            {
                var sut = Create(context);
                //---------------Assert Precondition---------------
                //---------------Execute Test ----------------------
                var expectedId = sut.Save(Item1);
                //---------------Test Result -----------------------
                var existingEntiy = context.Items.FirstOrDefault(x => x.ItemId == expectedId);
                Assert.IsNotNull(existingEntiy);
                existingEntiy.Title = existingEntiy.Title + "1";
                existingEntiy.Description = existingEntiy.Description + "1";
                existingEntiy.Mimetype = existingEntiy.Mimetype + "1";
                existingEntiy.Photo = new byte[] { 1 };
                var newExpectedId = sut.Save(existingEntiy);
                Assert.AreEqual(expectedId, newExpectedId);
                var entity = context.Items.FirstOrDefault(x => x.ItemId == newExpectedId);
                Assert.IsNotNull(entity);
                Assert.AreEqual(existingEntiy, entity);
            }
        }

        [Test]
        public void GetItem_GivenInValidId_ShouldNotReturnItem()
        {
            //---------------Set up test pack-------------------
            var Item = CreateItemWithId(1);
            using (var context = GetContext())
            {
                context.Items.Clear();
                context.Items.Add(Item);
                context.SaveChanges();
                var ItemRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var invalidId = Item.ItemId + 1;
                var result = ItemRepository.GetItem(invalidId);
                //---------------Test Result -----------------------
                Assert.IsNull(result);
            }
        }

        [Test]
        public void GetItem_GivenValidId_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            var Item = CreateItemWithId(1);
            using (var context = GetContext())
            {
                context.Items.Clear();
                context.Items.Add(Item);
                context.SaveChanges();
                var ItemRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = ItemRepository.GetItem(Item.ItemId);
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(Item.ItemId, result.ItemId);
                Assert.AreEqual(Item.Title, result.Title);
                Assert.AreEqual(Item.Description, result.Description);
                Assert.AreEqual(Item.Mimetype, result.Mimetype);
                Assert.AreEqual(Item.Photo, result.Photo);
            }
        }

        [Test]
        public void GetAllItems_GivenZeroItem_ShouldReturnEmtptyList()
        {
            //---------------Set up test pack-------------------
            using (var context = GetContext())
            {
                context.Items.Clear();
                context.SaveChanges();
                var ItemRepository = Create(context);

                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = ItemRepository.GetAllItems();
                //---------------Test Result -----------------------
                CollectionAssert.IsEmpty(result);
            }
        }

        [Test]
        public void GetAllItems_GivenOneItem_ShouldReturnThatItem()
        {
            //---------------Set up test pack-------------------
            var Item = CreateItemWithId(1);
            using (var context = GetContext())
            {
                context.Items.Add(Item);
                context.SaveChangesWithErrorReporting();
                var ItemRepository = Create(context);

                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = ItemRepository.GetAllItems();
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual(Item.Title, result.FirstOrDefault().Title);
                Assert.AreEqual(Item.Description, result.FirstOrDefault().Description);
                Assert.AreEqual(Item.Photo, result.FirstOrDefault().Photo);
                Assert.AreEqual(Item.Mimetype, result.FirstOrDefault().Mimetype);
            }
        }

        [Test]
        public void GetAllItems_GivenTwoItems_ShouldReturnTwoItems()
        {
            //---------------Set up test pack-------------------
            var Item1 = CreateItemWithId(1);
            var Item2 = CreateItemWithId(2);
            using (var context = GetContext())
            {
                context.Items.Clear();
                context.Items.AddRange(Item1, Item2);
                context.SaveChangesWithErrorReporting();
                var ItemRepository = Create(context);
                //---------------Assert Precondition----------------

                //---------------Execute Test ----------------------
                var result = ItemRepository.GetAllItems();
                //---------------Test Result -----------------------
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
            }
        }

        private static Item CreateItemWithId(int Id)
        {
            return new ItemBuilder().WithRandomProps()
                .WithId(Id)
                .Build();
        }

        private static ItemRepository Create(LendingLibraryContext context)
        {
            return new ItemRepository(context);
        }
    }
}
