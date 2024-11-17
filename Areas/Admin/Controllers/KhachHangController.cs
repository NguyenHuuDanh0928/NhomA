using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DichVuThuY.Service;
using WebDichVu.DichVuThuY.Services;
using BCrypt.Net;
using DichVuThuYService.Datas;


namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class KhachHangController : Controller
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index(string keyword, string gender, DateTime? startDate, DateTime? endDate)
        {
            var khachHangList = await _khachHangService.GetAllAsync();

            if (!string.IsNullOrEmpty(keyword))
                khachHangList = khachHangList.Where(kh => kh.TenKhachHang.Contains(keyword) || kh.SoDienThoai.Contains(keyword)).ToList();

            if (!string.IsNullOrEmpty(gender))
            {
                bool genderBool = gender.Equals("true", StringComparison.OrdinalIgnoreCase);
                khachHangList = khachHangList.Where(kh => kh.GioiTinh == genderBool).ToList();
            }

            if (startDate.HasValue)
                khachHangList = khachHangList.Where(kh => kh.NgaySinh >= DateOnly.FromDateTime(startDate.Value)).ToList();

            if (endDate.HasValue)
                khachHangList = khachHangList.Where(kh => kh.NgaySinh <= DateOnly.FromDateTime(endDate.Value)).ToList();


            return View(khachHangList);
        }

        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                await _khachHangService.AddAsync(khachHang);
                TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }




        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _khachHangService.GetByIdAsync(id.Value);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }


        [HttpPost("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenKhachHang,Matkhau,SoDienThoai,DiaChi,Email,GhiChu,NgaySinh,GioiTinh")] KhachHang khachHang, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            if (id != khachHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentCustomer = await _khachHangService.GetByIdAsync(id);
                if (currentCustomer == null)
                {
                    return NotFound();
                }

                // Validate mật khẩu cũ
                if (!BCrypt.Net.BCrypt.Verify(OldPassword, currentCustomer.Matkhau))
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng.");
                    return View(khachHang);
                }

                // Xác nhận mật khẩu mới
                if (NewPassword != ConfirmPassword)
                {
                    ModelState.AddModelError("NewPassword", "Mật khẩu mới và xác nhận mật khẩu không khớp.");
                    return View(khachHang);
                }

                // Hash mật khẩu mới nếu có
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    currentCustomer.Matkhau = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                }

                try
                {
                    currentCustomer.TenKhachHang = khachHang.TenKhachHang;
                    currentCustomer.SoDienThoai = khachHang.SoDienThoai;
                    currentCustomer.DiaChi = khachHang.DiaChi;
                    currentCustomer.Email = khachHang.Email;
                    currentCustomer.GhiChu = khachHang.GhiChu;
                    currentCustomer.NgaySinh = khachHang.NgaySinh;
                    currentCustomer.GioiTinh = khachHang.GioiTinh;

                    await _khachHangService.UpdateAsync(currentCustomer);
                    TempData["SuccessMessage"] = "Cập nhật thông tin khách hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _khachHangService.ExistsAsync(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(khachHang);
        }


        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _khachHangService.GetByIdAsync(id.Value);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }


        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _khachHangService.GetByIdAsync(id);
            if (khachHang != null)
            {
                await _khachHangService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa khách hàng thành công!";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _khachHangService.GetByIdAsync(id.Value);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

    }
}
