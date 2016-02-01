using System.ComponentModel.DataAnnotations;
using LendingLibrary.DataConstants;

namespace LendingLibrary.Domain.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        [MaxLength(FieldSizes.HUMAN_NAME)]
        public string FirstName { get; set;}
        [MaxLength(FieldSizes.HUMAN_NAME)]
        public string Surname { get; set;}
        [MaxLength(FieldSizes.PHONE)]
        public string PhoneNumber { get; set;}
        [MaxLength(FieldSizes.EMAIL)]
        public string Email { get; set;}
        public byte[] Photo { get; set; }
    }
}