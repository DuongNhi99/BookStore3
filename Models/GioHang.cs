using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class GioHang
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        //public Sach Sach { get; set; }

        [DisplayName("Ảnh")]
        public string HinhAnh { get; set; }
        public int MaSach { get; set; }
        [DisplayName("Tên Sách")]
        public string TenSach { get; set; }
        [DisplayName("Đơn giá")]
        public int Gia { get; set; }
        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }
        [DisplayName("Thành tiền")]
        public int TongTien
        {
            get
            {
                return SoLuong * Gia;
            }
        }
    }
}