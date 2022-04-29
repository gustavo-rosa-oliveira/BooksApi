using BooksApi.Models;
using BooksApi.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _bookDbContext;

        public BooksController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(List<Book>))]
        public IActionResult Get()
        {
            return Ok(_bookDbContext.Books.ToList());
        }

        /// <summary>
        /// Get a specific book by id.
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesDefaultResponseType(typeof(Book))]
        public IActionResult Get(int id)
        {
            var book = _bookDbContext.Books.SingleOrDefault(b => b.Id == id);

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
        public IActionResult Get(string title)
        {
            var book = _bookDbContext.Books.SingleOrDefault(b => b.Title == title);

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
        public IActionResult Post([FromBody] BookInputModel bookInputModel)
        {
            try
            {
                if (bookInputModel == null)
                    return BadRequest();

                var book = new Book(bookInputModel.Title, bookInputModel.Description);
                _bookDbContext.Books.Add(book);
                _bookDbContext.SaveChanges();

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
        public IActionResult Put(int id, [FromBody] BookInputModel bookInputModel)
        {
            if (bookInputModel == null)
                return BadRequest();

            var book = _bookDbContext.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            book.Title = bookInputModel.Title;
            book.Description = bookInputModel.Description;

            _bookDbContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Delete a specific book by id.
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookDbContext.Books.SingleOrDefault(p => p.Id == id);

            if (book == null)
                return NotFound();

            _bookDbContext.Remove(book);
            _bookDbContext.SaveChanges();

            return NoContent();
        }
    }
}
