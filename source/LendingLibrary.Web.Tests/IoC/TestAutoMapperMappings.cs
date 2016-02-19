using AutoMapper;
using LendingLibrary.Domain.Models;
using LendingLibrary.Web.IoC;
using LendingLibrary.Web.Models;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Web.Tests.IoC
{
    [TestFixture]
    public class TestAutoMapperMappings
    {
        private IMapper Create()
        {
            var bootstrapper = new WindsorBootstrapper(WindsorLifestyles.Transient);
            return bootstrapper.Bootstrap().Resolve<IMapper>();
        }

        [TestCase("PersonId")]
        public void PersonViewModel_To_Person_ShouldMap_(string propertyName)
        {
            //---------------Set up test pack-------------------
            var input = RandomValueGen.GetRandomValue<PersonViewModel>();
            var sut = Create();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Map<PersonViewModel, Person>(input);
            //---------------Test Result -----------------------
            PropertyAssert.AreEqual(input,result,propertyName);
        }
    }
}