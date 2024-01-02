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
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("ID Tour")]
        public int TourId { get; set; }
        [DisplayName("ID Hotel")]
        public int HotelId { get; set; }
        [DisplayName("ID_Transportation")]
        public int TransportationId { get; set; }
        [DisplayName("ID User")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày bạn muốn đi")]
        [DisplayName("Ngày đi tour")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookDate { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số người lớn")]
        //[Range(0, int.MaxValue, ErrorMessage = "Số người không thể là số âm.")]
        [DisplayName("Người Lớn")]
        public int NumberOfAdults { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số trẻ em")]
        //[Range(0, int.MaxValue, ErrorMessage = "Số người không thể là số âm.")]
        [DisplayName("Trẻ Em")]
        public int NumberOfChildren { get; set; }
        [DisplayName("Tổng Tiền")]
        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int TotalPrice { get; set; }
        [DisplayName("Tình Trạng")]
        public int Status { get; set; }
        public virtual Tour Tour { get; set; }
        //public virtual Customer Customer { get; set; }
    }
}




