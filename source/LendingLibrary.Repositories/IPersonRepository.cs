using System.Collections.Generic;
using LendingLibrary.Domain.Models;

namespace LendingLibrary.Repositories
{
    public interface IPersonRepository
    {
        int Save(Person person);
        IEnumerable<Person> GetAllPersons();
    }
}