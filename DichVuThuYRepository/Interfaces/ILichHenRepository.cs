using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Interfaces
{
    public interface ILichHenRepository
    {
        Task<List<SelectListItem>> GetBacSiThuYListAsync();   // Lấy danh sách bác sĩ thú y
        Task<List<SelectListItem>> GetDichVuListAsync();      // Lấy danh sách dịch vụ
        Task CreateLichHenAsync(LichHen lichHen);             // Tạo lịch hẹn
        Task ApproveLichHenAsync(int id);                     // Duyệt lịch hẹn

        // Khai báo phương thức GetAllLichHenAsync
        Task<LichHen> GetLichHenByIdAsync(int id); // Lấy lịch hẹn theo ID
        Task UpdateLichHenAsync(LichHen lichHen);  // Cập nhật lịch hẹn
        Task<LichHen> GetByIdAsync(int id);
        Task<List<LichHen>> GetAllLichHenAsync(int id);             // Lấy danh sách tất cả lịch hẹn
        Task<List<LichHen>> GetAllLichHenAsync();
       
        Task UpdateAsync(object lichHen);
    }
}
