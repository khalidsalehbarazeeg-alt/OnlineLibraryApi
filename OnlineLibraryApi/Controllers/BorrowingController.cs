using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Models;
using OnlineLibraryApi.Services;

namespace OnlineLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly BorrowingService _borrowingService;

        public BorrowingController(BorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _borrowingService.GetAllBorrowingsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null) return NotFound("borrowing not found");
            return Ok(borrowing);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Borrowing borrowing)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newBorrowing = await _borrowingService.AddBorrowingAsync(borrowing);
            return CreatedAtAction(nameof(Get), new { id = newBorrowing.BorrowId }, newBorrowing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Borrowing borrowing)
        {
            var updated = await _borrowingService.UpdateBorrowingAsync(id, borrowing);
            if (updated == null) return NotFound("Book not found");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _borrowingService.DeleteBorrowingAsync(id);
            if (!deleted) return NotFound("Book not found");

            return NoContent();
        }
    }
}
