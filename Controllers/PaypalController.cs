using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class PaypalController : Controller
    {
        CSDLContext db = new CSDLContext();
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }
        #region thanh toan bang tk paypal
        public ActionResult PaymentWithPaypal()
        {

            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            if(giohang == null)
            {
                int d = 0;
            }
            else
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
                    donHang.GiaVanChuyen = 20000;
                    donHang.KhuyenMai = "";
                    donHang.TongTien = cart.Sum(x => x.Gia * x.SoLuong);
                    donHang.IDUser = user.IDUser;
                    donHang.TinhTrangDonHang = "Đang duyệt";
                    donHang.LoaiTT = "Paypal";
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    if(donHang.User.Ranking == 1)
                    {
                        foreach (var item in cart)
                        {
                            ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                            {
                                SoLuong = item.SoLuong,
                                TongTien = 0,
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
                        dtl.DiemTichLuy = dtl.DiemTichLuy + cart.Sum(x => x.Gia * x.SoLuong);
                        db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (donHang.User.Ranking == 2)
                    {
                        donHang.TongTien = cart.Sum(x => x.Gia * x.SoLuong) * 95 / 100;
                        db.DonHangs.Add(donHang);
                        db.SaveChanges();
                        foreach (var item in cart)
                        {
                            ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                            {
                                SoLuong = item.SoLuong,
                                TongTien = 0,
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
                        dtl.DiemTichLuy = dtl.DiemTichLuy + cart.Sum(x => x.Gia * x.SoLuong);
                        db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (donHang.User.Ranking == 3)
                    {
                        donHang.TongTien = cart.Sum(x => x.Gia * x.SoLuong) * 90 / 100;
                        foreach (var item in cart)
                        {
                            ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                            {
                                SoLuong = item.SoLuong,
                                TongTien = 0,
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
                        dtl.DiemTichLuy = dtl.DiemTichLuy + cart.Sum(x => x.Gia * x.SoLuong);
                        db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else if (donHang.User.Ranking == 4)
                    {
                        donHang.TongTien = cart.Sum(x => x.Gia * x.SoLuong) * 85 / 100;
                        foreach (var item in cart)
                        {
                            ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                            {
                                SoLuong = item.SoLuong,
                                TongTien = 0,
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
                        dtl.DiemTichLuy = dtl.DiemTichLuy + cart.Sum(x => x.Gia * x.SoLuong);
                        db.Entry(dtl).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                        
                }
                else
                {
                    DonHang donHang = new DonHang();
                    donHang.SDT = sdt;
                    donHang.NgayDat = DateTime.Now;
                    donHang.GiaVanChuyen = 0;
                    donHang.KhuyenMai = "";

                    donHang.TongTien = cart.Sum(x => x.Gia * x.SoLuong);
                    donHang.TinhTrangDonHang = "Đang duyệt";
                    donHang.LoaiTT = "Paypal";
                    db.DonHangs.Add(donHang);
                    db.SaveChanges();
                    foreach (var item in cart)
                    {
                        ChiTietDonHang chiTietDonHang = new ChiTietDonHang()
                        {
                            SoLuong = item.SoLuong,
                            TongTien = 0,
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
            }

            //getting the apiContext as earlier
            APIContext apiContext = PaypalConfigruation.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";


                    var guid = Convert.ToString((new Random()).Next(100000));

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);


                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);
                   
                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }
            giohang.Clear();
            return View("SuccessView");
        }
        #endregion

        #region code dựng
        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
           
          
            var itemList = new ItemList() { items = new List<Item>() };



            List<GioHang> giohang = Session["giohang"] as List<GioHang>;
            foreach (var i in giohang)
            {
                itemList.items.Add(new PayPal.Api.Item()
                {
                    name = i.TenSach.ToString(),
                    price = i.Gia.ToString(),
                    quantity = i.SoLuong.ToString(),
                    sku = "sku",
                    currency = "USD",

                });
               
            }


            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = giohang.Sum(x => x.Gia * x.SoLuong).ToString(),
            };
          
            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Vận chuyển về Việt Nam",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);

        }
        #endregion
    }
}