using DichVuThuYService.Interfaces;
using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PhanHoiService : IPhanHoiService
{
    private readonly IPhanHoiRepository _phanHoiRepository;

    public PhanHoiService(IPhanHoiRepository phanHoiRepository)
    {
        _phanHoiRepository = phanHoiRepository;
    }

    public async Task<List<PhanHoi>> GetAllPhanHoiAsync()
    {
        return await _phanHoiRepository.GetAllPhanHoiAsync();
    }

    public async Task AddPhanHoiAsync(PhanHoi phanHoi)
    {
        await _phanHoiRepository.AddPhanHoiAsync(phanHoi);
    }

    public async Task<List<SelectListItem>> GetDichVuListAsync()
    {
        var dichVuList = await _phanHoiRepository.GetAllDichVuAsync();
        return dichVuList.Select(dv => new SelectListItem
        {
            Value = dv.Id.ToString(),
            Text = dv.TenDichVu
        }).ToList();
    }

    public Task<List<SelectListItem>> GetKhachHangListAsync()
    {
        throw new NotImplementedException();
    }
}
