using DichVuThuYRepository.Models;
using System.Threading.Tasks;

namespace DichVuThuYService.Interfaces
{
    public interface IProfileService
    {
        UserProfile GetUserProfile(int userId);
        Task<bool> UpdateUserProfile(UserProfile updatedProfile);
    }
}
