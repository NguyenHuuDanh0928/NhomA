using System;
using System.ComponentModel.DataAnnotations;

namespace DichVuThuYRepository.ViewModel
{
    public class AdminLichHenViewModel
    {
        public int Id { get; set; }

        // Thông tin khách hàng
        public int KhachHangId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
        public string TenKhachHang { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SoDienThoaiKhachHang { get; set; }

        public string DiaChiKhachHang { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string EmailKhachHang { get; set; }

        // Thông tin bác sĩ thú y
        public int BacSiThuYId { get; set; }

        [Required(ErrorMessage = "Tên bác sĩ là bắt buộc.")]
        public string TenBacSi { get; set; }

        public string ChuyenMonBacSi { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SoDienThoaiBacSi { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string EmailBacSi { get; set; }

        // Thông tin dịch vụ
        public int DichVuId { get; set; }

        [Required(ErrorMessage = "Tên dịch vụ là bắt buộc.")]
        public string TenDichVu { get; set; }

        public string MoTaDichVu { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá dịch vụ phải lớn hơn hoặc bằng 0.")]
        public decimal GiaDichVu { get; set; }

        // Thông tin lịch hẹn
        [Required(ErrorMessage = "Thời gian là bắt buộc.")]
        public DateTime ThoiGian { get; set; }

        [Required(ErrorMessage = "Địa điểm là bắt buộc.")]
        public string DiaDiem { get; set; }

        public string TrangThai { get; set; } // Trạng thái của lịch hẹn

        // Thêm thuộc tính để duyệt lịch hẹn
        public bool IsApproved { get; set; } // Đánh dấu nếu lịch hẹn đã được duyệt
    }
}
