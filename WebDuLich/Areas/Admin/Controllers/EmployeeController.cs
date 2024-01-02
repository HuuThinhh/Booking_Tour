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

namespace WebDuLich.Areas.Admin.Controllers
{
    public class EmployeeController : LoginController
    {
        EmployeeDAO employeeDAO = new EmployeeDAO();
        private DbConnect db = new DbConnect();
        // GET: Admin/Employee
        public ActionResult Index()
        {
            return View(employeeDAO.getList("Index"));
        }

        // GET: Admin/Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeDAO.getRow(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult ViewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeDAO.getRow(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }


        // GET: Admin/Employee/Create
        public ActionResult Create()
        {
            ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View();
        }

        // POST: Admin/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Phone,Address,Password,RoleId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateEmployee(employee))
                {
                    if (db.Employees.Any(t => t.Email == employee.Email && t.Id != employee.Id))
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                    }
                    if (db.Employees.Any(h => h.Phone == employee.Phone && h.Id != employee.Id))
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    }
                    ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(employee);
                }
                employeeDAO.Insert(employee);
                TempData["message"] = new XMessage("success", "Thêm nhân viên thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(employee);
        }
        private bool IsDuplicateEmployee(Employee employee)
        {
            return db.Employees.Any(t => (t.Email == employee.Email || t.Phone == employee.Phone) && t.Id != employee.Id);
        }

        // GET: Admin/Employee/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeDAO.getRow(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(employee);
        }


        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Phone,Address,Password,RoleId,CountError")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateEmployee(employee))
                {
                    if (db.Employees.Any(t => t.Email == employee.Email && t.Id != employee.Id))
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                    }
                    if (db.Employees.Any(h => h.Phone == employee.Phone && h.Id != employee.Id))
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    }
                    ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
                    return View(employee);
                }
                employeeDAO.Update(employee);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin nhân viên thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListRoleId = new SelectList(db.Roles.Where(m => m.Id != 0).ToList(), "Id", "Name");
            return View(employee);
        }


        // GET: Admin/Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeDAO.getRow(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = employeeDAO.getRow(id);
            employeeDAO.Delete(employee);
            TempData["message"] = new XMessage("success", "Xoá nhân viên thành công");
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
        public ActionResult ViewEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = employeeDAO.getRow(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewEdit([Bind(Include = "Id,FirstName,LastName,Email,Phone,Address,Password,RoleId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (IsDuplicateEmployee(employee))
                {
                    if (db.Employees.Any(t => t.Email == employee.Email && t.Id != employee.Id))
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại.");
                    }
                    if (db.Employees.Any(h => h.Phone == employee.Phone && h.Id != employee.Id))
                    {
                        ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                    }
                    return View(employee);
                }
                employeeDAO.Update(employee);
                TempData["message"] = new XMessage("success", "Chỉnh sửa thông tin thành công");
                return RedirectToAction("ViewDetails/", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}