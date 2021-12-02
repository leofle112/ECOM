using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.Models
{
    public class BookCategory
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
