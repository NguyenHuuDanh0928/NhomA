using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DichVuThuYService.Datas;
using DichVuThuYRepository.ViewModel;

namespace DichVuThuYService.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ThuYcaKoiVetContext db;
        private readonly IMapper _mapper;

        public KhachHangController(ThuYcaKoiVetContext context, IMapper mapper)
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
        public async Task<IActionResult> DangKy(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var existingKhachHang = db.KhachHangs.SingleOrDefault(kh => kh.Email == model.Email);
                if (existingKhachHang != null)
                {
                    ModelState.AddModelError("", "Email đã tồn tại.");
                    return View(model);
                }

                var khachHang = _mapper.Map<KhachHang>(model);

                db.KhachHangs.Add(khachHang);
                await db.SaveChangesAsync();
            }
            return View(model);
        }

        #region Login
        [HttpGet]
        public IActionResult DangNhap(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var khachHang = db.KhachHangs
                    .AsEnumerable()
                    .SingleOrDefault(kh => kh.Email == model.Email && kh.Matkhau == model.Matkhau);

                if (khachHang != null)
                {
                    // Lưu KhachHangId vào Session
                    HttpContext.Session.SetInt32("KhachHangId", khachHang.Id);

                    // Tạo Claims cho Authentication
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachHang.Email),
                        new Claim(ClaimTypes.NameIdentifier, khachHang.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Chuyển hướng đến ReturnUrl sau khi đăng nhập thành công
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Profile", "KhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không chính xác.");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        #endregion

        public IActionResult Profile()
        {
            return View();
        }
    }
}
