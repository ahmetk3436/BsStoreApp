using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;
using Repositories.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager context)
        {
            _manager = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.Book.GetAllBooks(false);
                return Ok(books);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOneBook(int id)
        {
            try
            {
                var book = _manager.Book.GetOneBookById(id, false);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                _manager.Book.CreateOneBook(book);
                _manager.Save();
                return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, book);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest();
                }
                var bookToUpdate = _manager.Book.GetOneBookById(id, true);
                if (bookToUpdate == null)
                {
                    return NotFound();
                }
                _manager.Book.UpdateOneBook(bookToUpdate);
                _manager.Save();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var bookToDelete = _manager.Book.GetOneBookById(id, false);
                if (bookToDelete == null)
                {
                    return NotFound();
                }
                _manager.Book.DeleteOneBook(bookToDelete);
                _manager.Save();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook(int id, [FromBody] JsonPatchDocument<Book> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest();
                }
                var bookToUpdate = _manager.Book.GetOneBookById(id, true);
                if (bookToUpdate == null)
                {
                    return NotFound();
                }
                var bookToPatch = new Book()
                {
                    Id = bookToUpdate.Id,
                    Title = bookToUpdate.Title,
                    Price = bookToUpdate.Price,
                };
                patchDoc.ApplyTo(bookToPatch);
                if (!TryValidateModel(bookToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                _manager.Book.UpdateOneBook(bookToPatch);
                _manager.Save();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
