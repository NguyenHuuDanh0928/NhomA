using DichVuThuYRepository.Interfaces;
using DichVuThuYRepository.ViewModel;
using DichVuThuYService.Datas;
using DichVuThuYService.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DichVuThuYService.Implementations
{
    public class LichHenService : ILichHenService
    {
        private readonly ILichHenRepository _lichHenRepository;

        public LichHenService(ILichHenRepository lichHenRepository)
        {
            _lichHenRepository = lichHenRepository;
        }

        public async Task<List<SelectListItem>> GetBacSiThuYListAsync()
        {
            return await _lichHenRepository.GetBacSiThuYListAsync();
        }

        public async Task<List<SelectListItem>> GetDichVuListAsync()
        {
            return await _lichHenRepository.GetDichVuListAsync();
        }

        public async Task CreateLichHenAsync(LichHenViewModel model)
        {
            var lichHen = new LichHen
            {
                KhachHangId = model.KhachHangId,
                BacSiThuYId = model.BacSiThuYId,
                DichVuId = model.DichVuId,
                ThoiGian = model.ThoiGian,
                DiaDiem = model.DiaDiem,
                TrangThai = "Chưa duyệt"
            };

            await _lichHenRepository.CreateLichHenAsync(lichHen);
        }

        public async Task<List<LichHen>> GetAllLichHenAsync()
        {
            return await _lichHenRepository.GetAllLichHenAsync();
        }

        public async Task<LichHen> GetLichHenByIdAsync(int id)
        {
            return await _lichHenRepository.GetByIdAsync(id);
        }

        public async Task ApproveLichHenAsync(int id)
        {
            var lichHen = await GetLichHenByIdAsync(id);
            if (lichHen != null && lichHen.TrangThai == "Chưa duyệt")
            {
                lichHen.TrangThai = "Đã xác nhận";
                await _lichHenRepository.UpdateAsync(lichHen);
            }
        }

        public Task<List<LichHen>> GetAllLichHenAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task ILichHenService.GetLichHenByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetLichHenByIdAsyc(int id)
        {
            throw new NotImplementedException();
        }
    }
}
