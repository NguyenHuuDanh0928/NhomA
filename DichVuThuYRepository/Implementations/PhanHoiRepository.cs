using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PhanHoiRepository : IPhanHoiRepository
{
    private readonly ThuYcaKoiVetContext _context;

    public PhanHoiRepository(ThuYcaKoiVetContext context)
    {
        _context = context;
    }

    public async Task<List<PhanHoi>> GetAllPhanHoiAsync()
    {
        return await _context.PhanHois.ToListAsync();
    }

    public async Task AddPhanHoiAsync(PhanHoi phanHoi)
    {
        await _context.PhanHois.AddAsync(phanHoi);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DichVu>> GetAllDichVuAsync()
    {
        return await _context.DichVus.ToListAsync();
    }

    public Task<List<KhachHang>> GetAllKhachHangAsync()
    {
        throw new NotImplementedException();
    }
}
