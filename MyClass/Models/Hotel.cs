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
    [Table("Hotel")]
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên khách sạn không được để trống")]
        [DisplayName("Tên khách sạn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(?:\+84|0)[1-9]\d{8,9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}
