using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Implementations
{
    public class LichHenRepository : ILichHenRepository
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichHenRepository(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        public async Task<List<SelectListItem>> GetBacSiThuYListAsync()
        {
            return await _context.BacSiThuYs
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.TenBacSi
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetDichVuListAsync()
        {
            return await _context.DichVus
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.TenDichVu
                }).ToListAsync();
        }

        public async Task CreateLichHenAsync(LichHen lichHen)
        {
            await _context.LichHens.AddAsync(lichHen);
            await _context.SaveChangesAsync();
        }

        // Thêm chức năng duyệt lịch hẹn
        public async Task ApproveLichHenAsync(int id)
        {
            var lichHen = await _context.LichHens.FindAsync(id);

            if (lichHen != null && lichHen.TrangThai == "Chưa duyệt")
            {
                lichHen.TrangThai = "Đã xác nhận";
                await _context.SaveChangesAsync();
            }
        }

        // Thêm phương thức GetAllLichHenAsync
        public async Task<List<LichHen>> GetAllLichHenAsync()
        {
            // Lấy tất cả lịch hẹn từ cơ sở dữ liệu
            return await _context.LichHens
                .Include(l => l.KhachHang) // Nếu có bảng liên quan, sử dụng Include
                .Include(l => l.BacSiThuY)
                .Include(l => l.DichVu)
                .ToListAsync();
        }

        public Task<List<LichHen>> GetAllLichHenAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<LichHen> GetLichHenByIdAsync(int id)
        {
            return await _context.LichHens.FindAsync(id);
        }

        public async Task UpdateLichHenAsync(LichHen lichHen)
        {
            _context.LichHens.Update(lichHen);
            await _context.SaveChangesAsync();
        }
        public async Task<LichHen> GetByIdAsync(int id)
        {
            return await _context.LichHens.FindAsync(id);
        }

        public Task UpdateAsync(object lichHen)
        {
            throw new NotImplementedException();
        }
    }
}
