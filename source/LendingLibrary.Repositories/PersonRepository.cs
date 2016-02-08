using System;
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

        public void Save(Person person)
        {
            throw new NotImplementedException();
        }
    }
}