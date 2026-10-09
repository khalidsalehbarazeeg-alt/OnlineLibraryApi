using Microsoft.EntityFrameworkCore;
using OnlineLibraryApi.Data;
using OnlineLibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibraryApi.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        // GET all books including authors
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
        }

        // GET book by ID including authors
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        // POST: add a new book
        public async Task<Book> AddBookAsync(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        // PUT: update a book
        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (existingBook == null) return null;

            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.Description = book.Description;
            existingBook.PublishYear = book.PublishYear;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.AvailableCopies = book.AvailableCopies;

            // تحديث المؤلفين (اختياري حسب احتياجك)
            // existingBook.BookAuthors = book.BookAuthors;

            await _context.SaveChangesAsync();
            return existingBook;
        }

        // DELETE: delete a book
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
