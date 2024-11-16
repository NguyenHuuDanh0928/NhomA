using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebDichVu.DichVuThuYRepository.Datas;

public partial class ThuYcaKoiVetContext : DbContext
{
    public ThuYcaKoiVetContext()
    {
    }

    public ThuYcaKoiVetContext(DbContextOptions<ThuYcaKoiVetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BacSiThuY> BacSiThuYs { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LichHen> LichHens { get; set; }

    public virtual DbSet<LichLamViec> LichLamViecs { get; set; }

    public virtual DbSet<LichSuDatDichVu> LichSuDatDichVus { get; set; }

    public virtual DbSet<PhanHoi> PhanHois { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ACER\\SQLEXPRESS;Initial Catalog=ThuYCaKoiVet;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BacSiThuY>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BacSiThu__3214EC07DF6620D0");

            entity.ToTable("BacSiThuY");

            entity.Property(e => e.ChuyenMon).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenBacSi).HasMaxLength(100);
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DichVu__3214EC07CBC1D976");

            entity.ToTable("DichVu");

            entity.Property(e => e.ChiPhiDiChuyen).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenDichVu).HasMaxLength(100);
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KhachHan__3214EC073AD01EA2");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.SoDienThoai, "UQ__KhachHan__0389B7BDB3219C2E").IsUnique();

            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Matkhau).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
        });

        modelBuilder.Entity<LichHen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichHen__3214EC07E87F5B40");

            entity.ToTable("LichHen");

            entity.Property(e => e.BacSiThuYid).HasColumnName("BacSiThuYId");
            entity.Property(e => e.DiaDiem).HasMaxLength(200);
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.BacSiThuY).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.BacSiThuYid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHen__BacSiTh__48CFD27E");

            entity.HasOne(d => d.DichVu).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.DichVuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHen__DichVuI__49C3F6B7");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.KhachHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHen__KhachHa__47DBAE45");
        });

        modelBuilder.Entity<LichLamViec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichLamV__3214EC0711871AF5");

            entity.ToTable("LichLamViec");

            entity.Property(e => e.BacSiThuYid).HasColumnName("BacSiThuYId");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.BacSiThuY).WithMany(p => p.LichLamViecs)
                .HasForeignKey(d => d.BacSiThuYid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichLamVi__BacSi__4CA06362");
        });

        modelBuilder.Entity<LichSuDatDichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LichSuDa__3214EC0705437F7A");

            entity.ToTable("LichSuDatDichVu");

            entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.DichVu).WithMany(p => p.LichSuDatDichVus)
                .HasForeignKey(d => d.DichVuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuDat__DichV__44FF419A");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.LichSuDatDichVus)
                .HasForeignKey(d => d.KhachHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuDat__Khach__440B1D61");
        });

        modelBuilder.Entity<PhanHoi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhanHoi__3214EC07D1862060");

            entity.ToTable("PhanHoi");

            entity.Property(e => e.ThoiGian).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DichVu).WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.DichVuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanHoi__DichVuI__403A8C7D");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.KhachHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanHoi__KhachHa__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
