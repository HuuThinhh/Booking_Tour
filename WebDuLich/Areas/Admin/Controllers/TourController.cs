using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class TourController : LoginController
    {
        TourDAO tourDAO = new TourDAO();
        private DbConnect db = new DbConnect();

        // GET: Admin/Tour
        public ActionResult Index()
        {
            return View(tourDAO.getList("Index"));
        }

        // GET: Admin/Tour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = tourDAO.getRow(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(tour);
        }

        // GET: Admin/Tour/Create
        public ActionResult Create()
        {
            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View();
        }

        // POST: Admin/Tour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //public ActionResult Create([Bind(Include = "Id,Name,Description,PlaceId,NumberOfParticipants,AdultPrice,ChildPrice,EndDate,Image,IsDomestic,Type")] Tour tour)
        //{
        //    if (ModelState.IsValid)
        //    { 
        //        if (tour.Image == null)
        //        {
        //            ModelState.AddModelError("Image", "Ảnh không được để trống");
        //            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
        //            return View(tour);
        //        }
        //        if (db.Tours.Any(t => t.Image == tour.Image))
        //        {
        //            ModelState.AddModelError("Image", "Ảnh này đã được sử dụng. Vui lòng chọn ảnh khác!");
        //            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
        //            return View(tour);
        //        }
        //        if (db.Tours.Any(t => t.Name == tour.Name))
        //        {
        //            ModelState.AddModelError("Name", "Tên tour đã tồn tại. Vui lòng nhập tên khác!");
        //            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
        //            return View(tour);
        //        }

        //        tourDAO.Insert(tour);
        //        TempData["message"] = new XMessage("success", "Thêm tour thành công");
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
        //    return View(tour);
        //}
        public ActionResult Create([Bind(Include = "Id,Name,Description,PlaceId,NumberOfParticipants,AdultPrice,ChildPrice,EndDate,IsDomestic,Type")] Tour tour, HttpPostedFileBase desktopImage)
        {
            if (ModelState.IsValid)
            {
                if (db.Tours.Any(t => t.Name == tour.Name && t.Id != tour.Id))
                {
                    ModelState.AddModelError("Name", "Tên tour đã tồn tại.");
                    ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(tour);
                }
                if (desktopImage == null || desktopImage.ContentLength <= 0)
                {
                    ModelState.AddModelError("desktopImage", "Ảnh không được để trống");
                    ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(tour);
                }

                string fileName = Path.GetFileName(desktopImage.FileName);
                string destinationPath = Server.MapPath("~/Public/images/tour_images/") + fileName;

                if (db.Tours.Any(t => t.Image == fileName))
                {
                    ModelState.AddModelError("desktopImage", "Ảnh này đã được sử dụng. Vui lòng chọn ảnh khác!");
                    ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(tour);
                }
             
                desktopImage.SaveAs(destinationPath);
              
                tour.Image = fileName;
               
                tourDAO.Insert(tour);

                TempData["message"] = new XMessage("success", "Thêm tour thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(tour);
        }

        // GET: Admin/Tour/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = tourDAO.getRow(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(tour);
        }

        // POST: Admin/Tour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,PlaceId,NumberOfParticipants,AdultPrice,ChildPrice,EndDate,Image,IsDomestic,Type")] Tour tour, int? id, HttpPostedFileBase desktopImage)
        {
            if (ModelState.IsValid)
            {
                if (db.Tours.Any(t => t.Name == tour.Name && t.Id != tour.Id))
                {
                    ModelState.AddModelError("Name", "Tên tour đã tồn tại.");
                    ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(tour);
                }
                if (desktopImage != null && desktopImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(desktopImage.FileName);
                    string destinationPath = Server.MapPath("~/Public/images/tour_images/") + fileName;
                    desktopImage.SaveAs(destinationPath);
                    if (IsImageNameUsed(fileName, id))
                    {
                        ModelState.AddModelError("desktopImage", "Ảnh vừa chọn đã được sử dụng. Vui lòng chọn ảnh khác!");
                        ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
                        return View(tour);
                    }
                    tour.Image = fileName;
                }
                if (desktopImage == null)
                {
                    var tourInfo = db.Tours.FirstOrDefault(t => t.Id == id);
                    var anh = tourInfo?.Image;
                    tour.Image = anh;
                }

                tourDAO.Update(tour);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin tour thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListPlaceId = new SelectList(db.Places.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(tour);
        }
        private bool IsImageNameUsed(string imageName, int? currentTourId)
        {
            return db.Tours.Any(t => t.Image == imageName && t.Id != currentTourId);
        }

        // GET: Admin/Tour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = tourDAO.getRow(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = tourDAO.getRow(id);
            bool hasAssociatedTours = db.Bookings.Any(t => t.TourId == id);
            if (hasAssociatedTours)
            {
                TempData["message"] = new XMessage("danger", "Không thể xóa tour. Vì có các booking của khách hàng liên quan đến tour này");
            }
            else
            {
                tourDAO.Delete(tour);
                TempData["message"] = new XMessage("success", "Xoá tour thành công");
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
