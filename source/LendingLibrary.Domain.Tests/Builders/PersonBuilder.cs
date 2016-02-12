using LendingLibrary.Domain.Models;
using PeanutButter.TestUtils.Entity;

namespace LendingLibrary.Domain.Tests.Builders
{
    public class PersonBuilder:GenericEntityBuilder<PersonBuilder,Person>
    {
        public PersonBuilder WithId(int Id)
        {
            return WithProp(x => x.PersonId = Id);
        }
    }
}
