using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DichVuThuYRepository.ViewModel
{
    public class LichHenViewModel
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public int BacSiThuYId { get; set; }
        public int DichVuId { get; set; }
        public DateTime ThoiGian { get; set; }
        public string DiaDiem { get; set; }

        // Dropdown lists
        public List<SelectListItem> KhachHangList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BacSiThuYList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DichVuList { get; set; } = new List<SelectListItem>();
    }
}
