using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Implementations
{

    
        public class DanhSachDichVuRepository : IDanhSachDichVuRepository
        {
            private readonly ThuYcaKoiVetContext _context;
            private readonly ILogger<DanhSachDichVuRepository> _logger;

            public DanhSachDichVuRepository(ThuYcaKoiVetContext context, ILogger<DanhSachDichVuRepository> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<DichVu>> GetAllAsync() => await _context.DichVus.ToListAsync();

            public async Task<DichVu?> GetByIdAsync(int id) => await _context.DichVus.FindAsync(id);

            public async Task AddAsync(DichVu dichVu)
            {
                try
                {
                    _context.DichVus.Add(dichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    var innerExceptionMessage = dbEx.InnerException?.Message ?? "Không có lỗi nội bộ";
                    _logger.LogError(dbEx, "Lỗi cập nhật cơ sở dữ liệu khi thêm Dịch Vụ: {InnerException}", innerExceptionMessage);
                    throw new Exception("Có lỗi xảy ra khi thêm dịch vụ: " + innerExceptionMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Lỗi khi thêm Dịch Vụ");
                    throw new Exception("Có lỗi xảy ra khi thêm dịch vụ: " + ex.Message);
                }
            }

            public async Task UpdateAsync(DichVu dichVu)
            {
                try
                {
                    var existingDichVu = await _context.DichVus.FindAsync(dichVu.Id);
                    if (existingDichVu == null)
                    {
                        throw new Exception("Không tìm thấy dịch vụ");
                    }

                    existingDichVu.TenDichVu = dichVu.TenDichVu;
                    existingDichVu.MoTa = dichVu.MoTa;
                    existingDichVu.Gia = dichVu.Gia;
                    existingDichVu.ChiPhiDiChuyen = dichVu.ChiPhiDiChuyen;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExistsAsync(dichVu.Id)) throw new Exception("Không tìm thấy dịch vụ");
                    else throw;
                }
            }

            public async Task DeleteAsync(int id)
            {
                var dichVu = await _context.DichVus.FindAsync(id);
                if (dichVu != null)
                {
                    _context.DichVus.Remove(dichVu);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<bool> ExistsAsync(int id)
            {
                return await _context.DichVus.AnyAsync(e => e.Id == id);
            }

        }
    }
