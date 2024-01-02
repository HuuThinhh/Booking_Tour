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
    [Table("Place")]
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên địa điểm không được để trống")]
        [DisplayName("Tên địa điểm")]
        public string Name { get; set; }
    }
}