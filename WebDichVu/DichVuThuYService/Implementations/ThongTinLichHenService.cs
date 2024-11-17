using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using DichVuThuYService.Interfaces;
using System.Collections.Generic;

namespace DichVuThuYService.Implementations
{
    public class ThongTinLichHenService : IThongTinLichHenService
    {
        private readonly IThongTinLichHenRepository _repository;

        public ThongTinLichHenService(IThongTinLichHenRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LichHen> GetLichHenByKhachHangId(int khachHangId)
        {
            return _repository.GetLichHenByKhachHangId(khachHangId);
        }
    }
}
