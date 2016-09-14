using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LendingLibrary.Domain.Models;
using LendingLibrary.Repositories;
using LendingLibrary.Web.Controllers;
using LendingLibrary.Web.IoC;
using LendingLibrary.Web.Models;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    public class TestItemController
    {
        [Test]
        public void Construct_GivenAllParameters_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => new ItemController(Substitute.For<IItemRepository>(), Substitute.For<IMapper>()));
        }

        [TestCase("itemRepository",typeof(IItemRepository))]
        [TestCase("mapper",typeof(IMapper))]
        public void Construct_ShouldRequire(string parameterName,Type parameterType)
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            ConstructorTestUtils.ShouldExpectNonNullParameterFor<ItemController>(parameterName,parameterType);
            //---------------Test Result -----------------------
        }

        [Test]
        public void Index_ShouldReturnPersonRowViewModel()
        {
            //---------------Set up test pack-------------------
            var itemlist = new List<Item>();
            var item = RandomValueGen.GetRandomValue<Item>();
            itemlist.Add(item);
            var itemRepository = Substitute.For<IItemRepository>();
            var mapper = ResolveMapper();
            itemRepository.GetAllItems().Returns(itemlist);
            var sut = CreateController(itemRepository, mapper);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Index() as ViewResult;
            //---------------Test Result -----------------------
            var model = result.Model as IEnumerable<ItemRowViewModel>;
            Assert.AreEqual(item.ItemId,model.FirstOrDefault().ItemId);
            Assert.AreEqual(item.Description,model.FirstOrDefault().Description);
            Assert.AreEqual(item.Mimetype,model.FirstOrDefault().Mimetype);
            Assert.AreEqual(item.Photo,model.FirstOrDefault().Photo);
            Assert.AreEqual(item.Title,model.FirstOrDefault().Title);
        }

        private static ItemController CreateController(IItemRepository itemRepository=null, IMapper mapper=null)
        {
            return new ItemController(itemRepository??Substitute.For<IItemRepository>(), mapper??Substitute.For<IMapper>());
        }

        private static IMapper ResolveMapper()
        {
            return new WindsorBootstrapper().Bootstrap().Resolve<IMapper>();
        }

    }
}
