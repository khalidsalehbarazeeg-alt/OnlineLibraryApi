using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Models;
using OnlineLibraryApi.Services;

namespace OnlineLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookService.GetAllBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _bookService.GetBookByIdAsync(id);
            if (user == null) return NotFound("Book not found");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newBook = await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = newBook.BookId }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            var updated = await _bookService.UpdateBookAsync(id, book);
            if (updated == null) return NotFound("Book not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteBookAsync(id);
            if (!deleted) return NotFound("Book not found");

            return NoContent();
        }
    }
}
