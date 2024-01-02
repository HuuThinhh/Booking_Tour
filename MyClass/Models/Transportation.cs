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
    [Table("Transportation")]
    public class Transportation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên phương tiện không được để trống")]
        [DisplayName("Tên phương tiện")]
        public string Name { get; set; }
    }
}

