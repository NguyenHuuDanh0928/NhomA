using DichVuThuYService.Datas;
using BCrypt.Net;

namespace WebDichVu.DichVuThuY.Services
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllAsync();
        Task<KhachHang?> GetByIdAsync(int id);
        Task<KhachHang?> GetByEmailAsync(string email);
        Task AddAsync(KhachHang khachHang);
        Task UpdateAsync(KhachHang khachHang);
        Task DeleteAsync(int id);
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<KhachHang>> SearchAsync(string keyword);
    }
}
