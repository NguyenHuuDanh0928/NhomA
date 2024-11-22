using DichVuThuYService.Datas;
using WebDichVu.DichVuThuY.Repositories;
using DichVuThuYRepository.Interfaces;
using WebDichVu.DichVuThuY.Service;
using WebDichVu.DichVuThuY.Services;

public class KhachHangService : IKhachHangService
{
    private readonly IKhachHangRepository _repository;

    public KhachHangService(IKhachHangRepository repository) => _repository = repository;

    public async Task<List<KhachHang>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<KhachHang?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<KhachHang?> GetByEmailAsync(string email) => await _repository.GetByEmailAsync(email);

    public async Task AddAsync(KhachHang khachHang)
    {
        try
        {
            if (await _repository.GetByEmailAsync(khachHang.Email) != null)
            {
                throw new Exception("Email đã tồn tại.");
            }
            await _repository.AddAsync(khachHang);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi trong service AddAsync: " + ex.ToString());
            throw;
        }
    }

    public async Task UpdateAsync(KhachHang khachHang)
    {
        var existingKhachHang = await _repository.GetByEmailAsync(khachHang.Email);

        if (existingKhachHang != null && existingKhachHang.Id != khachHang.Id)
        {
            throw new Exception("Email đã được sử dụng bởi bác sĩ khác.");
        }
        await _repository.UpdateAsync(khachHang);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public bool Exists(int id)
    {
        var khachHang = _repository.GetByIdAsync(id).Result;
        return khachHang != null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var khachHang = await _repository.GetByIdAsync(id);
        return khachHang != null;
    }

    public async Task<List<KhachHang>> SearchAsync(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return await _repository.GetAllAsync();
        }

        var khachHangList = await _repository.GetAllAsync(); // Chờ kết quả từ Task
        return khachHangList
            .Where(kh => kh.TenKhachHang.Contains(keyword) || kh.SoDienThoai.Contains(keyword))
            .ToList(); // Chuyển kết quả thành danh sách
    }

}

