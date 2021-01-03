using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class PhanQuyen
    {
        [Key]
        public int MaQuyen { get; set; }

        [DisplayName("Tên quyền")]
        [Required(ErrorMessage = "Nhập tên quyền")]
        [StringLength(10, ErrorMessage = "Tên quyềnkhông vượt quá 10 ký tự")]
        public string LoaiQuyen { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}