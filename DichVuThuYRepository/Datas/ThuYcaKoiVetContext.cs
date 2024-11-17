using System;
using Microsoft.EntityFrameworkCore;
using DichVuThuYService.Models;

namespace DichVuThuYService.Datas
{
    public partial class ThuYcaKoiVetContext : DbContext
    {
        public ThuYcaKoiVetContext()
        {
        }

        public ThuYcaKoiVetContext(DbContextOptions<ThuYcaKoiVetContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for database tables
        public DbSet<BacSiThuY> BacSiThuYs { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LichHen> LichHens { get; set; }
        public DbSet<LichLamViec> LichLamViecs { get; set; }
        public DbSet<LichSuDatDichVu> LichSuDatDichVus { get; set; }
        public DbSet<PhanHoi> PhanHois { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP\\SQLEXPRESS;Initial Catalog=ThuYCaKoiVet;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSiThuY>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("BacSiThuY");

                entity.Property(e => e.TenBacSi).HasMaxLength(100);
                entity.Property(e => e.ChuyenMon).HasMaxLength(100);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DichVu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("DichVu");

                entity.Property(e => e.TenDichVu).HasMaxLength(100);
                entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ChiPhiDiChuyen).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("KhachHang");

                entity.HasIndex(e => e.SoDienThoai).IsUnique();

                entity.Property(e => e.TenKhachHang).HasMaxLength(100);
                entity.Property(e => e.Matkhau).HasMaxLength(100);
                entity.Property(e => e.DiaChi).HasMaxLength(200);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LichHen>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("LichHen");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime2");
                entity.Property(e => e.DiaDiem).HasMaxLength(200);
                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichHen_KhachHang");

                entity.HasOne(d => d.BacSiThuY)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.BacSiThuYId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichHen_BacSiThuY");

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.DichVuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichHen_DichVu");
            });

            modelBuilder.Entity<LichLamViec>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("LichLamViec");

                entity.Property(e => e.Ngay).HasColumnType("date");
                entity.Property(e => e.GioBatDau).HasColumnType("time");
                entity.Property(e => e.GioKetThuc).HasColumnType("time");
                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.BacSiThuY)
                    .WithMany(p => p.LichLamViecs)
                    .HasForeignKey(d => d.BacSiThuYId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichLamViec_BacSiThuY");
            });

            modelBuilder.Entity<LichSuDatDichVu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("LichSuDatDichVu");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime2").HasDefaultValueSql("(getdate())");
                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.LichSuDatDichVus)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichSuDatDichVu_KhachHang");

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.LichSuDatDichVus)
                    .HasForeignKey(d => d.DichVuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LichSuDatDichVu_DichVu");
            });

            modelBuilder.Entity<PhanHoi>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("PhanHoi");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime2").HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.PhanHois)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanHoi_KhachHang");

                entity.HasOne(d => d.DichVu)
                    .WithMany(p => p.PhanHois)
                    .HasForeignKey(d => d.DichVuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhanHoi_DichVu");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}