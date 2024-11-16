using WebDichVu.Datas;

namespace WebDichVu.DichVuThuY.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDbContext _context;

        public KhachHangRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhachHang>> GetAllAsync() => await _context.KhachHangs.ToListAsync();
        public async Task<KhachHang?> GetByIdAsync(int id) => await _context.KhachHangs.FindAsync(id);
        public async Task<KhachHang?> GetByEmailAsync(string email) => await _context.KhachHangs.FirstOrDefaultAsync(kh => kh.Email == email);
        public async Task AddAsync(KhachHang khachHang) { await _context.KhachHangs.AddAsync(khachHang); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(KhachHang khachHang) { _context.KhachHangs.Update(khachHang); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var kh = await GetByIdAsync(id); if (kh != null) _context.KhachHangs.Remove(kh); await _context.SaveChangesAsync(); }
        public async Task<bool> ExistsAsync(int id) => await _context.KhachHangs.AnyAsync(kh => kh.Id == id);
        public async Task<bool> EmailExistsAsync(string email) => await _context.KhachHangs.AnyAsync(kh => kh.Email == email);
    }
}
