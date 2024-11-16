using WebDichVu.Datas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDichVu.DichVuThuY.Service
{
    public interface ILichSuDatDichVuService
    {
        Task<List<LichSuDatDichVu>> GetAllAsync();
        Task<LichSuDatDichVu?> GetByIdAsync(int id);
        Task AddAsync(LichSuDatDichVu lichSu);
        Task UpdateAsync(LichSuDatDichVu lichSu);
        Task DeleteAsync(int id);
        bool Exists(int id);

        // Thêm phương thức lấy danh sách dịch vụ và khách hàng
        Task<List<DichVu>> GetAllDichVuAsync();  // Lấy danh sách dịch vụ
        Task<List<KhachHang>> GetAllKhachHangAsync();  // Lấy danh sách khách hàng
    }

}
