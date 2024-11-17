using System.ComponentModel.DataAnnotations;

namespace DichVuThuYRepository.ViewModel
{
    public class RegisterVm
    {
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "*")]
        public required string MaKH { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Mật Khẩu")]
        public required string MatKhau { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Họ Tên")]
        public required string HoTen { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Địa Chỉ")]
        public required string DiaChi { get; set; }

        [Required(ErrorMessage = "Chưa đúng định dạng")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Chưa đúng định dạng số điện thoại")]
        [Display(Name = "Số Điện Thoại")]
        public required string SoDienThoai { get; set; }

        [Display(Name = "Ghi Chú")]
        [Required(ErrorMessage = "Vui lòng chọn loại khách hàng")]
        public LoaiKhachHangEnum GhiChu { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Ngày Sinh")]
        public DateOnly NgaySinh { get; set; }


        public enum GioiTinhEnum
        {
            Nam,
            Nu
        }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [Display(Name = "Giới Tính")]
        public GioiTinhEnum GioiTinh { get; set; }

        // Thêm enum LoaiKhachHangEnum cho GhiChu
        public enum LoaiKhachHangEnum
        {
            KhachHangMoi,
            KhachhangCu,
            KhachHangDacBiet
        }
    }
}
