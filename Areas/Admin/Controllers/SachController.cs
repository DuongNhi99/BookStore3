using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SachController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: Admin/Sach
        public ActionResult Index(int? begin)
        {
            //var saches = db.Saches.Include(s => s.Loai).Include(s => s.NhaCungCap);
            //return View(saches.ToList());
            int k = 0;
            if (begin != null)
            {
                k = begin.GetValueOrDefault() * 10;
            }
            List<Sach> sp = db.Saches.OrderBy(s => s.MaSach).Skip(k).Take(8).ToList();
            ViewBag.count = db.Saches.Count() / 7;
            return View(sp);
        }

        // GET: Admin/Sach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Admin/Sach/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.Loais, "MaLoai", "TenLoai");
            ViewBag.NCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }

        // POST: Admin/Sach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,TacGia,NXB,Gia,Description,SoLuong,HinhAnh,MaLoai,NCC")] Sach sach, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    var file = Path.GetFileName(HinhAnh.FileName);
                    sach.HinhAnh = file;
                    string path = Path.Combine(Server.MapPath("~/Image"), file);
                    HinhAnh.SaveAs(path);
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.Loais, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.NCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sach.NCC);
            return View(sach);
        }

        // GET: Admin/Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.Loais, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.NCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sach.NCC);
            return View(sach);
        }

        // POST: Admin/Sach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,TacGia,NXB,Gia,Description,SoLuong,HinhAnh,MaLoai,NCC")] Sach sach, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    var file = Path.GetFileName(HinhAnh.FileName);
                    sach.HinhAnh = file;
                    string path = Path.Combine(Server.MapPath("~/Image"), file);
                    HinhAnh.SaveAs(path);
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.Loais, "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.NCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sach.NCC);
            return View(sach);
        }

        // GET: Admin/Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Admin/Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
