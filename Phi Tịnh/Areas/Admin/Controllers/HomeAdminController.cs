﻿using Microsoft.AspNetCore.Mvc;
using WebDichVu.Datas;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        ThuYcaKoiVetContext db=new ThuYcaKoiVetContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("thongtinkhachhang")]
        public IActionResult ThongTinKhachHang()
        { 

            var lstKhachHang=db.KhachHangs.ToList();
            return View(lstKhachHang);
        }
        [Route("lichsu")]
        public IActionResult LichSu()
        {

            var lstLichSuDatDichVu = db.LichSuDatDichVus.ToList();
            return View(lstLichSuDatDichVu);
        }
        [Route("dashboard")]
        public IActionResult DashBoard() 
        {
            return View();

        }
    }
}
