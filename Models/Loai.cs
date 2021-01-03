using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Loai
    {
        [Key]
        public int MaLoai { get; set; }

        [DisplayName("Tên thể loại")]
        [Required(ErrorMessage ="Nhập tên thể loại")]
        [StringLength(10, ErrorMessage = "Tên thể loại không vượt quá 10 ký tự")]
        public string TenLoai { get; set; }

        public ICollection<Sach> Saches { get; set; }

    }
}