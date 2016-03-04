using System.ComponentModel.DataAnnotations;
using LendingLibrary.DataConstants;

namespace LendingLibrary.Domain.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        [MaxLength(FieldSizes.NAME)]
        public string FirstName { get; set;}
        [MaxLength(FieldSizes.NAME)]
        public string Surname { get; set;}
        [MaxLength(FieldSizes.PHONE)]
        public string PhoneNumber { get; set;}
        [MaxLength(FieldSizes.EMAIL)]
        public string Email { get; set;}
        public byte[] Photo { get; set; }
        public string Mimetype { get; set; }
    }
}