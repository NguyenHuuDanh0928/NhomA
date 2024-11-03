using KoiCareService.Models;
using Microsoft.EntityFrameworkCore;

public class KoiCareContext : DbContext
{
    public KoiCareContext(DbContextOptions<KoiCareContext> options) : base(options) { }

    public DbSet<Service> Services { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình quan hệ giữa các bảng
        modelBuilder.Entity<Service>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.Services)
            .HasForeignKey(s => s.CustomerId);

        modelBuilder.Entity<Service>()
            .HasOne(s => s.Doctor)
            .WithMany(d => d.Services)
            .HasForeignKey(s => s.DoctorId);
    }
}
