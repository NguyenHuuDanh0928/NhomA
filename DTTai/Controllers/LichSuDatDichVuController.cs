using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebDichVu.Datas;
using WebDichVu.DichVuThuY.Service;
using PagedList;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class LichSuDatDichVuController : Controller
    {
        private readonly ILichSuDatDichVuService _lichSuDatDichVuService;

        // Constructor nhận vào ILichSuDatDichVuService
        public LichSuDatDichVuController(ILichSuDatDichVuService lichSuDatDichVuService)
        {
            _lichSuDatDichVuService = lichSuDatDichVuService;
        }

        // GET: Admin/LichSuDatDichVu
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            var lichSuDatDichVus = await _lichSuDatDichVuService.GetAllAsync();

            // Tìm kiếm theo tên khách hàng hoặc tên dịch vụ nếu có
            if (!string.IsNullOrEmpty(searchString))
            {
                lichSuDatDichVus = lichSuDatDichVus.Where(l =>
                    l.KhachHang.TenKhachHang.Contains(searchString) ||
                    l.DichVu.TenDichVu.Contains(searchString)).ToList();
            }

            // Định nghĩa số lượng bản ghi trên mỗi trang
            int pageSize = 10;

            // Phân trang kết quả
            var lichSuDatDichVuPaged = lichSuDatDichVus.ToPagedList(page, pageSize);

            return View(lichSuDatDichVuPaged);
        }

        // GET: Admin/LichSuDatDichVu/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuDatDichVu = await _lichSuDatDichVuService.GetByIdAsync(id.Value);

            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return View(lichSuDatDichVu);
        }

        // GET: Admin/LichSuDatDichVu/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            // Các dữ liệu dùng trong dropdown cho Create View
            ViewData["DichVuId"] = new SelectList(await _lichSuDatDichVuService.GetAllDichVuAsync(), "Id", "TenDichVu");
            ViewData["KhachHangId"] = new SelectList(await _lichSuDatDichVuService.GetAllKhachHangAsync(), "Id", "TenKhachHang");

            return View();
        }

        // POST: Admin/LichSuDatDichVu/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachHangId,DichVuId,ThoiGian,TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Thêm lịch sử dịch vụ mới
                    await _lichSuDatDichVuService.AddAsync(lichSuDatDichVu);
                    TempData["SuccessMessage"] = "Lịch sử đặt dịch vụ đã được tạo thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                }
            }

            // Nếu có lỗi, trả về các dữ liệu cần thiết cho View
            ViewData["DichVuId"] = new SelectList(await _lichSuDatDichVuService.GetAllDichVuAsync(), "Id", "TenDichVu", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(await _lichSuDatDichVuService.GetAllKhachHangAsync(), "Id", "TenKhachHang", lichSuDatDichVu.KhachHangId);

            return View(lichSuDatDichVu);
        }

        // GET: Admin/LichSuDatDichVu/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy thông tin lịch sử đặt dịch vụ bằng cách gọi bất đồng bộ
            var lichSuDatDichVu = await _lichSuDatDichVuService.GetByIdAsync(id.Value);

            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            // Lấy dữ liệu cho các dropdown
            ViewData["DichVuId"] = new SelectList(await _lichSuDatDichVuService.GetAllDichVuAsync(), "Id", "TenDichVu", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(await _lichSuDatDichVuService.GetAllKhachHangAsync(), "Id", "TenKhachHang", lichSuDatDichVu.KhachHangId);

            return View(lichSuDatDichVu);
        }

        // POST: Admin/LichSuDatDichVu/Edit/5
        [HttpPost("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KhachHangId,DichVuId,ThoiGian,TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            if (id != lichSuDatDichVu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật thông tin lịch sử đặt dịch vụ
                    await _lichSuDatDichVuService.UpdateAsync(lichSuDatDichVu);
                    TempData["SuccessMessage"] = "Cập nhật lịch sử đặt dịch vụ thành công!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật: " + ex.Message;
                    return View(lichSuDatDichVu);
                }

                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi, trả lại dữ liệu cho view
            ViewData["DichVuId"] = new SelectList(await _lichSuDatDichVuService.GetAllDichVuAsync(), "Id", "TenDichVu", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(await _lichSuDatDichVuService.GetAllKhachHangAsync(), "Id", "TenKhachHang", lichSuDatDichVu.KhachHangId);

            return View(lichSuDatDichVu);
        }

        // GET: Admin/LichSuDatDichVu/Delete/5
        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuDatDichVu = await _lichSuDatDichVuService.GetByIdAsync(id.Value);

            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return View(lichSuDatDichVu);
        }

        // POST: Admin/LichSuDatDichVu/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _lichSuDatDichVuService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Đã xóa lịch sử đặt dịch vụ thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
