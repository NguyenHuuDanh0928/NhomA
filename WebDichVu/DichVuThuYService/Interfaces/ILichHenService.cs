using DichVuThuYRepository.ViewModel;
using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DichVuThuYService.Interfaces
{
    public interface ILichHenService
    {
        Task<List<SelectListItem>> GetBacSiThuYListAsync();    // Lấy danh sách bác sĩ thú y
        Task<List<SelectListItem>> GetDichVuListAsync();       // Lấy danh sách dịch vụ
        Task CreateLichHenAsync(LichHenViewModel model);       // Tạo lịch hẹn mới

        // Thêm phương thức lấy danh sách lịch hẹn
        Task ApproveLichHenAsync(int id); // Thêm phương thức để phê duyệt lịch hẹn

        Task<List<LichHen>> GetAllLichHenAsync();
        Task<List<LichHen>> GetAllLichHenAsync(int id);              // Lấy danh sách tất cả lịch hẹn
        Task GetLichHenByIdAsync(int id);
        Task GetLichHenByIdAsyc(int id);
        // Duyệt lịch hẹn theo ID

    }
}
