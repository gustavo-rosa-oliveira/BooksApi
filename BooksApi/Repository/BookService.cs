using BooksApi.Models;
using BooksApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Repository
{
    public class BookService : IBookService
    {
        private readonly BookDbContext _bookDbContext;


        public BookService(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }


        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await _bookDbContext.Books.ToListAsync();
            
            return books;
        }

        public async Task<Book> GetById(int id)
        {
            var book = await _bookDbContext.Books.SingleOrDefaultAsync(x => x.Id == id);

            return book;
        }

        public async Task<Book> GetByName(string title)
        {
            var book = await _bookDbContext.Books.SingleOrDefaultAsync(x => x.Title == title);

            return book;
        }

        public async Task Add(Book book)
        {
            _bookDbContext.Add(book);
            await _bookDbContext.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            _bookDbContext.Update(book);
            await _bookDbContext.SaveChangesAsync();
        }

        public async Task Delete(Book book)
        {
            _bookDbContext.Remove(book);
            await _bookDbContext.SaveChangesAsync();
        }
    }
}
