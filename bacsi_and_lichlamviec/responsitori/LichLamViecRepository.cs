using Microsoft.EntityFrameworkCore;
using WebDichVu.Datas;

namespace WebDichVu.DichVuThuY.Repositories
{
    public class LichLamViecRepository : ILichLamViecRepository
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichLamViecRepository(ThuYcaKoiVetContext context) => _context = context;
        public async Task<List<LichLamViec>> GetAllAsync() =>
        await _context.LichLamViecs.Include(l => l.BacSiThuY).ToListAsync();
        public async Task<LichLamViec?> GetByIdAsync(int id)
        {
            return await _context.LichLamViecs
                .Include(l => l.BacSiThuY)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<LichLamViec>> GetByBacSiIdAsync(int bacSiId) =>
            await _context.LichLamViecs.Where(l => l.BacSiThuYid == bacSiId).ToListAsync();
        public async Task AddAsync(LichLamViec lichLamViec)
        {
            _context.LichLamViecs.Add(lichLamViec);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(LichLamViec lichLamViec)
        {
            try
            {
                var existingLichLamViec = await _context.LichLamViecs.FindAsync(lichLamViec.Id);
                if (existingLichLamViec == null)
                {
                    throw new Exception("Không tìm thấy lịch làm việc");
                }

                existingLichLamViec.BacSiThuYid = lichLamViec.BacSiThuYid;
                existingLichLamViec.Ngay = lichLamViec.Ngay;
                existingLichLamViec.GioBatDau = lichLamViec.GioBatDau;
                existingLichLamViec.GioKetThuc = lichLamViec.GioKetThuc;
                existingLichLamViec.TrangThai = lichLamViec.TrangThai;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(lichLamViec.Id)) throw new Exception("Không tìm thấy lịch làm việc");
                else throw; 
            }
        }
        public async Task DeleteAsync(int id)
        {
            var lichLamViec = await _context.LichLamViecs.FindAsync(id);
            if (lichLamViec != null)
            {
                _context.LichLamViecs.Remove(lichLamViec);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<LichLamViec?> GetByIdWithBacSiAsync(int id)
        {
            return await _context.LichLamViecs
                .Include(l => l.BacSiThuY)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> ExistsAsync(int id) => await _context.LichLamViecs.AnyAsync(e => e.Id == id);

    }
}
