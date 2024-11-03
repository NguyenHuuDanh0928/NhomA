using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class KoiCareContextFactory : IDesignTimeDbContextFactory<KoiCareContext>
{
    public KoiCareContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KoiCareContext>();

        // Đọc cấu hình từ appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Cấu hình DbContext để sử dụng SQL Server với chuỗi kết nối
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("KoiCareDB"));

        return new KoiCareContext(optionsBuilder.Options);
    }
}
