using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Persistence
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }


        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
    }
}
