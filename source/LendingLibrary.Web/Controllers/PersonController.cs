using System;
using System.Web.Mvc;
using AutoMapper;
using LendingLibrary.Repositories;
using LendingLibrary.Web.Models;

namespace LendingLibrary.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMappingEngine _mappingEngine;

        public PersonController(IPersonRepository personRepository, IMappingEngine mappingEngine)
        {
            if (personRepository == null) throw new ArgumentNullException(nameof(personRepository));
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));
            _personRepository = personRepository;
            _mappingEngine = mappingEngine;
        }

        // GET: Person
        public ActionResult Index()
        {
          
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonViewModel personViewModel)
        {
            if (personViewModel.PhotoAttachment != null)
            {
                var file = personViewModel.PhotoAttachment;
                var content=new byte[personViewModel.PhotoAttachment.ContentLength];
                file.InputStream.Read(content, 0, personViewModel.PhotoAttachment.ContentLength);
                personViewModel.Photo = content;
            }
            if (ModelState.IsValid)
            {
                
            }
            return View(personViewModel);
        }
    }
}