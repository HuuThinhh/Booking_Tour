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

namespace WebDuLich.Areas.Admin.Controllers
{
    public class HotelController : LoginController
    {
        HotelDAO hotelDAO = new HotelDAO();
        private DbConnect db = new DbConnect();

        // GET: Admin/Hotel
        public ActionResult Index()
        {
            return View(hotelDAO.getList("Index"));
        }

        // GET: Admin/Hotel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = hotelDAO.getRow(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Admin/Hotel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,PhoneNumber")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateHotel(hotel))
                {
                    if (db.Hotels.Any(t => t.Name == hotel.Name && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("Name", "Tên khách sạn đã tồn tại.");

                    }
                    if (db.Hotels.Any(t => t.Address == hotel.Address && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("Address", "Địa chỉ khách sạn đã tồn tại.");

                    }
                    if (db.Hotels.Any(t => t.PhoneNumber == hotel.PhoneNumber && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("PhoneNumber", "Số điện thoại khách sạn đã tồn tại.");

                    }
                    return View(hotel);
                }
                hotelDAO.Insert(hotel);
                TempData["message"] = new XMessage("success", "Thêm khách sạn thành công");
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Admin/Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = hotelDAO.getRow(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }
        private bool IsDuplicateHotel(Hotel hotel)
        {
            return db.Hotels.Any(t => (t.Name == hotel.Name || t.Address == hotel.Address || t.PhoneNumber == hotel.PhoneNumber) && t.Id != hotel.Id);
        }

        // POST: Admin/Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,PhoneNumber")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateHotel(hotel))
                {
                    if (db.Hotels.Any(t => t.Name == hotel.Name && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("Name", "Tên khách sạn đã tồn tại.");
                        
                    }
                    if (db.Hotels.Any(t => t.Address == hotel.Address && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("Address", "Địa chỉ khách sạn đã tồn tại.");
                        
                    }
                    if (db.Hotels.Any(t => t.PhoneNumber == hotel.PhoneNumber && t.Id != hotel.Id))
                    {
                        ModelState.AddModelError("PhoneNumber", "Số điện thoại khách sạn đã tồn tại.");
                        
                    }
                    return View(hotel);
                }
                hotelDAO.Update(hotel);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin khách sạn thành công");
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Admin/Hotel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = hotelDAO.getRow(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Admin/Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = hotelDAO.getRow(id);
            bool hasAssociatedTours = db.Bookings.Any(t => t.HotelId == id);
            if (hasAssociatedTours)
            {
                TempData["message"] = new XMessage("danger", "Không thể xóa khách sạn. Vì có các booking của khách hàng liên quan đến khách sạn này");
            }
            else
            {
                hotelDAO.Delete(hotel);
                TempData["message"] = new XMessage("success", "Xoá khách sạn thành công");
            }
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
