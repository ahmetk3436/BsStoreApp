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
        public async Task<IActionResult> GetAllBooksAsync()
        {
                var books = await _manager.BookService.GetAllBooksAsync(false);
                return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync(int id)
        {
                var book = await _manager.BookService.GetOneBookByIdAsync(id, false);
                if (book == null)
                {
                    throw new BookNotFoundException(id);
                }
                return Ok(book);
           
        }
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDtoForInsertion book)
        {
            if (book is null)
                return BadRequest();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
               var bookDto = await _manager.BookService.CreateOneBookAsync(book);
            return StatusCode(201, bookDto);
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] BookDtoForUpdate bookDto)
        {   
           
                if (bookDto == null)
                {
                throw new BookNotFoundException(id);
            }
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
                return NoContent();
          
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
           
                var bookToDelete = await _manager.BookService.GetOneBookByIdAsync(id, false);
                if (bookToDelete == null)
                {
                throw new BookNotFoundException(id);
            }
            await _manager.BookService.DeleteOneBookAsync(id, true);
                return NoContent();
          
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync(int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if (bookPatch is null)
                return BadRequest();

            var result = await _manager.BookService.GetOneBookForPatchAsync(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);
            return NoContent();
        }
    }
}
