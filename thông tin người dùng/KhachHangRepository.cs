using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using DichVuThuYService.Datas;
using Microsoft.Extensions.Logging;


namespace WebDichVu.DichVuThuY.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly ThuYcaKoiVetContext _context;
        private readonly ILogger<KhachHangRepository> _logger; // Declare the logger
        public KhachHangRepository(ThuYcaKoiVetContext context, ILogger<KhachHangRepository> logger) // Inject ILogger
        {
            _context = context;
            _logger = logger; // Assign the injected logger
        }
        public KhachHangRepository(ThuYcaKoiVetContext context) => _context = context;


        public async Task<List<KhachHang>> GetAllAsync() => await _context.KhachHangs.ToListAsync();
        public async Task<List<KhachHang>> SearchAsync(string keyword)
        {
            return await _context.KhachHangs
                .Where(kh => kh.TenKhachHang.Contains(keyword) || kh.SoDienThoai.Contains(keyword)) // Tìm theo tên hoặc số điện thoại
                .ToListAsync();
        }

        public async Task<KhachHang?> GetByIdAsync(int id) => await _context.KhachHangs.FindAsync(id);
        public async Task<KhachHang?> GetByEmailAsync(string email) => // Sửa lại phương thức này
            await _context.KhachHangs.FirstOrDefaultAsync(b => b.Email == email);
        public async Task AddAsync(KhachHang khachHang)
        {
            try
            {
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log lỗi chi tiết hơn, bao gồm cả InnerException
                _logger.LogError(dbEx, "Lỗi cập nhật CSDL khi thêm KhachHang: {InnerExceptionMessage}", dbEx.InnerException?.Message);

                //  Kiểm tra xem có phải lỗi do trùng lặp khóa Unique hay không.
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    // Nếu là lỗi trùng lặp khóa, ném ra exception cụ thể hơn
                    throw new Exception("Email hoặc số điện thoại đã tồn tại.", dbEx); // Exception rõ ràng hơn

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm KhachHang");
                throw; // Re-throw exception gốc sau khi log
            }
        }

        public async Task UpdateAsync(KhachHang khachHang)
        {
            try
            {
                var existingKhachHang = await _context.KhachHangs.FindAsync(khachHang.Id);
                if (existingKhachHang == null)
                {
                    throw new Exception("Không tìm thấy khách hàng");
                }
                existingKhachHang.TenKhachHang = khachHang.TenKhachHang;
                existingKhachHang.DiaChi = khachHang.DiaChi;
                existingKhachHang.SoDienThoai = khachHang.SoDienThoai;
                existingKhachHang.Email = khachHang.Email;
                existingKhachHang.NgaySinh = khachHang.NgaySinh;
                existingKhachHang.GioiTinh = khachHang.GioiTinh;
                existingKhachHang.GhiChu = khachHang.GhiChu;
                existingKhachHang.Matkhau = khachHang.Matkhau;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(khachHang.Id)) throw new Exception("Không tìm thấy khách hàng");
                else throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var khachHang = await _context.KhachHangs.FindAsync(id);
                if (khachHang == null)
                {
                    throw new ArgumentException($"Khách Hàng có ID {id} không tồn tại.", nameof(id)); // Ném exception nếu không tìm thấy
                }
                _context.KhachHangs.Remove(khachHang); // Di chuyển dòng này RA KHỎI khối if
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Bản ghi đã bị thay đổi hoặc xóa bởi người dùng khác.", ex);
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";

                throw new Exception("Lỗi cơ sở dữ liệu khi xóa khách hàng: " + innerExceptionMessage, ex);
            }
            catch (Exception ex)
            {
                throw; // Re-throw
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.KhachHangs.AnyAsync(e => e.Id == id);
        }
        public bool Exists(int id)
        {
            return _context.KhachHangs.Any(b => b.Id == id);
        }
    }
}
