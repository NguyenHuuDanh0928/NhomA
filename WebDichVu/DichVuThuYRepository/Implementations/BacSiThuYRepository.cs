using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DichVuThuYService.Datas;

namespace WebDichVu.DichVuThuY.Repositories
{
    public class BacSiThuYRepository : IBacSiThuYRepository
    {
        private readonly ThuYcaKoiVetContext _context;
        private readonly ILogger<BacSiThuYRepository> _logger; // Declare the logger

        public BacSiThuYRepository(ThuYcaKoiVetContext context, ILogger<BacSiThuYRepository> logger) // Inject ILogger
        {
            _context = context;
            _logger = logger; // Assign the injected logger
        }
        public BacSiThuYRepository(ThuYcaKoiVetContext context) => _context = context;


        public async Task<List<BacSiThuY>> GetAllAsync() => await _context.BacSiThuYs.ToListAsync();
        public async Task<BacSiThuY?> GetByIdAsync(int id) => await _context.BacSiThuYs.FindAsync(id);


        public async Task<BacSiThuY?> GetByEmailAsync(string email) => // Sửa lại phương thức này
             await _context.BacSiThuYs.FirstOrDefaultAsync(b => b.Email == email);


        public async Task AddAsync(BacSiThuY bacSi)
        {
            try
            {
                _context.BacSiThuYs.Add(bacSi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log lỗi chi tiết hơn, bao gồm cả InnerException
                _logger.LogError(dbEx, "Lỗi cập nhật CSDL khi thêm BacSi: {InnerExceptionMessage}", dbEx.InnerException?.Message);

                //  Kiểm tra xem có phải lỗi do trùng lặp khóa Unique hay không.
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    // Nếu là lỗi trùng lặp khóa, ném ra exception cụ thể hơn
                    throw new Exception("Email hoặc số điện thoại đã tồn tại.", dbEx); // Exception rõ ràng hơn

                }

                throw; // Re-throw exception gốc sau khi log
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm BacSi");
                throw; // Re-throw exception gốc sau khi log
            }
        }





        public async Task UpdateAsync(BacSiThuY bacSi)
        {
            try
            {
                var existingBacSi = await _context.BacSiThuYs.FindAsync(bacSi.Id);
                if (existingBacSi == null)
                {
                    throw new Exception("Không tìm thấy bác sĩ");
                }

                existingBacSi.TenBacSi = bacSi.TenBacSi;
                existingBacSi.ChuyenMon = bacSi.ChuyenMon;
                existingBacSi.SoDienThoai = bacSi.SoDienThoai;
                existingBacSi.Email = bacSi.Email;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(bacSi.Id)) throw new Exception("Không tìm thấy bác sĩ");
                else throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var bacSi = await _context.BacSiThuYs.FindAsync(id);
                if (bacSi == null)
                {
                    throw new ArgumentException($"Bác sĩ có ID {id} không tồn tại.", nameof(id)); // Ném exception nếu không tìm thấy
                }

                _context.BacSiThuYs.Remove(bacSi); // Di chuyển dòng này RA KHỎI khối if
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new Exception("Bản ghi đã bị thay đổi hoặc xóa bởi người dùng khác.", ex);

            }
            catch (DbUpdateException ex)
            {

                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";

                throw new Exception("Lỗi cơ sở dữ liệu khi xóa bác sĩ: " + innerExceptionMessage, ex);


            }


            catch (Exception ex)
            {

                throw; // Re-throw
            }
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.BacSiThuYs.AnyAsync(e => e.Id == id);
        }


        public bool Exists(int id)
        {
            return _context.BacSiThuYs.Any(b => b.Id == id);
        }

    }
}
