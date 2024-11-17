using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DichVuThuY.Repositories;

namespace WebDichVu.DichVuThuY.Service
{
    public class LichLamViecService : ILichLamViecService
    {
        private readonly ILichLamViecRepository _repository;
        private readonly IBacSiThuYRepository _bacSiRepository; // Inject IBacSiThuYRepository

        public LichLamViecService(ILichLamViecRepository repository, IBacSiThuYRepository bacSiRepository)
        {
            _repository = repository;
            _bacSiRepository = bacSiRepository;
        }


        public async Task<List<LichLamViec>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<LichLamViec?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task<List<LichLamViec>> GetByBacSiIdAsync(int bacSiId) => await _repository.GetByBacSiIdAsync(bacSiId);

        public async Task AddAsync(LichLamViec lichLamViec)
        {
            try
            {

                await _repository.AddAsync(lichLamViec);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm Lịch làm việc: " + ex.Message);
            }
        }

        public async Task UpdateAsync(LichLamViec lichLamViec)
        {
            if (!await _bacSiRepository.ExistsAsync(id: lichLamViec.BacSiThuYid))
            {
                throw new ArgumentException("Bác sĩ không tồn tại.");
            }

            try
            {
                await _repository.UpdateAsync(lichLamViec);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật lịch làm việc: " + ex.Message);
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<LichLamViec?> GetByIdWithBacSiAsync(int id)
        {
            return await _repository.GetByIdWithBacSiAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
