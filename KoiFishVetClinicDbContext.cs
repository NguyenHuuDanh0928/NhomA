using KoiFishVetClinicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFishVetClinicAPI.Data
{
     public class KoiFishVetClinicDbContext : DbContext
     {
          public KoiFishVetClinicDbContext(DbContextOptions<KoiFishVetClinicDbContext> options)
              : base(options)
          {
          }

          public DbSet<KhachHang> KhachHang { get; set; }
          public DbSet<BacSiThuY> BacSiThuY { get; set; }
          public DbSet<DichVu> DichVu { get; set; }
          public DbSet<PhanHoi> PhanHoi { get; set; } 
          public DbSet<LichSuDatDichVu> LichSuDatDichVu { get; set; }
          public DbSet<LichHen> LichHen { get; set;}
     }
}     
              
