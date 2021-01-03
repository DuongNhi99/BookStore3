using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNCC { get; set; }

        [DisplayName("Tên NCC")]
        [Required(ErrorMessage = "Nhập tên NCC")]
        [StringLength(10, ErrorMessage = "Tên NCC không vượt quá 10 ký tự")]
        public string TenNCC { get; set; }

        public ICollection<Sach> Saches { get; set; }
    }
}