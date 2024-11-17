using DichVuThuYService.Datas;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPhanHoiRepository
{
    Task<List<PhanHoi>> GetAllPhanHoiAsync(); // Lấy danh sách phản hồi
    Task AddPhanHoiAsync(PhanHoi phanHoi);   // Thêm phản hồi mới
    Task<List<KhachHang>> GetAllKhachHangAsync(); // Lấy danh sách khách hàng
    Task<List<DichVu>> GetAllDichVuAsync();      // Lấy danh sách dịch vụ
}
