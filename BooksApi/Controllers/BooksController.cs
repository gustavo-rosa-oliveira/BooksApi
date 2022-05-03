using BooksApi.Models;
using BooksApi.Persistence;
using BooksApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(List<Book>))]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAll();

            return Ok(books);
        }
        
        /// <summary>
        /// Get a specific book by id.
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesDefaultResponseType(typeof(Book))]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
        
        /// <summary>
        /// Get a specific book by name.
        /// </summary>
        /// <param name="name">Book Title</param>
        /// <returns></returns>
        [HttpGet("{title}")]
        [ProducesDefaultResponseType(typeof(Book))]
        public async Task<IActionResult> Get(string title)
        {
            var book = await _bookService.GetByName(title);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
        
        /// <summary>
        /// Create a new book.
        /// </summary>
        /// <param name="bookInputModel">The book to create</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookInputModel bookInputModel)
        {
            try
            {
                if (bookInputModel == null)
                    return BadRequest();

                var book = new Book(bookInputModel.Title, bookInputModel.Description);
                await _bookService.Add(book);

                return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        
        /// <summary>
        /// Update a specific book.
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="bookInputModel">Updated book data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookInputModel bookInputModel)
        {
            if (bookInputModel == null)
                return BadRequest();

            var book = await _bookService.GetById(id);

            if (book == null)
                return NotFound();

            book.Title = bookInputModel.Title;
            book.Description = bookInputModel.Description;

            await _bookService.Update(book);

            return NoContent();
        }

        /// <summary>
        /// Delete a specific book by id.
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null)
                return NotFound();

            await _bookService.Delete(book);

            return NoContent();
        }
    }
}
