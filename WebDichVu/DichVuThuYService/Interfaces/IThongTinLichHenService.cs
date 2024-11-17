using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using System.Collections.Generic;

namespace DichVuThuYService.Interfaces
{
    public interface IThongTinLichHenService
    {
        IEnumerable<LichHen> GetLichHenByKhachHangId(int khachHangId);
    }
}
