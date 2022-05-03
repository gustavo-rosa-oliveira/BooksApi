using BooksApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Repository
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();

        Task<Book> GetById(int id);

        Task<Book> GetByName(string title);

        Task Add(Book book);

        Task Update(Book book);

        Task Delete(Book book);
    }
}
