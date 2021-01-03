using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
        [Key]
        public int MaSach { get; set; }

        [DisplayName("Tên Sách")]
        [Required(ErrorMessage = "Nhập tên sách")]
        [StringLength(50, ErrorMessage = "Tên sách không vượt quá 50 ký tự")]
        public string TenSach { get; set; }

        [DisplayName("Tác Giả")]
        [Required(ErrorMessage = "Nhập tên tác giả")]
        [StringLength(50, ErrorMessage = "Tên tác giả không vượt quá 20 ký tự")]
        public string TacGia { get; set; }

        [DisplayName("NXB")]
        [Required(ErrorMessage = "Nhập tên NXB")]
        [StringLength(50, ErrorMessage = "Tên tác giả không vượt quá 20 ký tự")]
        public string NXB { get; set; }

        [DisplayName("Giá")]
        [Required(ErrorMessage = "Nhập giá sản phẩm")]
        public int Gia { get; set; }

        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Nhập mô tả")]
        [StringLength(500, ErrorMessage = "Mô tả không vượt quá 500 ký tự")]
        public string Description { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Ảnh")]
        public string HinhAnh { get; set; }

        [ForeignKey("Loai")]
        public int? MaLoai { get; set; }
        public virtual Loai Loai { get; set; }

        [ForeignKey("NhaCungCap")]
        public int? NCC { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public ICollection<Comment> comments { get; set; }
    }
}