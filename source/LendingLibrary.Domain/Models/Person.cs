namespace LendingLibrary.Domain.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set;}
        public string Surname { get; set;}
        public string PhoneNumber { get; set;}
        public string Email { get; set;}
        public byte[] Photo { get; set; }
    }
}