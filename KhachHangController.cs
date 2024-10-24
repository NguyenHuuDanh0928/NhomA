using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebDichVu.DuLieu;
using WebDichVu.Helpers;
using WebDichVu.ViewModel;

namespace WebDichVu.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly PetShopDbContext db;
        private readonly IMapper _mapper;

        public KhachHangController(PetShopDbContext context, IMapper mapper)

        {
            db = context;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var khachHang = _mapper.Map<KhachHang>(model);
                khachHang.RandomKey = Util.GenerateRamdomKey();
                khachHang.Matkhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);

            }
            return View();
        }
        #region Login
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(LoginVM model, string? ReturnUrl)
        {

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


        #endregion
    }
}
