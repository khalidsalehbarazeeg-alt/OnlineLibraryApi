using OnlineLibraryApi.Models;

namespace OnlineLibraryApi.Services
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfile>> GetAllProfilesAsync();
        Task<UserProfile?> GetProfileByIdAsync(int id);
        Task<UserProfile> CreateProfileAsync(UserProfile profile);
        Task<UserProfile?> UpdateProfileAsync(int id, UserProfile profile);
        Task<bool> DeleteProfileAsync(int id);
    }
}
