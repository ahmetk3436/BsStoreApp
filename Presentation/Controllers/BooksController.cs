using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager context)
        {
            _manager = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
        }
        [HttpGet("{id}")]
        public IActionResult GetOneBook(int id)
        {
                var book = _manager.BookService.GetOneBookById(id, false);
                if (book == null)
                {
                    throw new BookNotFoundException(id);
                }
                return Ok(book);
           
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
           
                if (book == null)
                {
                throw new BookNotFoundException(book.Id);
            }
                _manager.BookService.CreateOneBook(book);
                return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, book);
           
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDtoForUpdate bookDto)
        {   
           
                if (bookDto == null)
                {
                throw new BookNotFoundException(id);
            }
               
                _manager.BookService.UpdateOneBook(id, bookDto, true);
                return NoContent();
          
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
           
                var bookToDelete = _manager.BookService.GetOneBookById(id, false);
                if (bookToDelete == null)
                {
                throw new BookNotFoundException(id);
            }
                _manager.BookService.DeleteOneBook(id, true);
                return NoContent();
          
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook(int id, [FromBody] JsonPatchDocument<Book> patchDoc)
        {
            var entity = _manager.BookService.GetOneBookById(id, true);

            patchDoc.ApplyTo(entity);
            _manager.BookService.UpdateOneBook(id, new BookDtoForUpdate(entity.Id,entity.Title,entity.Price), true);
            return NoContent();
        }
    }
}
