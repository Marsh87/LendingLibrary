﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using LendingLibrary.Repositories;
using LendingLibrary.Web.Controllers;
using LendingLibrary.Web.Models;
using NSubstitute;
using NUnit.Framework;
using PeanutButter.RandomGenerators;
using PeanutButter.TestUtils.Generic;

namespace LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    public class TestPersonController
    {

       [Test]
        public void Construct_ShouldNotThrow()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------

            //---------------Test Result -----------------------
            Assert.DoesNotThrow(()=>new PersonController(Substitute.For<IPersonRepository>(),Substitute.For<IMappingEngine>()));
        }

        [TestCase("personRepository", typeof(IPersonRepository))]
        [TestCase("mappingEngine", typeof(IMappingEngine))]
        public void Construct_ShouldRequire_(string parameterName, Type parameterType)
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            ConstructorTestUtils.ShouldExpectNonNullParameterFor<PersonController>(parameterName, parameterType);
            //---------------Test Result -----------------------
        }
        [Test]
        public void Create_Get_ShouldReturnView()
        {
            //---------------Set up test pack-------------------
            var sut=Create();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Create() as ViewResult;
            //---------------Test Result -----------------------
             Assert.IsNotNull(result);  
        }

        [Test]
        public void Create_Post__GivenInvalidPersonViewModel_ShouldReturnViewModel()
        {
            //---------------Set up test pack-------------------
            var personViewModel = RandomValueGen.GetRandomValue<PersonViewModel>();
            var sut=Create();
            FakeInvalidModelState(sut);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Create(personViewModel) as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
            var model = result.Model as PersonViewModel;
            Assert.AreEqual(personViewModel,model);
        }

        [Test]
        public void Create_Post_GivenValidPersonViewNodel_ShouldReturnBinaryData ()
        {

            var personViewModel = RandomValueGen.GetRandomValue<PersonViewModel>();
            var httpContextSub = Substitute.For<HttpContextBase>();
            var serverSub = Substitute.For<HttpServerUtilityBase>();
            serverSub.MapPath("~/app_data").Returns(@"c:\work\app_data");
            httpContextSub.Server.Returns(serverSub);
            var sut = Create();
            FakeInvalidModelState(sut);
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------
           sut.ControllerContext=new ControllerContext(httpContextSub,new RouteData(),sut);
            //---------------Execute Test ----------------------
            var fileMock = Substitute.For<HttpPostedFileBase>();
            var stream = Substitute.For<Stream>();
            stream.Write(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>());

            fileMock.InputStream.Returns(stream);
            fileMock.ContentLength.Returns(1);
            fileMock.FileName.Returns("file1.jpg");
            personViewModel.PhotoAttachment = fileMock;
            var result = sut.Create(personViewModel) as ViewResult;
            //---------------Test Result -----------------------
            var model = result.Model as PersonViewModel;
            var content = new byte[model.PhotoAttachment.ContentLength];
            Assert.IsNotNull(model.Photo);
            Assert.AreEqual(fileMock.InputStream, model.PhotoAttachment.InputStream);
            Assert.AreEqual(content, model.Photo);
        }
        private static void FakeInvalidModelState(PersonController controller)
        {
            controller.ModelState.AddModelError("", "Test Error");
        }


        private PersonController Create(IPersonRepository personRepository=null,
                                        IMappingEngine mappingEngine = null)
        {
            return new PersonController(
                personRepository??Substitute.For<IPersonRepository>(),
                mappingEngine ?? Substitute.For<IMappingEngine>()
                );
        }
    }
}