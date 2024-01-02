using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class DetailView
    {
        public Tour TourDt { get; set; }
        public List<Review> ReviewsDt { get; set; }       
        public Review NewReview { get; set; }
    }
}
