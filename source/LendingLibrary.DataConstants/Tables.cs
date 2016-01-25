using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingLibrary.DataConstants
{
    public class Tables
    {
        public class Person
        {
            public const string NAME = "Persons";

            public class Columns
            {
                public const string PersonId = "PersonId";
                public const string FirstName = "FirstName";
                public const string Surname = "Surname";
                public const string PhoneNumber = "PhoneNumber";
                public const string Email = "Email";
                public const string Photo = "Photo";
            }
        }
    }
}
