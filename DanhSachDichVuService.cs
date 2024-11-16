using WebDichVu.Datas;
using WebDichVu.DichVuThuY.Repositories;
using WebDichVu.DichVuThuY.Service;

public class DanhSachDichVuService : IDanhSachDichVuService
{
    private readonly IDanhSachDichVuRepository _repository;

    public DanhSachDichVuService(IDanhSachDichVuRepository repository) => _repository = repository;

    public async Task<List<DichVu>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<DichVu?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(DichVu dichVu)
    {
        try
        {
            await _repository.AddAsync(dichVu);
        }
        catch (Exception ex)
        {
            throw new Exception("Có lỗi xảy ra khi thêm dịch vụ: " + ex.Message);
        }
    }

    public async Task UpdateAsync(DichVu dichVu)
    {
        await _repository.UpdateAsync(dichVu);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);
}
