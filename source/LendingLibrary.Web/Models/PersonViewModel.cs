using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LendingLibrary.DataConstants;

namespace LendingLibrary.Web.Models
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        [MaxLength(FieldSizes.NAME)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Surname")]
        [MaxLength(FieldSizes.NAME)]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(FieldSizes.PHONE)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        [MaxLength(FieldSizes.EMAIL)]
        public string Email { get; set; } 
        public byte[] Photo { get; set; }
        public string MimeType { get; set; }
        [Display(Name = "Photo")]
        public HttpPostedFileBase PhotoAttachment { get; set; }
    }
}