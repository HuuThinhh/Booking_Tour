using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyClass.Models
{
    [Table("Tour")]
    public class Tour
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Tour không được để trống")]
        [DisplayName("Tên Tour")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [Required(ErrorMessage = "ID Địa điểm không được để trống")]
        [DisplayName("ID Địa điểm")]
        public int PlaceId { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống")]
        [DisplayName("Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
        public int NumberOfParticipants { get; set; }
        [Required(ErrorMessage = "Giá người lớn không được để trống")]
        [DisplayName("Giá người lớn")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá người lớn không hợp lệ")]
        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public double AdultPrice { get; set; }
        [Required(ErrorMessage = "Giá trẻ em không được để trống")]
        [DisplayName("Giá trẻ em")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá trẻ em không hợp lệ")]
        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public double ChildPrice { get; set; }
        //[Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        //[DisplayName("Ngày bắt đầu")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        //public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        [DisplayName("Ngày kết thúc")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        //[Required(ErrorMessage = "Ảnh không được để trống")]
        [DisplayName("Ảnh")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [DisplayName("Trong ngày?")]
        public bool? IsDomestic { get; set; }
        [Required]
        [DisplayName("Số ngày")]
        public string Type { get; set; }
        public virtual Place Place { get; set; }
    }
}
