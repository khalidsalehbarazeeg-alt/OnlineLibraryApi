using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Models;
using OnlineLibraryApi.Services;

namespace OnlineLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthersController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthersController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _authorService.GetAllAuthorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _authorService.GetAuthorByIdAsync(id);
            if (user == null) return NotFound("Author not found");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newAuthor = await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(Get), new { id = newAuthor.AuthorId }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Author author)
        {
            var updated = await _authorService.UpdateAuthorAsync(id, author);
            if (updated == null) return NotFound("Author not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorService.DeleteAuthorAsync(id);
            if (!deleted) return NotFound("Author not found");

            return NoContent();
        }
    }
}
