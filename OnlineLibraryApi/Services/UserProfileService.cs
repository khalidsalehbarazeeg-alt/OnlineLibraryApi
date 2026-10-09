using OnlineLibraryApi.Models;

namespace OnlineLibraryApi.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly List<UserProfile> _profiles;
        private int _nextId = 1;

        public UserProfileService()
        {
            // بيانات تجريبية
            _profiles = new List<UserProfile>
            {
                new UserProfile
                {
                    ProfileId = _nextId++,
                    Address = "Riyadh, Saudi Arabia",
                    BirthDate = new DateTime(1990, 1, 1),
                    UserId = 1
                },
                new UserProfile
                {
                    ProfileId = _nextId++,
                    Address = "Jeddah, Saudi Arabia",
                    BirthDate = new DateTime(1992, 5, 15),
                    UserId = 2
                }
            };
        }

        public async Task<IEnumerable<UserProfile>> GetAllProfilesAsync()
        {
            return await Task.FromResult(_profiles.AsEnumerable());
        }

        public async Task<UserProfile?> GetProfileByIdAsync(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileId == id);
            return await Task.FromResult(profile);
        }

        public async Task<UserProfile> CreateProfileAsync(UserProfile profile)
        {
            profile.ProfileId = _nextId++;
            _profiles.Add(profile);

            return await Task.FromResult(profile);
        }

        public async Task<UserProfile?> UpdateProfileAsync(int id, UserProfile updatedProfile)
        {
            var existingProfile = _profiles.FirstOrDefault(p => p.ProfileId == id);
            if (existingProfile == null)
                return null;

            existingProfile.Address = updatedProfile.Address;
            existingProfile.BirthDate = updatedProfile.BirthDate;
            existingProfile.UserId = updatedProfile.UserId;

            return await Task.FromResult(existingProfile);
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.ProfileId == id);
            if (profile == null)
                return false;

            _profiles.Remove(profile);
            return await Task.FromResult(true);
        }
    }
}
