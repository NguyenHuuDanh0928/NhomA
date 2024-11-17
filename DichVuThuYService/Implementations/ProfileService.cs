using DichVuThuYRepository.Interfaces;
using DichVuThuYRepository.Models;
using DichVuThuYService.Interfaces;
using System.Threading.Tasks;

namespace DichVuThuYService.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public UserProfile GetUserProfile(int userId)
        {
            return _profileRepository.GetUserProfileById(userId);
        }

        public async Task<bool> UpdateUserProfile(UserProfile updatedProfile)
        {
            return await _profileRepository.UpdateUserProfile(updatedProfile);
        }
    }
}
