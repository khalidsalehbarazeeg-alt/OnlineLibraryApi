using Microsoft.AspNetCore.Mvc;
using OnlineLibraryApi.Models;
using OnlineLibraryApi.Services;

namespace OnlineLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profiles = await _userProfileService.GetAllProfilesAsync();
            return Ok(profiles);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _userProfileService.GetProfileByIdAsync(id);
            if (profile == null)
                return NotFound("Profile not found");

            return Ok(profile);
        }

        // CREATE PROFILE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserProfile profile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newProfile = await _userProfileService.CreateProfileAsync(profile);
            return CreatedAtAction(nameof(Get), new { id = newProfile.ProfileId }, newProfile);
        }

        // UPDATE PROFILE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserProfile updatedProfile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProfile = await _userProfileService.GetProfileByIdAsync(id);
            if (existingProfile == null)
                return NotFound("Profile not found");

            // Update fields
            existingProfile.Address = updatedProfile.Address;
            existingProfile.BirthDate = updatedProfile.BirthDate;
            existingProfile.UserId = updatedProfile.UserId;

            var result = await _userProfileService.UpdateProfileAsync(id, existingProfile);
            return Ok(result);
        }

        // DELETE PROFILE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userProfileService.DeleteProfileAsync(id);
            if (!deleted)
                return NotFound("Profile not found");

            return NoContent();
        }
    }
}
