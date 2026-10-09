using Microsoft.EntityFrameworkCore;
using OnlineLibraryApi.Data;
using OnlineLibraryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibraryApi.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly AppDbContext _context;

        public BorrowingService(AppDbContext context)
        {
            _context = context;
        }

        // GET all borrowings including related user and book
        public async Task<List<Borrowing>> GetAllBorrowingsAsync()
        {
            return await _context.Borrowings
                .Include(b => b.User)
                .Include(b => b.Book)
                .ToListAsync();
        }

        // GET borrowing by ID
        public async Task<Borrowing> GetBorrowingByIdAsync(int id)
        {
            return await _context.Borrowings
                .Include(b => b.User)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BorrowId == id);
        }

        // POST: add a new borrowing
        public async Task<Borrowing> AddBorrowingAsync(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();
            return borrowing;
        }

        // PUT: update an existing borrowing
        public async Task<Borrowing> UpdateBorrowingAsync(int id, Borrowing borrowing)
        {
            var existing = await _context.Borrowings.FindAsync(id);
            if (existing == null) return null;

            existing.UserId = borrowing.UserId;
            existing.BookId = borrowing.BookId;
            existing.BorrowDate = borrowing.BorrowDate;
            existing.DueDate = borrowing.DueDate;
            existing.ReturnDate = borrowing.ReturnDate;
            existing.Status = borrowing.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        // DELETE: remove a borrowing
        public async Task<bool> DeleteBorrowingAsync(int id)
        {
            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing == null) return false;

            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
