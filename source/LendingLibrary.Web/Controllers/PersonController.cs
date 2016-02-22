using System;
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
                var person = _mapper.Map<PersonViewModel, Person>(personViewModel);
                _personRepository.Save(person);
                return RedirectToAction("Index");
            }
            return View(personViewModel);
        }
    }
}