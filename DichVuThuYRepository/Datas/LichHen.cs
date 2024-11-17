using System;
using System.Collections.Generic;

namespace DichVuThuYService.Datas;

public partial class LichHen
{
    public int Id { get; set; }

    public int KhachHangId { get; set; }
    

    public int BacSiThuYId { get; set; }
   


    public int DichVuId { get; set; }

    public DateTime ThoiGian { get; set; }

    public string? DiaDiem { get; set; }

    public string? TrangThai { get; set; }


    public virtual BacSiThuY BacSiThuY { get; set; } = null!;

    public virtual DichVu DichVu { get; set; } = null!;

    public virtual KhachHang KhachHang { get; set; } = null!;
}
