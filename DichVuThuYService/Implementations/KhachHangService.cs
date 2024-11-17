using DichVuThuYService.Datas;
using WebDichVu.DichVuThuY.Repositories;

namespace WebDichVu.DichVuThuY.Services
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepository _repository;

        public KhachHangService(IKhachHangRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<KhachHang>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<KhachHang?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<KhachHang?> GetByEmailAsync(string email) => await _repository.GetByEmailAsync(email);
        public async Task AddAsync(KhachHang khachHang) { if (await EmailExistsAsync(khachHang.Email)) throw new InvalidOperationException("Email đã tồn tại."); await _repository.AddAsync(khachHang); }
        public async Task UpdateAsync(KhachHang khachHang) { if (!await ExistsAsync(khachHang.Id)) throw new KeyNotFoundException("Khách hàng không tồn tại."); await _repository.UpdateAsync(khachHang); }
        public async Task DeleteAsync(int id) { if (!await ExistsAsync(id)) throw new KeyNotFoundException("Khách hàng không tồn tại."); await _repository.DeleteAsync(id); }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);
        public async Task<bool> EmailExistsAsync(string email) => await _repository.EmailExistsAsync(email);
    }
}
