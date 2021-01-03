using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int MaDHCT { get; set; }

        [ForeignKey("DonHang")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }
        public virtual DonHang DonHang { get; set; }
        // public virtual Order Order { get; set; }

        [ForeignKey("Sach")]
        public int? MaSach { get; set; }
        public virtual Sach Sach { get; set; }

        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }

        //[DisplayName("Tổng tiền")]
        //public string Total { get; set; }
        [DisplayName("Tổng tiền")]
        public int TongTien { get; set; }
    }
}