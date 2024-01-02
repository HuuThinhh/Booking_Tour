using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace WebDuLich.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        DbConnect db = new DbConnect();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult Edit()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var cus = (MyClass.Models.Customer)Session["user"];
            return View(cus);
        }

        public ActionResult ChangePass()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult QrCode()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string email,string firstname, string lastname, string address)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            mapCustomer map = new mapCustomer();
            Customer cus = (Customer)Session["user"];
            map.ChinhSua(cus, firstname, lastname, address);
            TempData["message"] = new XMessage("success", "Sửa thông tin thành công");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ChangePass(string oldpass, string newpass, string confirmpass)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Customer cus = (Customer)Session["user"];
            if(oldpass != cus.Password)
            {
                ViewBag.thongbao = "Mật khẩu không đúng";
                return View();
            }
            if(newpass != confirmpass)
            {
                ViewBag.thongbao = "Mật khẩu mới không trùng khớp";
                return View();
            }
            mapCustomer map = new mapCustomer();
            map.DoiMK(cus, confirmpass);
            TempData["message"] = new XMessage("success", "Đổi mật khẩu thành công");
            return View();
        }
        public ActionResult History()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var customer = (MyClass.Models.Customer)Session["user"];

            var history = db.Bookings
        .Where(b => b.UserId == customer.Id)
        .ToList();
            return View(history);
        }

    }
}
