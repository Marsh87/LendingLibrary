using System;
using System.Collections.Generic;
using System.Linq;
using LendingLibrary.Domain;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Repositories
{
    public class PersonRepository:IPersonRepository
    {
        private readonly ILendingLibraryContext _lendingLibraryContext;

        public PersonRepository(ILendingLibraryContext lendingLibraryContext)
        {
            if (lendingLibraryContext == null) throw new ArgumentNullException(nameof(lendingLibraryContext));
            _lendingLibraryContext = lendingLibraryContext;
        }

        public int Save(Person person)
        {
            var entity = _lendingLibraryContext.People
                .FirstOrDefault(x => x.PersonId == person.PersonId);
            if (entity == null)
            {
                _lendingLibraryContext.People.Add(person);
            }
            else
            {
                MapEntityTo(person, entity);
            }
            _lendingLibraryContext.SaveChanges();
            return person.PersonId;
        }

        private static void MapEntityTo(Person person, Person entity)
        {
            entity.PersonId = person.PersonId;
            entity.Email = person.Email;
            entity.FirstName = person.FirstName;
            entity.Mimetype = person.Mimetype;
            entity.PhoneNumber = person.PhoneNumber;
            entity.Photo = person.Photo;
            entity.Surname = person.Surname;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _lendingLibraryContext.People.ToList();
        }

        public Person GetPerson(int personId)
        {
            return _lendingLibraryContext.People
                .FirstOrDefault(x => x.PersonId==personId);
        }
    }
}