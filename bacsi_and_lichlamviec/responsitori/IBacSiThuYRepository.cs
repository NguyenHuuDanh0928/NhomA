using WebDichVu.Datas;

namespace WebDichVu.DichVuThuY.Repositories
{
    public interface IBacSiThuYRepository
    {
        Task<List<BacSiThuY>> GetAllAsync();
        Task<BacSiThuY?> GetByIdAsync(int id);
        Task<BacSiThuY?> GetByEmailAsync(string email); 
        Task AddAsync(BacSiThuY bacSi);
        Task UpdateAsync(BacSiThuY bacSi);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        bool Exists(int id);
    }

}
