using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DichVuThuY.Service;
using WebDichVu.DichVuThuY.Services;
using BCrypt.Net;
using DichVuThuYService.Datas;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebDichVu.DichVuThuYRepository.Datas;


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
        public async Task<IActionResult> Index(string keyword)
        {
            ViewData["Keyword"] = keyword;

            var khachHangList = string.IsNullOrEmpty(keyword)
                ? await _khachHangService.GetAllAsync() // Nếu không có từ khóa, lấy tất cả khách hàng
                : await _khachHangService.SearchAsync(keyword); // Tìm kiếm theo từ khóa

            return View(khachHangList);
        }



        [HttpGet("Create")]

        public IActionResult Create() => View();

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DichVuThuYService.Datas.KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                await _khachHangService.AddAsync(khachHang);
                TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: admin/KhachHang/Edit/5
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
        // POST: admin/KhachHang/Edit/{id}
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVuThuYService.Datas.KhachHang khachHang)
        {
            if (id != khachHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _khachHangService.UpdateAsync(khachHang);
                    // Chỉ định rõ Area khi chuyển hướng
                    return RedirectToAction("Index", "KhachHang", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }
            return View(khachHang);
        }

        // GET: admin/KhachHang/Delete/5
        [HttpGet("Delete/{id}")]
            public async Task<IActionResult> Delete(int id)
            {

                var khachHang = await _khachHangService.GetByIdAsync(id);
                return khachHang == null ? NotFound() : View(khachHang);

            }

            // POST: admin/KhachHang/Delete/5
            [HttpPost("Delete/{id}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    await _khachHangService.DeleteAsync(id);
                    TempData["SuccessMessage"] = "Xóa Khách hàng thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Xóa khách hàng thất bại" + ex.Message;
                    return RedirectToAction(nameof(Index));
                }
            }

            [HttpGet("Details/{id?}")]
            public async Task<IActionResult> Details(int? id)
            {
                var khachHang = await _khachHangService.GetByIdAsync(id.GetValueOrDefault());
                return khachHang == null ? NotFound() : View(khachHang);
            }


        }
    } 
