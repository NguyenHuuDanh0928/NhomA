using WebDichVu.Datas;

namespace WebDichVu.DichVuThuY.Service
{
    public interface IBacSiThuYService
    {
        Task<List<BacSiThuY>> GetAllAsync();
        Task<BacSiThuY?> GetByIdAsync(int id);
        Task<BacSiThuY?> GetByEmailAsync(string email);
        Task AddAsync(BacSiThuY bacSi);
        Task UpdateAsync(BacSiThuY bacSi);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
