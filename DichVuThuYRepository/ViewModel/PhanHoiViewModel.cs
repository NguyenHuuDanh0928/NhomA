using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DichVuThuYRepository.ViewModel
{
    public class PhanHoiViewModel
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public int DichVuId { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }

        // Danh sách khách hàng cho dropdown
        public List<SelectListItem> KhachHangList { get; set; } = new List<SelectListItem>();

        // Danh sách dịch vụ cho dropdown
        public List<SelectListItem> DichVuList { get; set; } = new List<SelectListItem>();
    }
}
