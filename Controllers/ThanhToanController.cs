using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class ThanhToanController : Controller
    {
        CSDLContext db = new CSDLContext();
        // GET: ThanhToan
        public ActionResult TimDH()
        {

            return View();
        }
        public ActionResult Index()
        {
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            return View(giohang);
        }
        public ActionResult HoaDon(string hoten1, string sdt1, string diachi1, string khuyenmai1, int Tong1)
        {
            ViewBag.ht = hoten1;
            ViewBag.sd = sdt1;
            ViewBag.dc = diachi1;
            ViewBag.km = khuyenmai1;
            ViewBag.Tong = Tong1;
            string sdt = Request.Form["sdt"];
            string hoten = Request.Form["hoten"];
            string diachi = Request.Form["diachi"];
            string khuyenmai = Request.Form["khuyenmai"];
            List<GioHang> cart = (List<GioHang>)Session["giohang"];
            //if (Session["giohang"] == null)
            //{
            //    ViewBag.nullCart = "Giỏ hàng trống...!";
            //    return View();
            //}
            //GioHang giohang = Session["giohang"] as GioHang;
            //if (cart.Items.Count() == 0)
            //{
            //    ViewBag.nullCart = "Giỏ hàng trống...!";
            //    return View();
            //}
            return View();
        }

        [HttpPost]
        public ActionResult StepEnd(int Tong)
        {
            

            string sdt = Request.Form["sdt"];
            string hoten = Request.Form["hoten"];
            string email = Request.Form["email"];
            string diachi = Request.Form["diachi"];
            string khuyenmai = Request.Form["khuyenmai"];
            
            List<GioHang> cart = (List<GioHang>)Session["giohang"];

            if (User.Identity.IsAuthenticated)
            {
                User user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                DonHang donHang = new DonHang();
                donHang.SDT = sdt;
                donHang.NgayDat = DateTime.Now;
                donHang.DiaChi = diachi;
                donHang.GiaVanChuyen = 20000;
                donHang.KhuyenMai = "";
                donHang.TongTien = Tong;
                donHang.IDUser = user.IDUser;
                donHang.TinhTrangDonHang = "Đang duyệt";
                donHang.LoaiTT = "Ship code";
                
                db.DonHangs.Add(donHang);
                db.SaveChanges();

                if (donHang.User.Ranking ==1)
                {
                    foreach (var item in cart)
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                        {
                            SoLuong = item.SoLuong,
                            TongTien = Tong,
                            MaSach = item.MaSach,
                            MaDH = donHang.MaDH,
                        };
                        db.ChiTietDonHangs.Add(chiTietDonHang);
                        db.SaveChanges();

                        var sach = db.Saches.FirstOrDefault(x => x.MaSach == item.MaSach);
                        sach.SoLuong = sach.SoLuong - item.SoLuong;
                        db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    var dtl = db.Users.FirstOrDefault(x => x.IDUser == donHang.User.IDUser);
                    dtl.DiemTichLuy = dtl.DiemTichLuy + Tong;
                    db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if(donHang.User.Ranking == 2)
                {
                    donHang.TongTien = Tong * 95 / 100;
                    Tong = Tong * 95 / 100;
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    foreach (var item in cart)
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                        {
                            SoLuong = item.SoLuong,
                            TongTien = Tong * 95 / 100,
                            MaSach = item.MaSach,
                            MaDH = donHang.MaDH,
                        };
                        db.ChiTietDonHangs.Add(chiTietDonHang);
                        db.SaveChanges();

                        var sach = db.Saches.FirstOrDefault(x => x.MaSach == item.MaSach);
                        sach.SoLuong = sach.SoLuong - item.SoLuong;
                        db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    var dtl = db.Users.FirstOrDefault(x => x.IDUser == donHang.User.IDUser);
                    dtl.DiemTichLuy = dtl.DiemTichLuy + Tong;
                    db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (donHang.User.Ranking == 3)
                {
                    
                    donHang.TongTien = Tong * 90 / 100;
                   
                    Tong = Tong * 90 / 100;
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    foreach (var item in cart)
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                        {
                            SoLuong = item.SoLuong,
                            TongTien = Tong * 90 / 100,
                            MaSach = item.MaSach,
                            MaDH = donHang.MaDH,
                        };
                        db.ChiTietDonHangs.Add(chiTietDonHang);
                        db.SaveChanges();

                        var sach = db.Saches.FirstOrDefault(x => x.MaSach == item.MaSach);
                        sach.SoLuong = sach.SoLuong - item.SoLuong;
                        db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    var dtl = db.Users.FirstOrDefault(x => x.IDUser == donHang.User.IDUser);
                    dtl.DiemTichLuy = dtl.DiemTichLuy + Tong;
                    db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (donHang.User.Ranking == 4)
                {
                    
                    donHang.TongTien = Tong * 85 / 100;
                    
                    Tong = Tong * 85 / 100;
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    foreach (var item in cart)
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                        {
                            SoLuong = item.SoLuong,
                            TongTien = Tong * 85 / 100,
                            MaSach = item.MaSach,
                            MaDH = donHang.MaDH,
                        };
                        db.ChiTietDonHangs.Add(chiTietDonHang);
                        db.SaveChanges();

                        var sach = db.Saches.FirstOrDefault(x => x.MaSach == item.MaSach);
                        sach.SoLuong = sach.SoLuong - item.SoLuong;
                        db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    var dtl = db.Users.FirstOrDefault(x => x.IDUser == donHang.User.IDUser);
                    dtl.DiemTichLuy = dtl.DiemTichLuy + Tong;
                    db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                DonHang donHang = new DonHang();
                donHang.SDT = sdt;
                donHang.NgayDat = DateTime.Now;
                donHang.DiaChi = diachi;
                donHang.GiaVanChuyen = 20000;
                donHang.KhuyenMai = "";
                donHang.TongTien = Tong;
                donHang.TinhTrangDonHang = "Đang duyệt";
                donHang.LoaiTT = "Ship code";
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                foreach (var item in cart)
                {
                    ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                    {
                        SoLuong = item.SoLuong,
                        TongTien = Tong,
                        MaSach = item.MaSach,
                        MaDH = donHang.MaDH,
                    };
                    db.ChiTietDonHangs.Add(chiTietDonHang);
                    db.SaveChanges();

                    var sach = db.Saches.FirstOrDefault(x => x.MaSach == item.MaSach);
                    sach.SoLuong = sach.SoLuong - item.SoLuong;
                    db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            cart.Clear();
            return RedirectToAction("HoaDon", "ThanhToan", new { hoten1 = hoten, sdt1 = sdt, diachi1 = diachi, khuyenmai1 = khuyenmai, Tong1 = Tong }) ;
            
          

        }
        public ActionResult DonHang(string sdt)
        {
            if (User.Identity.IsAuthenticated)
            {
                var donHang = db.DonHangs.Where(x => x.User.Email == User.Identity.Name).ToList();
                return View(donHang);
            }
            else
            {
                var donHang = db.DonHangs.Where(x => x.SDT == sdt).ToList();
                return View(donHang);
            }
        }
        public ActionResult ChiTietDonHang(int id)
        {
            var chiTietDonHang = db.ChiTietDonHangs.Where(x => x.MaDH ==  id).ToList();
            return View(chiTietDonHang);
        }
        public ActionResult Huy(int id)
        {
            var donHangs = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            donHangs.TinhTrangDonHang = "Đã hủy";
            db.Entry(donHangs).State = EntityState.Modified;
            db.SaveChanges();
            var increaseProduct = db.ChiTietDonHangs.FirstOrDefault(x => x.MaDH == donHangs.MaDH);
            increaseProduct.Sach.SoLuong += 1;
            db.Entry(increaseProduct).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DonHang", "ThanhToan");
        }
    }
}