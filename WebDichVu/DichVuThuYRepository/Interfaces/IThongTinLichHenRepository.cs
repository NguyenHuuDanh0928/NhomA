using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using System.Collections.Generic;

namespace DichVuThuYRepository.Interfaces
{
    public interface IThongTinLichHenRepository
    {
        IEnumerable<LichHen> GetLichHenByKhachHangId(int khachHangId);
    }
}
