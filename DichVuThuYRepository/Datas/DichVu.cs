using System;
using System.Collections.Generic;

namespace DichVuThuYService.Datas
{
    public partial class DichVu
    {
        // ID của dịch vụ, đóng vai trò là khóa chính
        public int Id { get; set; }

        // Tên dịch vụ - bắt buộc nhập
        public string TenDichVu { get; set; } = null!; // Đảm bảo thuộc tính không null

        // Mô tả dịch vụ - có thể null
        public string? MoTa { get; set; }

        // Giá dịch vụ - kiểu decimal phù hợp cho tiền tệ
        public decimal Gia { get; set; }

        // Chi phí di chuyển - có thể null
        public decimal? ChiPhiDiChuyen { get; set; }

        // Liên kết với LichHen - để quản lý các lịch hẹn liên quan đến dịch vụ
        public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

        // Liên kết với LichSuDatDichVu - để theo dõi lịch sử đặt dịch vụ
        public virtual ICollection<LichSuDatDichVu> LichSuDatDichVus { get; set; } = new List<LichSuDatDichVu>();

        // Liên kết với PhanHoi - để lấy các phản hồi liên quan đến dịch vụ
        public virtual ICollection<PhanHoi> PhanHois { get; set; } = new List<PhanHoi>();
    }
}
