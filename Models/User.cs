using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class User
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public User()
        //{
        //    DonHangs = new HashSet<DonHang>();
        //}

        [Key]
        public int IDUser { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Họ Tên!")]
        [StringLength(30)]
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập SDT!")]
        //[StringLength(20)]
        //[DisplayName("SĐT")]
        //public string SDT { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập Địa chỉ!")]
        //[StringLength(30)]
        //[DisplayName("Địa chỉ")]
        //public string DiaChi { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ! Hãy kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Password!")]
        [RegularExpression(@"^(?=.*[a-z]).{8,15}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [ForeignKey("PhanQuyen")]
        public int? MaQuyen { get; set; }
        public virtual PhanQuyen PhanQuyen { get; set; }

        [DisplayName("Tuổi")]
        public int Age { get; set; }

        [DisplayName("Điểm tích lũy")]
        public int? DiemTichLuy { get; set; }

        [DisplayName("Loại khách hàng")]
        [ForeignKey("Rank")]
        public int? Ranking { get; set; }
        public virtual Rank Rank { get; set; }
        //[Required(ErrorMessage = "Bạn chưa nhập Họ Tên!")]
        //[StringLength(30)]
        //[DisplayName("Họ Tên")]
        //public string HoTen { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập SDT!")]
        //[StringLength(20)]
        //[DisplayName("SĐT")]
        //public string SDT { get; set; }


        //[Required(ErrorMessage = "Bạn chưa nhập Email!")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        //[EmailAddress(ErrorMessage = "Email không hợp lệ! Hãy kiểm tra lại.")]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Bạn chưa nhập Password!")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //[Required(ErrorMessage = "Error ConfirmPassword!")]
        //[NotMapped]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
        //[NotMapped]
        //public string LoginErrorMessage { get; set; }

        //[ForeignKey("PhanQuyen")]
        //public int? MaQuyen { get; set; }
        //public virtual PhanQuyen PhanQuyen { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public ICollection<DonHang> DonHangs { get; set; }
        public ICollection<DonHang> donHangs { get; set; }
        public ICollection<Comment> comments { get; set; }
    }
}