using System;
using System.ComponentModel.DataAnnotations;

namespace DichVuThuYRepository.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Display(Name = "Mật khẩu")]
        public string Matkhau { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được bỏ trống")]
        [Display(Name = "Tên Khách Hàng")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ghi Chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateOnly NgaySinh { get; set; }  // Sửa DateOnly thành DateTime

        public enum GioiTinhEnum
        {
            Nam,
            Nu
        }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        [Display(Name = "Giới Tính")]
        public GioiTinhEnum GioiTinh { get; set; }
    }
}
