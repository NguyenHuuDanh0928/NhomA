using System;
using System.Collections.Generic;

namespace DichVuThuYService.Datas;

public partial class BacSiThuY
{
    public int Id { get; set; }

    public string TenBacSi { get; set; } = null!;

    public string? ChuyenMon { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<LichLamViec> LichLamViecs { get; set; } = new List<LichLamViec>();
}
