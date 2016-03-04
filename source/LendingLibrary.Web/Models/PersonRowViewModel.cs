using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LendingLibrary.Web.Models
{
    public class PersonRowViewModel
    {
        public int PersonId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
        public string Mimetype { get; set; }
    }
}