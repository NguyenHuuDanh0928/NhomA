using WebDichVu.Datas;
using WebDichVu.DichVuThuY.Repositories;
using WebDichVu.DichVuThuY.Service;

public class BacSiThuYService : IBacSiThuYService
{
    private readonly IBacSiThuYRepository _repository;

    public BacSiThuYService(IBacSiThuYRepository repository) => _repository = repository;


    public async Task<List<BacSiThuY>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<BacSiThuY?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<BacSiThuY?> GetByEmailAsync(string email) => await _repository.GetByEmailAsync(email);

    public async Task AddAsync(BacSiThuY bacSi)
    {
        try
        {
            if (await _repository.GetByEmailAsync(bacSi.Email) != null)
            {
                throw new Exception("Email đã tồn tại.");
            }

            await _repository.AddAsync(bacSi);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi trong service AddAsync: " + ex.ToString());

            // Re-throw exception để controller xử lý
            throw;
        }
    }


    public async Task UpdateAsync(BacSiThuY bacSi)
    {
        
        var existingBacSi = await _repository.GetByEmailAsync(bacSi.Email);


        if (existingBacSi != null && existingBacSi.Id != bacSi.Id)
        {
            throw new Exception("Email đã được sử dụng bởi bác sĩ khác.");
        }
        await _repository.UpdateAsync(bacSi);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);

    }


    public bool Exists(int id) => _repository.Exists(id);

}