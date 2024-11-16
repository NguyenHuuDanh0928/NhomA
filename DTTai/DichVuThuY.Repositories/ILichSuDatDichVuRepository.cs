using WebDichVu.Datas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDichVu.DichVuThuY.Repositories
{
    public interface ILichSuDatDichVuRepository
    {
        // Lấy tất cả lịch sử đặt dịch vụ
        Task<List<LichSuDatDichVu>> GetAllAsync();

        // Lấy lịch sử đặt dịch vụ theo ID
        Task<LichSuDatDichVu?> GetByIdAsync(int id);

        // Lấy lịch sử dịch vụ của khách hàng theo ID khách hàng
        Task<List<LichSuDatDichVu>> GetByCustomerIdAsync(int customerId);

        // Thêm một bản ghi lịch sử đặt dịch vụ
        Task AddAsync(LichSuDatDichVu lichSuDatDichVu);

        // Cập nhật một bản ghi lịch sử đặt dịch vụ
        Task UpdateAsync(LichSuDatDichVu lichSuDatDichVu);

        // Xóa một bản ghi lịch sử đặt dịch vụ theo ID
        Task DeleteAsync(int id);

        // Kiểm tra sự tồn tại của một bản ghi lịch sử dịch vụ theo ID
        Task<bool> ExistsAsync(int id);

        // Kiểm tra sự tồn tại của một bản ghi lịch sử dịch vụ
        Task<bool> ExistsAsync(LichSuDatDichVu lichSu);
    }
}
