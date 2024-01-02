using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Emplyee")]
    public class Emplyee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string FisrtName { get; set; }
        [Required]
        [StringLength(15)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set;}
        [Required]
        [StringLength(25)]
        public string Password { get; set;}
        [Required]
        [StringLength(50)]
        public bool Role { get; set;}
    }
}
