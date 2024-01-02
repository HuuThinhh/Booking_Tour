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
    public class CustomerController : LoginController
    {
        CustomerDAO customerDAO = new CustomerDAO();
        private DbConnect db = new DbConnect();

        // GET: Admin/Customer
        public ActionResult Index()
        {
            return View(customerDAO.getList("Index"));
        }

        // GET: Admin/Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customerDAO.getRow(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Phone,Address,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerDAO.Insert(customer);
                TempData["message"] = new XMessage("success", "Thêm khách hàng thành công");
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Admin/Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customerDAO.getRow(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Phone,Address,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateCustomer(customer))
                {
                    if (db.Customers.Any(t => t.Email == customer.Email && t.Id != customer.Id))
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                    }
                    if (db.Customers.Any(h => h.Phone == customer.Phone && h.Id != customer.Id))
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    }
                    return View(customer);
                }
                customerDAO.Update(customer);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin khách hàng thành công");
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        private bool IsDuplicateCustomer(Customer customer)
        {
            return db.Customers.Any(t => (t.Email == customer.Email || t.Phone == customer.Phone) && t.Id != customer.Id);
        }

        // GET: Admin/Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = customerDAO.getRow(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = customerDAO.getRow(id);
            bool hasAssociatedTours = db.Bookings.Any(t => t.UserId == id);
            if (hasAssociatedTours)
            {
                TempData["message"] = new XMessage("danger", "Không thể xóa khách hàng. Vì có các booking của khách hàng liên quan đến khách hàng này");
            }
            else
            {
                customerDAO.Delete(customer);
                TempData["message"] = new XMessage("success", "Xoá khách hàng thành công");
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
