using Entities.DTOs;
using Entities.Exceptions;
using Entities.RequestParameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System.Text.Json;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        public BooksController(IServiceManager context)
        {
            _manager = context;
        }

        [HttpHead]
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters()
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };
                var result = await _manager.BookService.GetAllBooksAsync(linkParameters,false);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.metadata));
            return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);  
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
        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookDtoForInsertion book)
        {
            var bookDto = await _manager.BookService.CreateOneBookAsync(book);
            return StatusCode(201, bookDto);
           
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] BookDtoForUpdate bookDto)
        {   
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
        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
