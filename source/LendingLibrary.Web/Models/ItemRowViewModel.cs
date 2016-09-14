using System.ComponentModel.DataAnnotations;

namespace LendingLibrary.Web.Models
{
    public class ItemRowViewModel
    {
        public int ItemId { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name ="Description")]
        public string Description { get; set; }
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
        public string Mimetype { get; set; }
    }
}