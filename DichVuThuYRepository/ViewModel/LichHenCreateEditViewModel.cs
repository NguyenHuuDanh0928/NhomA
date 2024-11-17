using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DichVuThuYRepository.ViewModel
{
    public class LichHenCreateEditViewModel
    {
        [Required]
        public int? Id { get; set; }  // ID của lịch hẹn (nullable để phân biệt khi tạo mới)

        [Required(ErrorMessage = "Vui lòng chọn Khách Hàng")]
        public int KhachHangId { get; set; }  // ID của khách hàng

        [Required(ErrorMessage = "Vui lòng chọn Bác Sĩ")]
        public int BacSiThuYId { get; set; }  // ID của Bác Sĩ

        [Required(ErrorMessage = "Vui lòng chọn Dịch Vụ")]
        public int DichVuId { get; set; }     // ID của Dịch Vụ

        [Required(ErrorMessage = "Vui lòng chọn thời gian")]
        [DataType(DataType.DateTime)]
        public DateTime ThoiGian { get; set; } // Thời gian đặt hẹn

        [Required(ErrorMessage = "Vui lòng nhập địa điểm")]
        [StringLength(200, ErrorMessage = "Địa điểm không được vượt quá 200 ký tự")]
        public string DiaDiem { get; set; }    // Địa điểm của lịch hẹn

        public string TrangThai { get; set; }  // Trạng thái của lịch hẹn

        // Danh sách các Khách Hàng để hiển thị trong dropdown
        public List<SelectListItem> KhachHangList { get; set; } = new List<SelectListItem>();

        // Danh sách các Bác Sĩ để hiển thị trong dropdown
        public List<SelectListItem> BacSiThuYList { get; set; } = new List<SelectListItem>();

        // Danh sách các Dịch Vụ để hiển thị trong dropdown
        public List<SelectListItem> DichVuList { get; set; } = new List<SelectListItem>();
    }
}
