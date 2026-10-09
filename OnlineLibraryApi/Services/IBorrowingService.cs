using OnlineLibraryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibraryApi.Services
{
    public interface IBorrowingService
    {
        Task<List<Borrowing>> GetAllBorrowingsAsync();
        Task<Borrowing> GetBorrowingByIdAsync(int id);
        Task<Borrowing> AddBorrowingAsync(Borrowing borrowing);
        Task<Borrowing> UpdateBorrowingAsync(int id, Borrowing borrowing);
        Task<bool> DeleteBorrowingAsync(int id);
    }
}
