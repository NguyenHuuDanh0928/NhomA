using System;
using System.Collections.Generic;

namespace WebDichVu.DichVuThuYRepository.Datas;

public partial class PhanHoi
{
    public int Id { get; set; }

    public int KhachHangId { get; set; }

    public int DichVuId { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual DichVu DichVu { get; set; } = null!;

    public virtual KhachHang KhachHang { get; set; } = null!;
}
