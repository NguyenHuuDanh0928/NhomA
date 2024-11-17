using DichVuThuYRepository.Interfaces;
using DichVuThuYService.Datas;
using DichVuThuYRepository.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DichVuThuYRepository.Implementations
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ThuYcaKoiVetContext _context;

        public ProfileRepository(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        public UserProfile GetUserProfileById(int userId)
        {
            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.Id == userId);

            if (khachHang == null) return null;

            return new UserProfile
            {
                Id = khachHang.Id,
                Matkhau = khachHang.Matkhau,
                TenKhachHang = khachHang.TenKhachHang,
                SoDienThoai = khachHang.SoDienThoai,
                DiaChi = khachHang.DiaChi,
                Email = khachHang.Email,
                GhiChu = khachHang.GhiChu,
                NgaySinh = khachHang.NgaySinh,
                GioiTinh = khachHang.GioiTinh ? UserProfile.GioiTinhEnum.Nam : UserProfile.GioiTinhEnum.Nu
            };
        }

        public async Task<bool> UpdateUserProfile(UserProfile updatedProfile)
        {
            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.Id == updatedProfile.Id);

            if (khachHang == null) return false;

            khachHang.TenKhachHang = updatedProfile.TenKhachHang;
            khachHang.SoDienThoai = updatedProfile.SoDienThoai;
            khachHang.DiaChi = updatedProfile.DiaChi;
            khachHang.Email = updatedProfile.Email;
            khachHang.GhiChu = updatedProfile.GhiChu;
            khachHang.NgaySinh = updatedProfile.NgaySinh;
            khachHang.GioiTinh = updatedProfile.GioiTinh == UserProfile.GioiTinhEnum.Nam;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
