using System;
using System.Collections.Generic;

namespace WebDichVu.DichVuThuYRepository.Datas;

public partial class LichLamViec
{
    public int Id { get; set; }

    public int BacSiThuYid { get; set; }

    public DateOnly Ngay { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public string? TrangThai { get; set; }

    public virtual BacSiThuY BacSiThuY { get; set; } = null!;
}
