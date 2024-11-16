using WebDichVu.Datas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDichVu.DichVuThuY.Repositories
{
    public class LichSuDatDichVuRepository : ILichSuDatDichVuRepository
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichSuDatDichVuRepository(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // Lấy tất cả lịch sử đặt dịch vụ
        public async Task<List<LichSuDatDichVu>> GetAllAsync()
        {
            return await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .ToListAsync();
        }

        // Lấy lịch sử đặt dịch vụ theo ID
        public async Task<LichSuDatDichVu?> GetByIdAsync(int id)
        {
            return await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        // Lấy lịch sử dịch vụ của khách hàng theo ID khách hàng
        public async Task<List<LichSuDatDichVu>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .Where(l => l.KhachHangId == customerId)
                .ToListAsync();
        }

        // Thêm một bản ghi lịch sử đặt dịch vụ
        public async Task AddAsync(LichSuDatDichVu lichSuDatDichVu)
        {
            await _context.LichSuDatDichVus.AddAsync(lichSuDatDichVu);
            await _context.SaveChangesAsync();
        }

        // Cập nhật một bản ghi lịch sử đặt dịch vụ
        public async Task UpdateAsync(LichSuDatDichVu lichSuDatDichVu)
        {
            _context.LichSuDatDichVus.Update(lichSuDatDichVu);
            await _context.SaveChangesAsync();
        }

        // Xóa một bản ghi lịch sử đặt dịch vụ theo ID
        public async Task DeleteAsync(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVus.FindAsync(id);
            if (lichSuDatDichVu != null)
            {
                _context.LichSuDatDichVus.Remove(lichSuDatDichVu);
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra sự tồn tại của một bản ghi lịch sử dịch vụ theo ID
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.LichSuDatDichVus.AnyAsync(e => e.Id == id);
        }

        // Kiểm tra sự tồn tại của một bản ghi lịch sử dịch vụ
        public async Task<bool> ExistsAsync(LichSuDatDichVu lichSu)
        {
            return await _context.LichSuDatDichVus.AnyAsync(e =>
                e.KhachHangId == lichSu.KhachHangId &&
                e.DichVuId == lichSu.DichVuId &&
                e.ThoiGian == lichSu.ThoiGian);
        }
    }
}
