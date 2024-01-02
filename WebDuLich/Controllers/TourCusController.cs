using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MyClass.DAO;
using MyClass.Models;


namespace WebDuLich.Controllers
{
    public class TourCusController : Controller
    {
        // GET: Tour
        TourDAO tourDAO = new TourDAO();
        BookingDAO bookingDAO = new BookingDAO();
        DbConnect db = new DbConnect();


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Index(string searchString)
        {
            ViewData["searchString"] = searchString;
            return View();
        }
        //public ActionResult listTour(string loaitour , int placeId) 
        //{
        //    mapTour map = new mapTour();
        //    List<Tour> tours = map.DanhSach(loaitour,placeId);
        //    return PartialView("_TourListPartial", tours);
        //}
        public ActionResult listTour(string loaitour, string searchString, int placeId = 0)
        {
            mapTour map = new mapTour();
            List<Tour> tours = map.DanhSach(loaitour, placeId, searchString);
            return PartialView("_TourListPartial", tours);
        }
        //public ActionResult Detail(int? id)
        //{
        //    var idtour = db.Tours.Find(id);
        //    ViewBag.SelectedTour = idtour;
        //    Customer cus = (Customer)Session["user"];
        //    var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
        //    ViewBag.AdultPrice = tourInfo?.AdultPrice;
        //    ViewBag.ChildPrice = tourInfo?.ChildPrice;
        //    ViewBag.Name = tourInfo?.Name;
        //    ViewBag.Image = tourInfo?.Image;
        //    ViewBag.Description = tourInfo?.Description;
        //    ViewBag.EndDate = tourInfo?.EndDate;
        //    ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
        //    var listreview = db.Reviews
        //.Where(b => b.TourId == id)
        //.ToList();
        //    return View();

        //}
        public ActionResult Detail(int? id)
        {
            var idtour = db.Tours.Find(id);
            ViewBag.SelectedTour = idtour;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.Name = tourInfo?.Name;
            ViewBag.Image = tourInfo?.Image;
            ViewBag.Description = tourInfo?.Description;
            ViewBag.EndDate = tourInfo?.EndDate;
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            Customer cus = (Customer)Session["user"];
            var tour = db.Tours.FirstOrDefault(t => t.Id == id);

            if (tour == null)
            {
                return HttpNotFound();
            }

            var reviews = db.Reviews.Where(r => r.TourId == id).ToList();

            var viewModel = new DetailView
            {
                TourDt = tour,
                ReviewsDt = reviews,
                NewReview = new Review()
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Detail(int? id, DetailView viewModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tour = db.Tours.Find(id);

            if (tour == null)
            {
                return HttpNotFound();
            }
           

            if (ModelState.IsValid)
            {
                var currentUser = (Customer)Session["user"];
                var booking = db.Bookings.FirstOrDefault(b => b.UserId == currentUser.Id && b.TourId == tour.Id && b.Status == 3);
                int numberOfBookings = db.Bookings.Count(b => b.UserId == currentUser.Id && b.TourId == tour.Id && b.Status == 3);
                int numberOfReviews = db.Reviews.Count(r => r.CustomerId == currentUser.Id && r.TourId == tour.Id);
                if (booking != null && numberOfReviews < numberOfBookings)
                {
                    viewModel.NewReview.CustomerId = ((Customer)Session["user"]).Id;
                    viewModel.NewReview.TourId = tour.Id;
                    viewModel.NewReview.ReviewDate = DateTime.Now;
                    db.Reviews.Add(viewModel.NewReview);
                    db.SaveChanges();
                    TempData["message"] = new XMessage("success", "Bạn đã thêm bình luận thành công");
                    return RedirectToAction("Detail", new { id = tour.Id });
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Bạn hãy trải nghiệm tour này để bình luận hoặc đã bình luận trước đó");
                    return RedirectToAction("Detail", new { id = tour.Id });
                }
            }

     
            viewModel.ReviewsDt = db.Reviews.Where(r => r.TourId == id).ToList();
            viewModel.TourDt = tour;
            var idtour = db.Tours.Find(id);
            ViewBag.SelectedTour = idtour;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.Name = tourInfo?.Name;
            ViewBag.Image = tourInfo?.Image;
            ViewBag.Description = tourInfo?.Description;
            ViewBag.EndDate = tourInfo?.EndDate;
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            Customer cus = (Customer)Session["user"];
            return View(viewModel);
        }
        

        public ActionResult BookingTour(int? id)
        {         
            var idtour = db.Tours.Find(id);
            ViewBag.SelectedTour = idtour;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.Name = tourInfo?.Name;
            ViewBag.Image = tourInfo?.Image;
            ViewBag.EndDate = tourInfo?.EndDate.ToString("yyyy-MM-dd");
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            ViewBag.ListHotelId = new SelectList(db.Hotels.Where(m => m.Id != 0).ToList(), "Id", "Name", 21);
            ViewBag.ListTransportationId = new SelectList(db.Transportations.Where(m => m.Id != 0).ToList(), "Id", "Name", 8);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookingTour([Bind(Include = "Id,TourId,HotelId,TransportationId,UserId,BookDate,NumberOfAdults,NumberOfChildren,TotalPrice,Status")] Booking booking, int? id)
        {

            if (ModelState.IsValid)
            {
                var numberOfUnpaidTours = db.Bookings.Count(b => b.UserId == booking.UserId && b.Status == 1);
                if (numberOfUnpaidTours >= 3)
                {
                    TempData["message"] = new XMessage("danger", "Bạn đã đặt quá 3 tour mà chưa thanh toán. Vui lòng liên hệ 0898543218 để biết thêm chi tiết !!!");
                    return RedirectToAction("Index", "TourCus");
                }
                //var tour = db.Tours.Find(id);
                //tour.NumberOfParticipants -= (booking.NumberOfAdults + booking.NumberOfChildren);
                //db.SaveChanges();
                bookingDAO.Insert(booking);
                TempData["message"] = new XMessage("success", "Bạn đã đặt tour thành công. Chúng tôi sẽ sớm liên lạc với bạn !!!");
                return RedirectToAction("Index", "TourCus");
            }
            var model = (booking.TourId);
            var idtour = db.Tours.Find(id);
            ViewBag.SelectedTour = idtour;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.Name = tourInfo?.Name;
            ViewBag.Image = tourInfo?.Image;
            ViewBag.EndDate = tourInfo?.EndDate.ToString("yyyy-MM-dd");
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            ViewBag.ListHotelId = new SelectList(db.Hotels.Where(m => m.Id != 0).ToList(), "Id", "Name", 21);
            ViewBag.ListTransportationId = new SelectList(db.Transportations.Where(m => m.Id != 0).ToList(), "Id", "Name", 8);
            return View();
        }
    }
}