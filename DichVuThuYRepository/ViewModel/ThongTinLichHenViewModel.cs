using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichVuThuYRepository.ViewModel
{
    public class ThongTinLichHenViewModel
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string TenBacSi { get; set; }
        public string TenDichVu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string DiaDiem { get; set; }
        public string TrangThai { get; set; }
    }
}
