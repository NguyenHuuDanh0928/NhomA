using DichVuThuYRepository.Interfaces;

using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DichVuThuYRepository.Implementations
{
    public class ThongTinLichHenRepository : IThongTinLichHenRepository
    {
        private readonly ThuYcaKoiVetContext _context;

        public ThongTinLichHenRepository(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        public IEnumerable<LichHen> GetLichHenByKhachHangId(int khachHangId)
        {
            return _context.LichHens
                .Include(lh => lh.KhachHang)
                .Include(lh => lh.BacSiThuY)
                .Include(lh => lh.DichVu)
                .Where(lh => lh.KhachHangId == khachHangId)
                .ToList();
        }
    }
}
