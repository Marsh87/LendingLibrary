namespace LendingLibrary.Domain.Tests.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set;}
        public string PhoneNumber { get; set;}
        public string Email { get; set;}
        public byte[] Photos { get; set; }
    }
}