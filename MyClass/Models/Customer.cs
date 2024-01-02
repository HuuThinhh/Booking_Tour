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
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ.")]
        [DisplayName("Tên")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [DisplayName("Họ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Số điện thoại.")]
        [RegularExpression(@"^(?:\+84|0)[1-9]\d{8,9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

    }
}
