using DichVuThuYService.Datas;
using DichVuThuYRepository.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public interface IPhanHoiService
{
    Task<List<PhanHoi>> GetAllPhanHoiAsync();
    Task AddPhanHoiAsync(PhanHoi phanHoi);
    Task<List<SelectListItem>> GetKhachHangListAsync();
    Task<List<SelectListItem>> GetDichVuListAsync();
}
