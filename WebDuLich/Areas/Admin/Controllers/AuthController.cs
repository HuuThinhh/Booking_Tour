using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data.Entity.Migrations;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        DbConnect db = new DbConnect();

        // GET: Admin/Auth
        public ActionResult Login()
        {
            if (!Session["UserAdmin"].Equals(""))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
            string strerror = "";
            string email = field["email"];
            string password = field["password"];
            //LoginDetails rowlogin = new LoginDetails();
            Employee rowuser = db.Employees.Where(m => m.Email == email).FirstOrDefault();
            if (rowuser == null)
            {
                strerror = "Email không tồn tại";
            }
            else
            {
                if (rowuser.CountError == 5 && rowuser.RoleId != 1)
                {
                    strerror = "Tài khoản đã bị khoá vui lòng liên hệ quản trị viên ";
                }
                else
                {
                    if (rowuser.Password.Equals(password))
                    {
                        Session["UserAdmin"] = rowuser.Email;
                        Session["UserId"] = rowuser.Id;
                        Session["UserName"] = rowuser.FirstName;
                        var listRoles = GetListRoleID(email);
                        DateTime now = DateTime.Now;
                        Session.Add("Session_role", listRoles);
                        rowuser.CountError = 0;
                        db.Entry(rowuser).State = EntityState.Modified;
                        db.SaveChanges();
                        //var loginHistory = new LoginDetails
                        //{
                        //    EmployeeId = rowuser.Id,
                        //    LoginTime = now
                        //};
                        //db.LoginDetailss.Add(loginHistory);
                        //db.SaveChanges();
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        rowuser.CountError += 1;
                        db.Entry(rowuser).State = EntityState.Modified;
                        db.SaveChanges();
                        strerror = "Mật khẩu không đúng";
                    }
                }
            }

            ViewBag.Error = "(*) " + strerror;
            return View();
        }

        public List<string> GetListRoleID(string email)
        {
            var data = (from a in db.Roles
                        join b in db.Employees on a.Id equals b.RoleId
                        where b.Email == email
                        select new
                        {
                            UserRoleID = b.RoleId,
                            UserRoleName = a.Name,
                        }
                      );
            return data.Select(x => x.UserRoleName).ToList();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["UserName"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}