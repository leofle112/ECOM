using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int BookAuthorId { get; set; }
        public BookAuthor BookAuthor { get; set; }
        public int BookStoreId { get; set; }
        public BookStore BookStore { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        public ICollection<BookPictures> BookPictures { get; set; }


    }
}
