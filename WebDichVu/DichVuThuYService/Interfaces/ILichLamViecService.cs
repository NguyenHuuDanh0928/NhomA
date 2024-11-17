using DichVuThuYService.Datas;


namespace WebDichVu.DichVuThuY.Service
{
    public interface ILichLamViecService
    {
        Task<List<LichLamViec>> GetAllAsync();
        Task<LichLamViec?> GetByIdAsync(int id);
        Task<List<LichLamViec>> GetByBacSiIdAsync(int bacSiId);
        Task AddAsync(LichLamViec lichLamViec);
        Task UpdateAsync(LichLamViec lichLamViec);
        Task<bool> ExistsAsync(int id);
        Task<LichLamViec?> GetByIdWithBacSiAsync(int id);
        Task DeleteAsync(int id);
    }
}
