using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class TransportationController : LoginController
    {
        TransportationDAO transportationDAO = new TransportationDAO();
        private DbConnect db = new DbConnect();

        // GET: Admin/Transportation
        public ActionResult Index()
        {
            return View(transportationDAO.getList("Index"));
        }

        // GET: Admin/Transportation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = transportationDAO.getRow(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // GET: Admin/Transportation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Transportation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                if (db.Transportations.Any(t => t.Name == transportation.Name))
                {
                    ModelState.AddModelError("Name", "Tên phương tiện đã tồn tại.");
                    return View(transportation);
                }
                transportationDAO.Insert(transportation);
                TempData["message"] = new XMessage("success", "Thêm phương tiện thành công");
                return RedirectToAction("Index");
            }

            return View(transportation);
        }

        // GET: Admin/Transportation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = transportationDAO.getRow(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Admin/Transportation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                if (db.Transportations.Any(t => t.Name == transportation.Name && t.Id != transportation.Id))
                {
                    ModelState.AddModelError("Name", "Tên phương tiện đã tồn tại.");
                    return View(transportation);
                }
                transportationDAO.Update(transportation);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin phương tiện thành công");
                return RedirectToAction("Index");
            }
            return View(transportation);
        }

        // GET: Admin/Transportation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = transportationDAO.getRow(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Admin/Transportation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportation transportation = transportationDAO.getRow(id);
            bool hasAssociatedTours = db.Bookings.Any(t => t.TransportationId == id);
            if (hasAssociatedTours)
            {
                TempData["message"] = new XMessage("danger", "Không thể xóa phương tiện. Vì có các booking của khách hàng liên quan đến phương tiện này");
            }
            else
            {
                transportationDAO.Delete(transportation);
                TempData["message"] = new XMessage("success", "Xoá phương tiện thành công");
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
