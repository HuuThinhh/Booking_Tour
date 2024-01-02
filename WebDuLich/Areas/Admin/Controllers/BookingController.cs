using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.Web.Helpers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class BookingController : LoginController
    {
        BookingDAO bookingDAO = new BookingDAO();
        private DbConnect db = new DbConnect();
        TourDAO tourDAO = new TourDAO();

        // GET: Admin/Booking
        public ActionResult Index()
        {

            return View(bookingDAO.getList("Index"));
        }

        // GET: Admin/Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingDAO.getRow(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

       

        // GET: Admin/Booking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TourId,HotelId,TransportationId,UserId,BookDate,NumberOfAdults,NumberOfChildren,TotalPrice,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                bookingDAO.Insert(booking);
                TempData["message"] = new XMessage("success", "Thêm  thành công");
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Admin/Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingDAO.getRow(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            var CusInfo = db.Customers.FirstOrDefault(t => t.Id == booking.UserId);
            ViewBag.Email = CusInfo?.Email;
            ViewBag.FirstName = CusInfo?.FirstName;
            ViewBag.LastName = CusInfo?.LastName;
            return View(booking);
        }

        // POST: Admin/Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TourId,HotelId,TransportationId,UserId,BookDate,NumberOfAdults,NumberOfChildren,TotalPrice,Status")] Booking booking, string email, string phone)
        {
            if (ModelState.IsValid)
            {
                var tour = db.Tours.Find(booking.TourId);
                var customer = db.Customers.Find(booking.UserId);
                if (booking.Status == 3)
                {
                    db.SaveChanges();
                    await Library.Mail.SendMailGoogleSmtp("QuangDaTour@gmail.com", customer.Email, "Thông báo thanh toán tour", "Bạn đã thanh toán tour " + tour.Name + " thành công. Chúng tôi cảm ơn bạn đã tin tưởng đặt tour ở Quảng Đà Tour !", "baovuong158@gmail.com", "ljze qnww mvrc hljl");
                }
                if (booking.Status == 2 && tour.NumberOfParticipants >= (booking.NumberOfAdults + booking.NumberOfChildren))
                {
                    tour.NumberOfParticipants -= (booking.NumberOfAdults + booking.NumberOfChildren);
                    db.SaveChanges();
                    await Library.Mail.SendMailGoogleSmtp("QuangDaTour@gmail.com", customer.Email, "Thông báo đặt cọc tour", "Bạn đã đặt cọc tour " + tour.Name + " thành công. Chúc bạn sẽ có 1 tour du lịch vui vẻ tại Quảng Đà Tour !", "baovuong158@gmail.com", "ljze qnww mvrc hljl");
                }
                if (booking.Status == 1)
                {
                    tour.NumberOfParticipants += (booking.NumberOfAdults + booking.NumberOfChildren);
                    db.SaveChanges();
                }


                bookingDAO.Update(booking);
                TempData["message"] = new XMessage("success", "Chỉnh sửa tình trạng thanh toán thành công");
                return RedirectToAction("Index");
            }
            var CusInfo = db.Customers.FirstOrDefault(t => t.Id == booking.UserId);
            ViewBag.Email = CusInfo?.Email;
            ViewBag.FirstName = CusInfo?.FirstName;
            ViewBag.LastName = CusInfo?.LastName;
            return View(booking);
        }
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingDAO.getRow(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            var CusInfo = db.Customers.FirstOrDefault(t => t.Id == booking.UserId);
            ViewBag.Email = CusInfo?.Email;
            ViewBag.FirstName = CusInfo?.FirstName;
            ViewBag.LastName = CusInfo?.LastName;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == booking.TourId);
            ViewBag.EndDate = tourInfo?.EndDate.ToString("yyyy-MM-dd");
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.ListTourId = new SelectList(db.Tours.Where(m => m.Id != 0).ToList(), "Id", "Name");
            ViewBag.ListHotelId = new SelectList(db.Hotels.Where(m => m.Id != 0).ToList(), "Id", "Name");
            ViewBag.ListTransportationId = new SelectList(db.Transportations.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(booking);
        }

        // POST: Admin/Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id,TourId,HotelId,TransportationId,UserId,BookDate,NumberOfAdults,NumberOfChildren,TotalPrice,Status")] Booking booking)
        {

            if (ModelState.IsValid)
            {
                bookingDAO.Update(booking);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin thành công");
                return RedirectToAction("Index");
            }
            var CusInfo = db.Customers.FirstOrDefault(t => t.Id == booking.UserId);
            ViewBag.Email = CusInfo?.Email;
            ViewBag.FirstName = CusInfo?.FirstName;
            ViewBag.LastName = CusInfo?.LastName;
            var tourInfo = db.Tours.FirstOrDefault(t => t.Id == booking.TourId);
            ViewBag.EndDate = tourInfo?.EndDate.ToString("yyyy-MM-dd");
            ViewBag.NumberOfParticipants = tourInfo?.NumberOfParticipants;
            ViewBag.AdultPrice = tourInfo?.AdultPrice;
            ViewBag.ChildPrice = tourInfo?.ChildPrice;
            ViewBag.ListTourId = new SelectList(db.Tours.Where(m => m.Id != 0).ToList(), "Id", "Name");
            ViewBag.ListHotelId = new SelectList(db.Hotels.Where(m => m.Id != 0).ToList(), "Id", "Name");
            ViewBag.ListTransportationId = new SelectList(db.Transportations.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(booking);
        }

        // GET: Admin/Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingDAO.getRow(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Admin/Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = bookingDAO.getRow(id);
            bookingDAO.Delete(booking);
            TempData["message"] = new XMessage("success", "Xoá tour được booking thành công");
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
