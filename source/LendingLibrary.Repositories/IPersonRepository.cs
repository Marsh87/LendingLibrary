using LendingLibrary.Domain.Models;

namespace LendingLibrary.Repositories
{
    public interface IPersonRepository
    {
        int Save(Person person);
    }
}