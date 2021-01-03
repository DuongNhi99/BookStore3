using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: Admin/DonHang
        public ActionResult Index(int? begin)
        {

            //var donHangs = db.DonHangs;
            //return View(donHangs.ToList());
            int k = 0;
            if (begin != null)
            {
                k = begin.GetValueOrDefault() * 10;
            }
            List<DonHang> sp = db.DonHangs.OrderBy(s => s.MaDH).Skip(k).Take(8).ToList();
            ViewBag.count = db.DonHangs.Count() / 7;
            return View(sp);
        }

        // GET: Admin/DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }
        public ActionResult ChiTietDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: Admin/DonHang/Create
        public ActionResult Create()
        {
            ViewBag.SDT = new SelectList(db.Users, "SDT", "HoTen");
            return View();
        }

        // POST: Admin/DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,SDT,NgayDat, DiaChi,GiaVanChuyen,KhuyenMai,TongTien,TinhTrangDonHang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SDT = new SelectList(db.Users, "SDT", "HoTen", donHang.SDT);
            return View(donHang);
        }

        // GET: Admin/DonHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.SDT = new SelectList(db.Users, "SDT", "HoTen", donHang.SDT);
            return View(donHang);
        }

        // POST: Admin/DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,SDT,NgayDat,GiaVanChuyen,KhuyenMai,TongTien,TinhTrangDonHang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SDT = new SelectList(db.Users, "SDT", "HoTen", donHang.SDT);
            return View(donHang);
        }

        // GET: Admin/DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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
        public ActionResult Accept(int id)
        {
            var donHangs = db.DonHangs.FirstOrDefault(x=>x.MaDH == id);
            if (donHangs.User == null)
            {

                donHangs.TinhTrangDonHang = "Đã duyệt";
                db.Entry(donHangs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "DonHang");
            }
            else
            {

                donHangs.TinhTrangDonHang = "Đã duyệt";
                db.Entry(donHangs).State = EntityState.Modified;
                db.SaveChanges();

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("yiyi120509@gmail.com");
                msg.To.Add(donHangs.User.Email);
                msg.Subject = "Bookstore NTN";
                msg.Body = "Your order id: " + donHangs.MaDH + ", Đơn hàng của bạn đã được duyệt rồi đấy hehehe";

                //msg.Priority = MailPriority.High;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("yiyi120509@gmail.com", "duongyennhi");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                }
                if (donHangs.User.DiemTichLuy >= 300000 && donHangs.User.DiemTichLuy < 500000)
                {
                    donHangs.User.Ranking = 2;
                    db.Entry(donHangs).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (donHangs.User.DiemTichLuy >= 500000 && donHangs.User.DiemTichLuy < 1000000)
                {
                    donHangs.User.Ranking = 3;
                    db.Entry(donHangs).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (donHangs.User.DiemTichLuy >= 1000000)
                {
                    donHangs.User.Ranking = 4;
                    db.Entry(donHangs).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index","DonHang");
        }
        public ActionResult Huy(int id)
        {
            var donHangs = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            if (donHangs.User == null)
            {
                donHangs.TinhTrangDonHang = "Đã hủy";
                db.Entry(donHangs).State = EntityState.Modified;
                db.SaveChanges();
            }
               else
            {
                 MailMessage msg = new MailMessage();
                msg.From = new MailAddress("yiyi120509@gmail.com");
                msg.To.Add(donHangs.User.Email);
                msg.Subject = "Bookstore NTN";
                msg.Body = "Your order id: " + donHangs.MaDH + ", Đơn hàng của bạn đã bị hủy";

                //msg.Priority = MailPriority.High;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("yiyi120509@gmail.com", "duongyennhi");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                }
                donHangs.TinhTrangDonHang = "Đã hủy";
                db.Entry(donHangs).State = EntityState.Modified;
                db.SaveChanges();
            }
            var increaseProduct = db.ChiTietDonHangs.FirstOrDefault(x => x.MaDH == donHangs.MaDH);
            increaseProduct.Sach.SoLuong += 1;
            db.Entry(increaseProduct).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "DonHang");
        }
        public ActionResult DaGiao(int id)
        {
            var donHangs = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            donHangs.TinhTrangDonHang = "Đã giao";
            db.Entry(donHangs).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "DonHang");
        }
        public ActionResult DHCN()
        {
            var order = db.DonHangs.Where(x => x.TinhTrangDonHang == "Đã duyệt").ToList();
            return View(order);
        }
        //Đơn hàng đang chờ
        public ActionResult DHDC()
        {
            var order = db.DonHangs.Where(x => x.TinhTrangDonHang == "Đang duyệt").ToList();
            return View(order);
        }
        //Đơn hàng đã xong
        public ActionResult DHDG()
        {
            var order = db.DonHangs.Where(x => x.TinhTrangDonHang == "Đã giao").ToList();
            return View(order);
        }
        //Đơn hàng đã hủy
        public ActionResult DHDH()
        {
            var order = db.DonHangs.Where(x => x.TinhTrangDonHang == "Đã Hủy").ToList();
            return View(order);
        }
    }
}
