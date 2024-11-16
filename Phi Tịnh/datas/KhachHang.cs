using System;
using System.Collections.Generic;

namespace WebDichVu.Datas;

public partial class KhachHang
{
    public int Id { get; set; }

    public string Matkhau { get; set; } = null!;

    public string TenKhachHang { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? GhiChu { get; set; }

    public DateOnly NgaySinh { get; set; }

    public bool GioiTinh { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<LichSuDatDichVu> LichSuDatDichVus { get; set; } = new List<LichSuDatDichVu>();

    public virtual ICollection<PhanHoi> PhanHois { get; set; } = new List<PhanHoi>();
}
