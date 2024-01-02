using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("ID Tour")]
        public int TourId { get; set; }
        [Required]
        [DisplayName("ID Khách hàng")]
        public int CustomerId { get; set; }
        [Required]
        [DisplayName("Số sao")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập bình luận.")]
        [DisplayName("Thông tin")]
        public string Comment { get; set; }
        [DisplayName("Ngày bình luận")]
        [Required]
        public DateTime ReviewDate { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
