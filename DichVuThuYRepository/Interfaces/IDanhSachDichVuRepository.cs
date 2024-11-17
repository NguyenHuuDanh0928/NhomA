using DichVuThuYService.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Interfaces
{
    public interface IDanhSachDichVuRepository
    {
        Task<List<DichVu>> GetAllAsync();
        Task<DichVu?> GetByIdAsync(int id);
        Task AddAsync(DichVu dichVu);
        Task UpdateAsync(DichVu dichVu);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

    }
}
