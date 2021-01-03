using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class DanhGia
    {
        [Key]
        public int MaGY { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Họ Tên!")]
        [StringLength(30)]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập SDT!")]
        [StringLength(20)]
        [DisplayName("SĐT")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ! Hãy kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Nội Dung cần góp ý!")]
        [StringLength(100)]
        [DisplayName("Nội Dung")]
        public string NoiDung { get; set; }

    }
}