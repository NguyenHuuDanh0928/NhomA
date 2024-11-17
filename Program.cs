using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using DichVuThuYService.Datas;
using DichVuThuYService.Helpers;
using DichVuThuYService.Implementations;
using DichVuThuYService.Interfaces;
using DichVuThuYRepository.Implementations;
using DichVuThuYRepository.Interfaces;
using DichVuThuYRepository;
using DichVuThuY.Service;
using WebDichVu.DichVuThuY.Repositories;
using WebDichVu.DichVuThuY.Service;
using WebDichVu.DichVuThuY.Services;

var builder = WebApplication.CreateBuilder(args);

// Đọc chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ThuYCaKoiVetDb");

// Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<ThuYcaKoiVetContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký dịch vụ cho AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Đăng ký dịch vụ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của Session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký Authentication với Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/KhachHang/DangNhap";      // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/KhachHang/DangXuat";     // Đường dẫn đến trang đăng xuất
        options.AccessDeniedPath = "/KhachHang/AccessDenied"; // Đường dẫn khi truy cập bị từ chối
    });

// Đăng ký các Service và Repository trong DI container
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ILichHenRepository, LichHenRepository>();
builder.Services.AddScoped<ILichHenService, LichHenService>();
builder.Services.AddScoped<IPhanHoiRepository, PhanHoiRepository>();
builder.Services.AddScoped<IPhanHoiService, PhanHoiService>();
builder.Services.AddScoped<IThongTinLichHenRepository, ThongTinLichHenRepository>();
builder.Services.AddScoped<IThongTinLichHenService, ThongTinLichHenService>();
builder.Services.AddScoped<IDanhSachDichVuRepository, DanhSachDichVuRepository>();
builder.Services.AddScoped<IDanhSachDichVuService, DanhSachDichVuService>();
builder.Services.AddScoped<IBacSiThuYRepository, BacSiThuYRepository>();
builder.Services.AddScoped<ILichLamViecRepository, LichLamViecRepository>();
builder.Services.AddScoped<ILichLamViecService, LichLamViecService>();
builder.Services.AddScoped<IBacSiThuYService, BacSiThuYService>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();

// Đăng ký MVC và Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Thêm Razor Pages nếu bạn sử dụng

var app = builder.Build();

// Cấu hình pipeline cho HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Kích hoạt HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Tự động chuyển hướng HTTP sang HTTPS
app.UseStaticFiles(); // Kích hoạt phục vụ các tệp tĩnh (CSS, JS, hình ảnh)

app.UseRouting(); // Kích hoạt định tuyến cho ứng dụng

app.UseSession(); // Kích hoạt Session (đặt trước middleware tùy chỉnh)

// Middleware xác thực và cấp quyền
app.UseAuthentication(); // Xác thực người dùng
app.UseAuthorization();  // Cấp quyền truy cập cho các yêu cầu

// Cấu hình định tuyến cho các Areas và Razor Pages
app.UseEndpoints(endpoints =>
{
    // Định tuyến cho các Areas
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Định tuyến cho /ThongTinLichHen/Index
    endpoints.MapControllerRoute(
        name: "thongtinlichhen",
        pattern: "ThongTinLichHen/{action=Index}/{id?}",
        defaults: new { controller = "ThongTinLichHen", action = "Index" });

    // Các định tuyến tùy chỉnh
    endpoints.MapControllerRoute(
        name: "danhgia",
        pattern: "DanhGia/{action=DanhGia}/{id?}",
        defaults: new { controller = "DanhGia" });

    endpoints.MapControllerRoute(
        name: "datlichhen",
        pattern: "LichHen/{action=DatLichHen}/{id?}",
        defaults: new { controller = "LichHen" });

    // Định tuyến cho ProfileController
    endpoints.MapControllerRoute(
        name: "profile",
        pattern: "Profile/{action=Index}/{id?}",
        defaults: new { controller = "Profile", action = "Index" });


    endpoints.MapControllerRoute(
        name: "khachhang",
        pattern: "KhachHang/{action=Index}/{id?}",
        defaults: new { controller = "KhachHang" });

    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{controller=HomeAdmin}/{action=Index}/{id?}",
        defaults: new { area = "Admin" });

    // Định tuyến mặc định cho các controller
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    // Định tuyến cho PhanHoiController
    endpoints.MapControllerRoute(
        name: "phanhoi",
        pattern: "PhanHoi/{action=Index}/{id?}",
        defaults: new { controller = "PhanHoi", action = "Index" });

    // Định tuyến cho AdminPhanHoiController
    endpoints.MapControllerRoute(
        name: "adminphanhoi",
        pattern: "Admin/PhanHoi/{action=Index}/{id?}",
        defaults: new { area = "Admin", controller = "AdminPhanHoi" });




    // Định tuyến cho Razor Pages
    endpoints.MapRazorPages();
});

// Endpoint tĩnh cho giới thiệu (nếu cần)
app.MapGet("/gioi-thieu", async context =>
{
    await context.Response.SendFileAsync("wwwroot/about.html");
});

app.Run(); // Chạy ứng dụng
