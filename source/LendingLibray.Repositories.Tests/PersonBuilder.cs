using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain.Models;
using PeanutButter.RandomGenerators;

namespace LendingLibray.Repositories.Tests
{
    public class PersonBuilder: GenericBuilder<PersonBuilder, Person>
    {
        public PersonBuilder WithId(int id)
        {
            return WithProp(o => o.PersonId = id);
        }
        public PersonBuilder WithFirstName()
        {
            return WithProp(o => o.FirstName=RandomValueGen.GetRandomString(1,13));
        }
        public PersonBuilder WithSurname()
        {
            return WithProp(o => o.Surname=RandomValueGen.GetRandomString(1,13));
        }
        public PersonBuilder WithPhoneNumber()
        {
            return WithProp(o => o.PhoneNumber=RandomValueGen.GetRandomString(1,10));
        }
    }
}
