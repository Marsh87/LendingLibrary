namespace LendingLibrary.DataConstants
{
    public class Tables
    {
        public class Person
        {
            public const string NAME = "People";

            public class Columns
            {
                public const string PersonId = "PersonId";
                public const string FirstName = "FirstName";
                public const string Surname = "Surname";
                public const string PhoneNumber = "PhoneNumber";
                public const string Email = "Email";
                public const string Photo = "Photo";
                public const string Mimetype = "Mimetype";                                
            }
        }
        public class Item
        {
            public const string NAME = "Items";

            public class Columns
            {
                public const string ItemId = "ItemId";
                public const string Title = "Title";
                public const string  Description = "Description";
                public const string Photo = "Photo";
                public const string Mimetype = "Mimetype";
            }
        }
    }
}
