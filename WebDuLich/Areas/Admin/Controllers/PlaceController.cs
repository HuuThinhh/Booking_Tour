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
    public class PlaceController : LoginController
    {
        PlaceDAO placeDAO = new PlaceDAO();
        private DbConnect db = new DbConnect();

        // GET: Admin/Place
        public ActionResult Index()
        {
            return View(placeDAO.getList("Index"));
        }

        // GET: Admin/Place/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeDAO.getRow(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // GET: Admin/Place/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Place/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Place place)
        {
            if (ModelState.IsValid)
            {
                if (db.Places.Any(t => t.Name == place.Name))
                {
                    ModelState.AddModelError("Name", "Tên địa điểm đã tồn tại.");
                    return View(place);
                }
                placeDAO.Insert(place);
                TempData["message"] = new XMessage("success", "Thêm địa điểm thành công");
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Admin/Place/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeDAO.getRow(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Admin/Place/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Place place)
        {
            if (ModelState.IsValid)
            {
                if (db.Places.Any(t => t.Name == place.Name && t.Id != place.Id))
                {
                    ModelState.AddModelError("Name", "Tên địa điểm đã tồn tại.");
                    return View(place);
                }
                placeDAO.Update(place);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin địa điểm thành công");
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Admin/Place/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeDAO.getRow(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Admin/Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = placeDAO.getRow(id);
            bool hasAssociatedTours = db.Tours.Any(t => t.PlaceId == id);
            if (hasAssociatedTours)
            {
                TempData["message"] = new XMessage("danger", "Không thể xóa địa điểm. Vì có các tour liên quan đến địa điểm này");
            }
            else
            {
                placeDAO.Delete(place);
                TempData["message"] = new XMessage("success", "Xoá địa điểm thành công");
            }
            return RedirectToAction("Index");
        }
        //public ActionResult Status(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("Index", "Place");
        //        TempData["message"] = "Bill";
        //    }
        //    Place place = placeDAO.getRow(id);
        //    if (place == null)
        //    {
        //        return RedirectToAction("Index", "Place");
        //    }
        //    placeDAO.Update(place);
        //    TempData["message"] = "Bill";
        //    return RedirectToAction("Index", "Place");
        //}
    }
}