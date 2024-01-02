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
    public class ReviewController : LoginController
    {
        ReviewDAO reviewDAO = new ReviewDAO();

        // GET: Admin/Review
        public ActionResult Index()
        {
            return View(reviewDAO.getList("Index"));
        }

        // GET: Admin/Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = reviewDAO.getRow(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Admin/Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TourId,CustomerId,Rating,Comment,ReviewDate")] Review review)
        {
            if (ModelState.IsValid)
            {
                reviewDAO.Insert(review);
                TempData["message"] = new XMessage("success", "Thêm bình luận thành công");
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Admin/Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = reviewDAO.getRow(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TourId,CustomerId,Rating,Comment,ReviewDate")] Review review)
        {
            if (ModelState.IsValid)
            {
                reviewDAO.Update(review);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin bình luận thành công");
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Admin/Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = reviewDAO.getRow(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = reviewDAO.getRow(id);
            reviewDAO.Delete(review);
            TempData["message"] = new XMessage("success", "Xoá bình luận thành công");
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
