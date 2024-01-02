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
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [DisplayName("Tên")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Họ không được để trống")]
        [DisplayName("Họ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(?:\+84|0)[1-9]\d{8,9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vai trò không được để trống")]
        [DisplayName("Vai trò")]
        public int RoleId { get; set; }
        [DisplayName("Lỗi sai mật khẩu")]
        public int? CountError { get; set; }
    }
}

