using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECOM.Areas.Identity.Data;
using ECOM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECOM.Data
{
    public class ECOMContext : IdentityDbContext<ECOMUser>
    {
        public ECOMContext(DbContextOptions<ECOMContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookStore> BookStores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookPictures> BookPictures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
