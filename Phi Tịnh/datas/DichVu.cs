using System;
using System.Collections.Generic;

namespace WebDichVu.Datas;

public partial class DichVu
{
    public int Id { get; set; }

    public string TenDichVu { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal Gia { get; set; }

    public decimal? ChiPhiDiChuyen { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<LichSuDatDichVu> LichSuDatDichVus { get; set; } = new List<LichSuDatDichVu>();

    public virtual ICollection<PhanHoi> PhanHois { get; set; } = new List<PhanHoi>();
}
