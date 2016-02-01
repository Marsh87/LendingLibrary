using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingLibrary.Domain.Models;
using PeanutButter.RandomGenerators;
using PeanutButter.TestUtils.Entity;

namespace LendingLibrary.Domain.Tests.Builders
{
    public class PersonBuilder:GenericEntityBuilder<PersonBuilder,Person>
    {
        public PersonBuilder WithFirstName()
        {
            return WithProp(x => x.FirstName = RandomValueGen.GetRandomString(1, 512));
        }
        public PersonBuilder WithSurname()
        {
            return WithProp(x => x.Surname = RandomValueGen.GetRandomString(1, 512));
        }
        public PersonBuilder WithPhoneNumber()
        {
            return WithProp(x => x.PhoneNumber = RandomValueGen.GetRandomString(1, 10));
        }
        public PersonBuilder WithEmail()
        {
            return WithProp(x => x.Email = RandomValueGen.GetRandomString(1,512));
        }
    }
}
