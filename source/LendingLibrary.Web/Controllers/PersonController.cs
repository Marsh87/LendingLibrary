using System;
using System.Web.Mvc;
using LendingLibrary.Repositories;
using LendingLibrary.Web.Models;

namespace LendingLibrary.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            if (personRepository == null) throw new ArgumentNullException("personRepository");
            _personRepository = personRepository;
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