using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOM.ViewModels
{
    public class CreateBookViewModel

        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Descripton { get; set; }
            public string ISBN { get; set; }
            public decimal Price { get; set; }
            public int BookAuthorId { get; set; }
            public int BookStoreId { get; set; }
            public List<IFormFile> Pictures { get; set; }
    }
    }



