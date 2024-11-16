using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDichVu.Datas;
using WebDichVu.DichVuThuY.Service;

namespace WebDichVu.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/[controller]")]
	public class DanhSachDichVuController : Controller
	{
		private readonly IDanhSachDichVuService _danhSachDichVuService;

		public DanhSachDichVuController(IDanhSachDichVuService danhSachDichVuService)
		{
			_danhSachDichVuService = danhSachDichVuService;
		}

		// GET: admin/DanhSachDichVu
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var dichVuList = await _danhSachDichVuService.GetAllAsync();
			return View(dichVuList);
		}

		// GET: admin/DanhSachDichVu/Details/5
		[HttpGet("Details/{id?}")]
		public async Task<IActionResult> Details(int? id)
		{
			var dichVu = await _danhSachDichVuService.GetByIdAsync(id.GetValueOrDefault());
			return dichVu == null ? NotFound() : View(dichVu);
		}

		// GET: admin/DanhSachDichVu/Create
		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View();
		}

		// POST: admin/DanhSachDichVu/Create
		[HttpPost("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DichVu dichVu)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _danhSachDichVuService.AddAsync(dichVu);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi thêm mới: " + ex.Message);
				}
			}
			return View(dichVu);
		}

		// GET: admin/DanhSachDichVu/Edit/5
		[HttpGet("Edit/{id?}")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var dichVu = await _danhSachDichVuService.GetByIdAsync(id.Value);
			return dichVu == null ? NotFound() : View(dichVu);
		}

		// POST: admin/DanhSachDichVu/Edit/5
		[HttpPost("Edit/{id}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, DichVu dichVu)
		{
			if (id != dichVu.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _danhSachDichVuService.UpdateAsync(dichVu);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật: " + ex.Message);
				}
			}
			return View(dichVu);
		}

		// GET: admin/DanhSachDichVu/Delete/5
		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var dichVu = await _danhSachDichVuService.GetByIdAsync(id);
			return dichVu == null ? NotFound() : View(dichVu);
		}

		// POST: admin/DanhSachDichVu/Delete/5
		[HttpPost("Delete/{id}")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			try
			{
				await _danhSachDichVuService.DeleteAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "Xóa dịch vụ thất bại: " + ex.Message);
				var dichVu = await _danhSachDichVuService.GetByIdAsync(id);
				return View("Delete", dichVu);
			}
		}

		// Kiểm tra xem dịch vụ có tồn tại không
		private async Task<bool> DichVuExistsAsync(int id)
		{
			return await _danhSachDichVuService.ExistsAsync(id);
		}
	}
}
