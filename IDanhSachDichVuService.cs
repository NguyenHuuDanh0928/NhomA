﻿using WebDichVu.Datas;

namespace WebDichVu.DichVuThuY.Service
{
    public interface IDanhSachDichVuService
    {
        Task<List<DichVu>> GetAllAsync();
        Task<DichVu?> GetByIdAsync(int id);
        Task AddAsync(DichVu dichVu);
        Task UpdateAsync(DichVu dichVu);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
