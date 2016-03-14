using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingLibrary.Domain.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public string Mimetype { get; set; }
    }
}
