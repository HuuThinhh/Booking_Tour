using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class StatisticController : LoginController
    {
        DbConnect db = new DbConnect();
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatisticalTour2()
        {

            var query = (from a in db.Tours
                         join b in db.Bookings on a.Id equals b.TourId
                         where b.Status == 2
                         select new
                         {
                             idTour = b.TourId,
                             TourName = a.Name,
                             Adults = b.NumberOfAdults,
                             Children = b.NumberOfChildren,
                             Total = b.TotalPrice
                         }
                      );
            var result = query.GroupBy(x => x.idTour).Select(x =>
                new
                {
                    idTour = x.Key,
                    TourName = x.FirstOrDefault().TourName,
                    Adults = x.Sum(y => y.Adults),
                    Children = x.Sum(y => y.Children),
                    Total = x.Sum(y => y.Total) * 0.25
                });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStatisticalTour3()
        {

            var query = (from a in db.Tours
                         join b in db.Bookings on a.Id equals b.TourId
                         where b.Status == 3
                         select new
                         {
                             idTour = b.TourId,
                             TourName = a.Name,
                             Adults = b.NumberOfAdults,
                             Children = b.NumberOfChildren,
                             Total = b.TotalPrice
                         }
                      );
            var result = query.GroupBy(x => x.idTour).Select(x =>
                new
                {
                    idTour = x.Key,
                    TourName = x.FirstOrDefault().TourName,
                    Adults = x.Sum(y => y.Adults),
                    Children = x.Sum(y => y.Children),
                    Total = x.Sum(y => y.Total)
                });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStatisticalMonth3()
        {
            var query = from a in db.Bookings
                        where a.Status == 3
                        select new
                        {
                            DateStatistic = a.BookDate,
                            Adults = a.NumberOfAdults,
                            Children = a.NumberOfChildren,
                            Total = a.TotalPrice
                        };

            var result = query.GroupBy(x => new { month = x.DateStatistic.Month, year = x.DateStatistic.Year })
                .Select(x => new
                {
                    Month = x.Key.month,
                    Year = x.Key.year,
                    Adults = x.Sum(y => y.Adults),
                    Children = x.Sum(y => y.Children),
                    Total = x.Sum(y => y.Total)
                });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStatisticalMonth2()
        {
            var query = from a in db.Bookings
                        where a.Status == 2
                        select new
                        {
                            DateStatistic = a.BookDate,
                            Adults = a.NumberOfAdults,
                            Children = a.NumberOfChildren,
                            Total = a.TotalPrice
                        };

            var result = query.GroupBy(x => new { month = x.DateStatistic.Month, year = x.DateStatistic.Year })
                .Select(x => new
                {
                    Month = x.Key.month,
                    Year = x.Key.year,
                    Adults = x.Sum(y => y.Adults),
                    Children = x.Sum(y => y.Children),
                    Total = x.Sum(y => y.Total) * 0.25
                });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}