using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string Review { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]
        public int? UserID { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Sach")]
        public int? ProductID { get; set; }
        public virtual Sach Sach { get; set; }
    }
}