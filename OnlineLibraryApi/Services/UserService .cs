using Microsoft.EntityFrameworkCore;
using OnlineLibraryApi.Data;
using OnlineLibraryApi.Models;

namespace OnlineLibraryApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Borrowings)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Borrowings)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
                return null;

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.Phone = user.Phone;
            existing.Password = user.Password;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
