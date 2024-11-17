using DichVuThuYRepository.Models;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Interfaces
{
    public interface IProfileRepository
    {
        UserProfile GetUserProfileById(int userId);
        Task<bool> UpdateUserProfile(UserProfile updatedProfile);
    }
}
