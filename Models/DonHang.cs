using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }

        //public int? IDUser { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [DisplayName("Ngày đặt hàng")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy }",
            ApplyFormatInEditMode = true)]
        public DateTime NgayDat { get; set; }

        [StringLength(20)]
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }

        [DisplayName("Giá vận chuyển")]
        public int GiaVanChuyen { get; set; }

        [StringLength(20)]
        [DisplayName("Mã Khuyến mãi")]
        public string KhuyenMai { get; set; }

        [DisplayName("Tổng Tiền")]
        public float? TongTien { get; set; }

        [DisplayName("Loại thanh toán")]
        public string LoaiTT { get; set; }

        [DisplayName("Tình trạng")]
        public string TinhTrangDonHang { get; set; }

        [ForeignKey("User")]
        public int? IDUser { get; set; }
        public virtual User User { get; set; }

        //[ForeignKey("ThanhToan")]
        //public int? MaTT { get; set; }
        //public virtual ThanhToan ThanhToan { get; set; }

        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

    }
}