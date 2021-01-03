using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Rank
    {
        [Key]
        public int Ranking { get; set; }

        [DisplayName("Xếp loại khách hàng")]
        [Required(ErrorMessage = "Nhập tên loại khách hàng")]
        public string TenRank { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}