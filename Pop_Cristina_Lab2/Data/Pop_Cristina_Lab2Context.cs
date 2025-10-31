using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Data
{
    public class Pop_Cristina_Lab2Context : DbContext
    {
        public Pop_Cristina_Lab2Context(DbContextOptions<Pop_Cristina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Author { get; set; }

        // Lab 3
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
    }
}
