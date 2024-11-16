using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebDichVu.Datas
{
    public class LichSuDatDichVu
    {
        public int Id { get; set; }

        public int KhachHangId { get; set; }
        public virtual KhachHang KhachHang { get; set; }

        public int DichVuId { get; set; }
        public virtual DichVu DichVu { get; set; }

        [Required]
        public DateTime ThoiGian { get; set; }

        [Required]
        [StringLength(100)]
        public string TrangThai { get; set; }  // Trạng thái của dịch vụ
    }
}
