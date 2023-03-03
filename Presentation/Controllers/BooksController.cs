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
        public IActionResult CreateBook([FromBody] BookDtoForInsertion book)
        {
            if (book is null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
               var bookDto = _manager.BookService.CreateOneBook(book);
            return StatusCode(201, bookDto);
           
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookDtoForUpdate bookDto)
        {   
           
                if (bookDto == null)
                {
                throw new BookNotFoundException(id);
            }
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            _manager.BookService.UpdateOneBook(id, bookDto, false);
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
        public IActionResult PartiallyUpdateBook(int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = _manager.BookService.GetOneBookForPatch(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate, result.book);
            return NoContent();
        }
    }
}
