using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.Models
{
    public class BookPictures
    {
        public int Id { get; set; }
        public string PictureUri { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
    }
}
