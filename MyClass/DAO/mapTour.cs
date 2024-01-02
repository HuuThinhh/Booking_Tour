using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class mapTour
    {
        public DbConnect db = new DbConnect();

        public Tour TimKiem(int id)
        {
            try
            {
                if (db != null)
                {
                    var tour = db.Tours.FirstOrDefault(t => t.Id == id);

                    if (tour != null)
                    {
                        return tour;
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        //public List<Tour> DanhSach(string loaitour, int placeId)
        //{
        //    var tours = db.Tours.ToList();
        //    if (string.IsNullOrEmpty(loaitour))
        //    {
        //        return tours;
        //    }
        //    else
        //    {
        //        var fillter_tours = tours.Where(tour => tour.Type.Equals(loaitour, StringComparison.OrdinalIgnoreCase)).ToList();
        //        return fillter_tours;
        //    }
        //}
        public List<Tour> DanhSach(string loaitour, int placeId, string searchString)
        {
            var tours = db.Tours.ToList();
            tours = tours.Where(tour => tour.EndDate > DateTime.Now).ToList();
            //search
            if (!string.IsNullOrEmpty(searchString))
            {
                tours = tours.Where(tour => tour.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            // Filter by Type
            if (!string.IsNullOrEmpty(loaitour))
            {
                tours = tours.Where(tour => tour.Type.Equals(loaitour, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter by PlaceId
            if (placeId != 0)
            {
                tours = tours.Where(tour => tour.PlaceId == placeId).ToList();
            }

            return tours;
        }


    }
}
