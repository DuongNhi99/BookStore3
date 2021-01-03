using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        CSDLContext db = new CSDLContext();
        public ActionResult Index()
        {
            List<Sach> kynang = db.Saches.Where(a => a.MaLoai == 1).OrderBy(a => a.MaSach).Take(8).ToList();
            List<Sach> thieunhi = db.Saches.Where(a => a.MaLoai == 2).OrderBy(a => a.MaSach).Take(8).ToList();
            List<Sach> vanhoc = db.Saches.Where(a => a.MaLoai == 3).OrderBy(a => a.MaSach).Take(8).ToList();
            List<Sach> slider = db.Saches.OrderByDescending(a => a.MaSach).Take(5).ToList();
            ViewBag.kynang = kynang;
            ViewBag.thieunhi = thieunhi;
            ViewBag.vanhoc = vanhoc;
            ViewBag.slider = slider;
            return View();
        }

        public ActionResult DanhMucSach(int? page, int maLoai, int? min, int? max)
        {
            min = (min ?? 10000);
            max = (max ?? 5000000);
            ViewBag.min = min;
            ViewBag.max = max;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var ds = db.Saches.Where(t => (t.Gia <= max) && (t.Gia >= min)).OrderBy(x => x.TenSach).Where(a => a.MaLoai == maLoai).ToPagedList(pageNumber, pageSize);
            var pagecount = ds.PageCount;
            ViewBag.pc = pagecount;
            ViewBag.pageNumber = pageNumber;
            Loai br = db.Loais.Find(maLoai);
            ViewBag.br = br;
            ViewBag.maLoai = maLoai;
            ViewBag.ds = ds;
            return View();
        }
        public ActionResult Comment([Bind(Include = "ID, Review, UserID, ProductID")] Comment comment, int sanpham)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            comment.UserID = user.IDUser;
            comment.ProductID = sanpham;
            comment.Date = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("ChiTietSach","Home", new {id = sanpham });
        }
        public ActionResult ChiTietSach(int id)
        {
            var comment = db.Comments.Where(x => x.ProductID == id).ToList();
            ViewBag.comments = comment;
            Sach pd = db.Saches.Find(id);
            Loai br = db.Loais.Find(pd.MaLoai);
            ViewBag.pd = pd;
            ViewBag.br = br;
            return View();
        }

        [HttpGet]
        public ActionResult TimKiemTheoTen(string name, int? page)
        {
            ViewBag.name = name;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            //List<Product> ds = db.Products.Where(t => t.ProductName.Contains(name)).OrderBy(x => x.ProductName).ToList();
            var ds = db.Saches.Where(t => t.TenSach.Contains(name)).OrderBy(x => x.TenSach).ToPagedList(pageNumber, pageSize);
            var pagecount = ds.PageCount;
            //var pagecount = ds.ToPagedList(pageNumber, pageSize).PageCount;
            ViewBag.pc = pagecount;
            ViewBag.pageNumber = pageNumber;
            return View(ds);
            //return View(db.Products.Where(t => t.ProductName.Contains(name)).OrderBy(x => x.ProductName).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }


    }
}