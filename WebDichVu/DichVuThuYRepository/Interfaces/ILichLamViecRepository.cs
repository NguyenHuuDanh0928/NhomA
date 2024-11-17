using DichVuThuYService.Datas;

namespace WebDichVu.DichVuThuY.Repositories
{
    public interface ILichLamViecRepository
    {
        Task<List<LichLamViec>> GetAllAsync();
        Task<LichLamViec?> GetByIdAsync(int id);
        Task<List<LichLamViec>> GetByBacSiIdAsync(int bacSiId);
        Task AddAsync(LichLamViec lichLamViec);
        Task UpdateAsync(LichLamViec lichLamViec);
        Task DeleteAsync(int id);
        Task<LichLamViec?> GetByIdWithBacSiAsync(int id);
        Task<bool> ExistsAsync(int id);

    }
}
