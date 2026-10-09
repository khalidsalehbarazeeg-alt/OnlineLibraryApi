using Microsoft.EntityFrameworkCore;
using OnlineLibraryApi.Data;
using OnlineLibraryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibraryApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        // GET all authors with their books
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .ToListAsync();
        }

        // GET author by ID with their books
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        // POST: add new author
        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        // PUT: update existing author
        public async Task<Author> UpdateAuthorAsync(int id, Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null) return null;

            existingAuthor.Name = author.Name;
            existingAuthor.Nationality = author.Nationality;
            existingAuthor.Biography = author.Biography;

            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        // DELETE: remove an author
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
