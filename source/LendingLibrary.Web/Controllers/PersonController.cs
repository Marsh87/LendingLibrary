﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using AutoMapper;
using LendingLibrary.Domain.Models;
using LendingLibrary.Repositories;
using LendingLibrary.Web.Models;

namespace LendingLibrary.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            if (personRepository == null) throw new ArgumentNullException(nameof(personRepository));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository;
            _mapper = mapper;
        }

        // GET: Person
        public ActionResult Index()
        {
            var persons = _personRepository.GetAllPersons();
            var model=_mapper.Map<IEnumerable<Person>,IEnumerable<PersonRowViewModel>> (persons);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       // [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            if (personViewModel.PhotoAttachment != null)
            {
                var file = personViewModel.PhotoAttachment;
                var content=new byte[personViewModel.PhotoAttachment.ContentLength];
                file.InputStream.Read(content, 0, personViewModel.PhotoAttachment.ContentLength);
                personViewModel.Photo = content;
                personViewModel.MimeType =personViewModel.PhotoAttachment.ContentType;
            }
            if (ModelState.IsValid)
            {
                var person = _mapper.Map<PersonViewModel, Person>(personViewModel);
                _personRepository.Save(person);
                return RedirectToAction("Index");
            }
            return View(personViewModel);
        }

        public ActionResult GetImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return File(ms.ToArray(), "image/jpg");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var person = _personRepository.GetPerson(id);
            var model = _mapper.Map<PersonViewModel>(person);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel personViewModel)
        {
            if (personViewModel.PhotoAttachment != null)
            {
                var file = personViewModel.PhotoAttachment;
                var content = new byte[personViewModel.PhotoAttachment.ContentLength];
                file.InputStream.Read(content, 0, personViewModel.PhotoAttachment.ContentLength);
                personViewModel.Photo = content;
                personViewModel.MimeType = personViewModel.PhotoAttachment.ContentType;
            }

            if (ModelState.IsValid)
            {
                var person = _mapper.Map<Person>(personViewModel);
                _personRepository.Save(person);
                return RedirectToAction("Index");
            }
            return View(personViewModel);
        }
    }
}