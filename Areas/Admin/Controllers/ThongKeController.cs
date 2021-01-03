using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        CSDLContext db = new CSDLContext();

        public ActionResult Index(int? month)
        {
            int[] days = {
                1,2,3,4,5,6,7,8,9,10,
                11,12,13,14,15,16,17,18,19,20,
                21,22,23,24,25,26,27,28,29,30,31};
            ViewBag.Month = month;
            ViewBag.days = days;
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            ViewBag.months = months;
            ViewBag.count = 1;
            ViewBag.countDays = 1;
            int[] outcomeDay = days;
            if (month == null)
            {
                ViewBag.alert = "Select Month";
            }
            else
            {
                for (int i = 0; i < days.Length; i++)
                {
                    int y = days[i];
                    var test1 = db.ChiTietDonHangs.Where(x => x.DonHang.NgayDat.Day == y && x.DonHang.NgayDat.Month == month && x.DonHang.TinhTrangDonHang == "Đã giao").ToList();
                    if (test1.Count == 0)
                    {
                        outcomeDay[i] = 0;
                    }
                    else
                    {
                        outcomeDay[i] = test1.Sum(x => x.TongTien);
                    }
                }
                ViewBag.test1 = outcomeDay;
                for (int i = 0; i <= months.Length; i++)
                {
                    if (i == month)
                    {
                        ViewBag.oneMonth = months[i - 1];
                    }
                }
                int? monthOutcome = db.ChiTietDonHangs.Where(x => x.DonHang.NgayDat.Month == month && x.DonHang.TinhTrangDonHang == "Đã giao").Sum(x => (int?)x.TongTien) ?? 0;
                if (monthOutcome == null)
                {
                    ViewBag.monthOutcome = "0";
                }
                else
                {
                    ViewBag.monthOutcome = monthOutcome;
                }
            }
            return View();
        }
        public ActionResult GetMonth(int Month)
        {
            return RedirectToAction("Index", "ThongKe", new { month = Month });
        }
        //Hiển thị doanh thu 
        public ActionResult QLDoanhThu(int? Day, int? Month, int? Year)
        {

            int[] days = {
                1,2,3,4,5,6,7,8,9,10,
                11,12,13,14,15,16,17,18,19,20,
                21,22,23,24,25,26,27,28,29,30,31};
            int[] month = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int year = DateTime.Now.Year;
            int[] years = new int[10];
            for (int i = 0; i < years.Length; i++)
            {
                if (i == 0)
                {
                    years[i] = year;
                }
                else
                {
                    year = year + 1;
                    years[i] = year;
                }
            }
            ViewBag.year = years;
            ViewBag.day = days;
            ViewBag.month = month;


            if (Day == null || Month == null)
            {
                var order = db.DonHangs.Where(x => x.NgayDat.Day == DateTime.Now.Day && x.NgayDat.Month == DateTime.Now.Month && x.NgayDat.Year == Year && x.TinhTrangDonHang == "Đã giao").ToList();
                ViewData["order"] = order;
            }
            else
            {
                var order = db.DonHangs.Where(x => x.NgayDat.Day == Day && x.NgayDat.Month == Month && x.NgayDat.Year == Year && x.TinhTrangDonHang == "Đã giao").ToList();
                ViewData["order"] = order;
                if (order.Count != 0)
                {
                    var orderDetail = db.DonHangs.Where(x => x.NgayDat.Day == DateTime.Now.Day && x.NgayDat.Month == DateTime.Now.Month && x.NgayDat.Year == Year && x.TinhTrangDonHang == "Đã giao").Sum(x => x.TongTien);
                    ViewData["orderDetail"] = orderDetail;
                }
                else
                {
                    ViewData["orderDetail"] = "0";
                }
            }

            return View();
        }
        public ActionResult ChiTietDon(int id)
        {
            var orderDetail = db.ChiTietDonHangs.Where(x => x.MaDH == id).ToList();
            var total = db.DonHangs.Where(x => x.MaDH == id).Sum(x => x.TongTien);
            ViewBag.total = total;
            return View(orderDetail);
        }
    }
}