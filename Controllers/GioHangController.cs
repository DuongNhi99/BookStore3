using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class GioHangController : Controller
    {
        CSDLContext db = new CSDLContext();
        // GET: GioHang
        public ActionResult Index()
        {
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            return View(giohang);
        }

        public RedirectResult ThemVaoGio(int MaSach)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<GioHang>();
            }
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;

            if (giohang.FirstOrDefault(m => m.MaSach == MaSach) == null)
            {
                Sach pd = db.Saches.Find(MaSach);
                GioHang newItem = new GioHang()
                {
                    MaSach = MaSach,
                    TenSach = pd.TenSach,
                    SoLuong = 1,
                    HinhAnh = pd.HinhAnh,
                    Gia = pd.Gia
                };
                giohang.Add(newItem);
            }
            else
            {
                GioHang cardItem = giohang.FirstOrDefault(m => m.MaSach == MaSach);
                cardItem.SoLuong++;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public RedirectToRouteResult SuaSoLuong(int MaSach, int soluongmoi)
        {
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            GioHang itemSua = giohang.FirstOrDefault(m => m.MaSach == MaSach);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult XoaKhoiGio(int MaSach)
        {
            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            GioHang itemXoa = giohang.FirstOrDefault(m => m.MaSach == MaSach);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);

            }
            return RedirectToAction("Index");
        }
    }
}