using WebDichVu.Datas;
using WebDichVu.DichVuThuY.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDichVu.DichVuThuY.Service
{
    public class LichSuDatDichVuService : ILichSuDatDichVuService
    {
        private readonly ILichSuDatDichVuRepository _repository;

        public LichSuDatDichVuService(ILichSuDatDichVuRepository repository) => _repository = repository;

        // Lấy tất cả lịch sử đặt dịch vụ
        public async Task<List<LichSuDatDichVu>> GetAllAsync() => await _repository.GetAllAsync();

        // Lấy lịch sử đặt dịch vụ theo ID
        public async Task<LichSuDatDichVu?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        // Thêm lịch sử đặt dịch vụ mới
        public async Task AddAsync(LichSuDatDichVu lichSu)
        {
            try
            {
                if (await _repository.ExistsAsync(lichSu))
                {
                    throw new Exception("Dịch vụ đã được đặt trước đó.");
                }
                await _repository.AddAsync(lichSu);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm lịch sử đặt dịch vụ: " + ex.Message);
            }
        }

        // Cập nhật lịch sử đặt dịch vụ
        public async Task UpdateAsync(LichSuDatDichVu lichSu)
        {
            try
            {
                var existingLichSu = await _repository.GetByIdAsync(lichSu.Id);
                if (existingLichSu == null)
                {
                    throw new Exception("Không tìm thấy lịch sử đặt dịch vụ cần cập nhật.");
                }
                await _repository.UpdateAsync(lichSu);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật lịch sử đặt dịch vụ: " + ex.Message);
            }
        }

        // Xóa lịch sử đặt dịch vụ
        public async Task DeleteAsync(int id)
        {
            try
            {
                var existingLichSu = await _repository.GetByIdAsync(id);
                if (existingLichSu == null)
                {
                    throw new Exception("Không tìm thấy lịch sử đặt dịch vụ cần xóa.");
                }
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa lịch sử đặt dịch vụ: " + ex.Message);
            }
        }

        // Kiểm tra xem lịch sử đặt dịch vụ có tồn tại không
        public bool Exists(int id) => _repository.Exists(id);
    }
}
